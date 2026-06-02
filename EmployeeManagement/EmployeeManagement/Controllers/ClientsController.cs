
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Models;
using EmployeeManagement.Data;
using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc.Rendering;
using EmployeeManagement.Services;

public class ClientsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ClientsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: CLIENTS
    public async Task<IActionResult> Index()
    {
        var clients = await _context.Clients.Include(x => x.CreatedBy).Include(x => x.Status).ToListAsync();
        return View(clients);
    }

    // GET: CLIENTS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var client = await _context.Clients
            .FirstOrDefaultAsync(m => m.Id == id);
        if (client == null)
        {
            return NotFound();
        }

        return View(client);
    }

    // GET: CLIENTS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: CLIENTS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Client client)
    {
        try
        {

            var statusId = await _context.systemCodeDetails
                .Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "ClientsStatus" && x.Code == "Active").FirstOrDefaultAsync();

            var Userid = User.GetUserId();
            client.CreatedOn = DateTime.UtcNow;
            client.CreatedById = Userid;
            client.StatusId = statusId.Id;

            _context.Add(client);
            await _context.SaveChangesAsync(Userid);
            TempData["Message"] = "Clients details created Successfully!";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Clients details could not be created Successfully!" + ex.Message;
            return View(client);
        }

    }

    // GET: CLIENTS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var client = await _context.Clients.FindAsync(id);
        if (client == null)
        {
            return NotFound();
        }

        return View(client);
    }

    // POST: CLIENTS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, Client client)
    {
        if (id != client.Id)
        {
            return NotFound();
        }

        try
        {
            var Userid = User.GetUserId();
            client.ModifiedOn = DateTime.UtcNow;
            client.ModifiedById = Userid;
            _context.Update(client);

            await _context.SaveChangesAsync(Userid);
            TempData["Message"] = "Client details Updated Successfully!";
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ClientExists(client.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
    }

    // GET: CLIENTS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var client = await _context.Clients
            .FirstOrDefaultAsync(m => m.Id == id);
        if (client == null)
        {
            return NotFound();
        }

        return View(client);
    }

    // POST: CLIENTS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var client = await _context.Clients.FindAsync(id);
        if (client != null)
        {
            _context.Clients.Remove(client);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ClientExists(int? id)
    {
        return _context.Clients.Any(e => e.Id == id);
    }
}
