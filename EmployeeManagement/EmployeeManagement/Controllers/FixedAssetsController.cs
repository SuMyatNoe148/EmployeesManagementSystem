
using EmployeeManagement.Data;
using EmployeeManagement.Data.Migrations;
using EmployeeManagement.Models;
using EmployeeManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

public class FixedAssetsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public FixedAssetsController(IConfiguration configuration, ApplicationDbContext context)
    {
        _context = context;
        _configuration = configuration;
    }

    // GET: FIXEDASSETS
    public async Task<IActionResult> Index()
    {
        var asset = await _context.FixedAssets.Include(x => x.Category).Include(x => x.CreatedBy).Include(x => x.ModifiedBy)
            .Include(x => x.Status)
            .Include(x => x.ResponsibleEmployee)
            .ToListAsync();
        return View(asset);
    }

    // GET: FIXEDASSETS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var fixedasset = await _context.FixedAssets
            .Include(x => x.Category)
            .Include(x => x.CreatedBy)
            .Include(x => x.ModifiedBy)
            .Include(x => x.Status)
            .Include(x => x.ResponsibleEmployee)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (fixedasset == null)
        {
            return NotFound();
        }

        return View(fixedasset);
    }

    // GET: FIXEDASSETS/Create
    public IActionResult Create()
    {
        ViewData["CategoryId"] = new SelectList(_context.systemCodeDetails.Include(x => x.SystemCode)
            .Where(x => x.SystemCode.Code == "AssetCategory"), "Id", "Description");
        ViewData["ResponsibleEmployeeId"] = new SelectList(_context.Employees, "Id", "FullName");

        return View();
    }

    // POST: FIXEDASSETS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(FixedAsset fixedasset, IFormFile assetphoto)
    {
        try
        {
            if (assetphoto != null && assetphoto.Length > 0)
            {
                var fileName = "AssetPhoto_" + DateTime.Now.ToString("yyyymmddhhmmss") + "_" + assetphoto.FileName;
                var path = _configuration["FileSettings:UploadFolder"]!;
                var filePath = Path.Combine(path, fileName);
                var stream = new FileStream(filePath, FileMode.Create);
                await assetphoto.CopyToAsync(stream);
                fixedasset.Photo = fileName;
            }
            var fixedassetstatus = await _context.systemCodeDetails
                .Include(x => x.SystemCode)
                .Where(x => x.SystemCode.Code == "AssetStatus" && x.Code == "Active")
                .FirstOrDefaultAsync();

            fixedasset.StatusId = fixedassetstatus.Id;

            var Userid = User.GetUserId();
            fixedasset.CreatedOn = DateTime.UtcNow;
            fixedasset.CreatedById = Userid;

            _context.Add(fixedasset);
            await _context.SaveChangesAsync(Userid);
            TempData["Message"] = "Asset Created Successfully!";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Asset could not be created Successfully!" + ex.Message;
            return View(fixedasset);
        }

        ViewData["CategoryId"] = new SelectList(_context.systemCodeDetails.Include(x => x.SystemCode)
            .Where(x => x.SystemCode.Code == "AssetCategory"), "Id", "Description", fixedasset.CategoryId);
        ViewData["ResponsibleEmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", fixedasset.ResponsibleEmployeeId);

    }

    // GET: FIXEDASSETS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var fixedasset = await _context.FixedAssets.FindAsync(id);
        if (fixedasset == null)
        {
            return NotFound();
        }
        ViewData["CategoryId"] = new SelectList(_context.systemCodeDetails.Include(x => x.SystemCode)
            .Where(x => x.SystemCode.Code == "AssetCategory"), "Id", "Description", fixedasset.CategoryId);
        ViewData["ResponsibleEmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", fixedasset.ResponsibleEmployeeId);
        return View(fixedasset);
    }

    // POST: FIXEDASSETS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, FixedAsset fixedasset, IFormFile assetphoto)
    {
        if (id != fixedasset.Id)
        {
            return NotFound();
        }

        if (assetphoto != null && assetphoto.Length > 0)
        {
            var fileName = "AssetPhoto_" + DateTime.Now.ToString("yyyymmddhhmmss") + "_" + assetphoto.FileName;
            var path = _configuration["FileSettings:UploadFolder"]!;
            var filePath = Path.Combine(path, fileName);
            var stream = new FileStream(filePath, FileMode.Create);
            await assetphoto.CopyToAsync(stream);
            fixedasset.Photo = fileName;
        }
        var fixedassetstatus = await _context.systemCodeDetails
               .Include(x => x.SystemCode)
               .Where(x => x.SystemCode.Code == "AssetStatus" && x.Code == "Active")
               .FirstOrDefaultAsync();

        fixedasset.StatusId = fixedassetstatus.Id;

        var Userid = User.GetUserId();
        fixedasset.ModifiedById = Userid;
        fixedasset.ModifiedOn = DateTime.UtcNow;
        try
        {
            _context.Update(fixedasset);
            await _context.SaveChangesAsync(Userid);
            TempData["Message"] = "Fixed Asset Updated Successful!";
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!FixedAssetExists(fixedasset.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
       

        ViewData["CategoryId"] = new SelectList(_context.systemCodeDetails.Include(x => x.SystemCode)
            .Where(x => x.SystemCode.Code == "AssetCategory"), "Id", "Description", fixedasset.CategoryId);
        ViewData["ResponsibleEmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", fixedasset.ResponsibleEmployeeId);
        return View(fixedasset);
    }

    // GET: FIXEDASSETS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var fixedasset = await _context.FixedAssets
            .FirstOrDefaultAsync(m => m.Id == id);
        if (fixedasset == null)
        {
            return NotFound();
        }

        return View(fixedasset);
    }

    // POST: FIXEDASSETS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var fixedasset = await _context.FixedAssets.FindAsync(id);
        if (fixedasset != null)
        {
            _context.FixedAssets.Remove(fixedasset);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool FixedAssetExists(int? id)
    {
        return _context.FixedAssets.Any(e => e.Id == id);
    }
}
