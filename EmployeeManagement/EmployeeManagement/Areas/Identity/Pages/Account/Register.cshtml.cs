using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            
            // Custom validations
            if (!string.IsNullOrEmpty(Input.Email))
            {
                // Check if email format is valid
                var emailAttribute = new EmailAddressAttribute();
                if (!emailAttribute.IsValid(Input.Email))
                {
                    ModelState.AddModelError(string.Empty, "Please enter a valid email address.");
                    return Page();
                }

                // Check if email already exists (use FirstOrDefault to handle duplicates)
                var existingUsers = await _userManager.Users.Where(u => u.Email == Input.Email).ToListAsync();
                if (existingUsers.Any())
                {
                    ModelState.AddModelError(string.Empty, "This email address is already registered. Please use a different email or try logging in.");
                    return Page();
                }
            }

            // Check if passwords match
            if (Input.Password != Input.ConfirmPassword)
            {
                ModelState.AddModelError(string.Empty, "Passwords do not match. Please make sure both passwords are identical.");
                return Page();
            }

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    try
                    {
                        var emailSubject = "Welcome to EMS - Please Confirm Your Email";
                        var emailBody = $@"<!DOCTYPE html>
<html>
<head>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
        .header {{ background: linear-gradient(135deg, #667eea 0%, #764ba2 100%); color: white; padding: 30px; text-align: center; border-radius: 10px 10px 0 0; }}
        .header h1 {{ margin: 0; font-size: 24px; }}
        .content {{ background: #f9f9f9; padding: 30px; border-radius: 0 0 10px 10px; }}
        .button {{ display: inline-block; background: linear-gradient(135deg, #667eea 0%, #764ba2 100%); color: white; padding: 15px 30px; text-decoration: none; border-radius: 8px; margin: 20px 0; font-weight: bold; }}
        .footer {{ text-align: center; color: #999; font-size: 12px; margin-top: 20px; }}
        .link {{ word-break: break-all; color: #667eea; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>🏢 EMS - Employee Management System</h1>
        </div>
        <div class='content'>
            <h2>Welcome! Confirm Your Email</h2>
            <p>Thank you for registering with EMS. Please confirm your email address to activate your account.</p>
            <div style='text-align: center;'>
                <a href='{HtmlEncoder.Default.Encode(callbackUrl)}' class='button'>Confirm My Email</a>
            </div>
            <p>Or copy and paste this link in your browser:</p>
            <p class='link'>{HtmlEncoder.Default.Encode(callbackUrl)}</p>
            <p style='margin-top: 30px;'><strong>Note:</strong> This link will expire in 24 hours. If you didn't create this account, please ignore this email.</p>
        </div>
        <div class='footer'>
            <p>© 2024 EMS Employee Management System. All rights reserved.</p>
        </div>
    </div>
</body>
</html>";
                        
                        await _emailSender.SendEmailAsync(Input.Email, emailSubject, emailBody);
                        _logger.LogInformation("Confirmation email sent to {Email}", Input.Email);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to send confirmation email to {Email}", Input.Email);
                        ModelState.AddModelError(string.Empty, $"Failed to send confirmation email: {ex.Message}. Please check your SMTP settings or try again later.");
                        // Still delete the user since email failed
                        await _userManager.DeleteAsync(user);
                        return Page();
                    }

                    // Always require email confirmation - auto-login disabled for security
                    return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
