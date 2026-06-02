using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using EmployeeManagement.Models;

namespace EmployeeManagement.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public ForgotPasswordModel(UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                // Check if email exists (use FirstOrDefault to handle duplicates)
                var existingUsers = await _userManager.Users.Where(u => u.Email == Input.Email).ToListAsync();
                if (!existingUsers.Any())
                {
                    // Show error - email not registered
                    ModelState.AddModelError(string.Empty, "This email address is not registered. Please check your email or register a new account.");
                    return Page();
                }
                
                var user = existingUsers.First();

                // For more information on how to enable account confirmation and password reset please 
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { area = "Identity", code },
                    protocol: Request.Scheme);

                try
                {
                    var emailSubject = "EMS - Reset Your Password";
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
            <h2>Password Reset Request</h2>
            <p>We received a request to reset your password. Click the button below to create a new password.</p>
            <div style='text-align: center;'>
                <a href='{HtmlEncoder.Default.Encode(callbackUrl)}' class='button'>Reset My Password</a>
            </div>
            <p>Or copy and paste this link in your browser:</p>
            <p class='link'>{HtmlEncoder.Default.Encode(callbackUrl)}</p>
            <p style='margin-top: 30px;'><strong>Note:</strong> This link will expire in 24 hours. If you didn't request a password reset, please ignore this email.</p>
        </div>
        <div class='footer'>
            <p>© 2024 EMS Employee Management System. All rights reserved.</p>
        </div>
    </div>
</body>
</html>";
                    
                    await _emailSender.SendEmailAsync(Input.Email, emailSubject, emailBody);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Failed to send password reset email: {ex.Message}. Please check your SMTP settings or try again later.");
                    return Page();
                }

                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            return Page();
        }
    }
}
