using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockControl.Data;
using StockControl.Models;

namespace StockControl.Controllers
{
    public class MachinesController : Controller
    {
        private readonly AppDbContext _context;
        private const int PageSize = 9; // Her sayfada 9 makine

        public MachinesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Machines
        public async Task<IActionResult> Index(int page = 1)
        {
            var totalMachines = await _context.Machines.CountAsync();
            var machines = await _context.Machines
                .Include(m => m.Parts)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            ViewBag.TotalPages = (int)Math.Ceiling(totalMachines / (double)PageSize);
            ViewBag.CurrentPage = page;

            return View(machines);
        }

        // GET: Machines/SearchMachine
        public async Task<IActionResult> SearchMachine(string searchString, int page = 1)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                var trimmedSearchString = searchString.Trim().ToLower();
                var totalMachines = await _context.Machines
                    .Where(m => m.Name.Trim().ToLower().Contains(trimmedSearchString))
                    .CountAsync();
                var machines = await _context.Machines
                    .Include(m => m.Parts)
                    .Where(m => m.Name.Trim().ToLower().Contains(trimmedSearchString))
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize)
                    .ToListAsync();

                if (machines.Any())
                {
                    ViewBag.SearchPerformed = true;
                    ViewBag.TotalPages = (int)Math.Ceiling(totalMachines / (double)PageSize);
                    ViewBag.CurrentPage = page;
                    return View("Index", machines);
                }
                else
                {
                    ViewBag.ErrorMessage = "Bu tarz bir makine bulunamadı.";
                    var allMachines = await _context.Machines.Include(m => m.Parts).ToListAsync();
                    ViewBag.TotalPages = (int)Math.Ceiling(allMachines.Count / (double)PageSize);
                    ViewBag.CurrentPage = 1;
                    return View("Index", allMachines);
                }
            }
            return RedirectToAction("Index", new { page });
        }

        // GET: Machines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var machine = await _context.Machines
                .Include(m => m.Parts)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (machine == null) return NotFound();

            return View(machine);
        }

        // GET: Machines/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Machines/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Machine machine, [FromForm] List<string> PartName, [FromForm] List<int?> PartStockQuantity, int? page)
        {
            if (ModelState.IsValid)
            {
                if (PartName != null && PartStockQuantity != null && PartName.Count == PartStockQuantity.Count)
                {
                    machine.Parts = new List<Part>();
                    for (int i = 0; i < PartName.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(PartName[i]) && PartStockQuantity[i].HasValue)
                        {
                            machine.Parts.Add(new Part { Name = PartName[i], StockQuantity = PartStockQuantity[i].Value });
                        }
                    }
                }
                else
                {
                    machine.Parts = new List<Part>();
                }

                _context.Machines.Add(machine);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { page = page ?? 1 });
            }
            return View(machine);
        }

        // GET: Machines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var machine = await _context.Machines.FindAsync(id);
            if (machine == null) return NotFound();

            return View(machine);
        }

        // POST: Machines/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Machine machine, string NewPartName, int? NewPartStockQuantity, int? page)
        {
            if (id != machine.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var existingMachine = await _context.Machines.Include(m => m.Parts).FirstOrDefaultAsync(m => m.Id == id);
                    if (existingMachine != null)
                    {
                        existingMachine.Name = machine.Name;

                        if (!string.IsNullOrEmpty(NewPartName) && NewPartStockQuantity.HasValue)
                        {
                            existingMachine.Parts.Add(new Part { Name = NewPartName, StockQuantity = NewPartStockQuantity.Value });
                        }

                        _context.Update(existingMachine);
                        await _context.SaveChangesAsync();
                    }
                    return RedirectToAction("Index", new { page = page ?? 1 });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Machines.Any(e => e.Id == machine.Id)) return NotFound();
                    throw;
                }
            }
            return View(machine);
        }

        // GET: Machines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var machine = await _context.Machines
                .Include(m => m.Parts)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (machine == null) return NotFound();

            return View(machine);
        }

        // POST: Machines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int? page)
        {
            var machine = await _context.Machines
                .Include(m => m.Parts)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (machine != null)
            {
                _context.Parts.RemoveRange(machine.Parts);
                _context.Machines.Remove(machine);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", new { page = page ?? 1 });
        }

        // GET: Machines/DeletePart/5
        public async Task<IActionResult> DeletePart(int partId, int machineId, int? page)
        {
            var part = await _context.Parts.Include(p => p.Machine).FirstOrDefaultAsync(p => p.Id == partId);
            if (part == null || part.Machine?.Id != machineId)
            {
                return NotFound();
            }

            _context.Parts.Remove(part);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new { id = machineId, page = page ?? 1 });
        }

        // GET: Machines/EditParts/5
        public async Task<IActionResult> EditParts(int? id)
        {
            if (id == null) return NotFound();

            var machine = await _context.Machines
                .Include(m => m.Parts)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (machine == null) return NotFound();

            return View(machine);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditParts(int machineId, List<int> partIds, List<string> partNames, List<int> partStockQuantities, int? page)
        {
            var machine = await _context.Machines
                .Include(m => m.Parts)
                .FirstOrDefaultAsync(m => m.Id == machineId);
            if (machine == null)
            {
                return NotFound();
            }

            if (partIds == null || partNames == null || partStockQuantities == null ||
                partIds.Count != partNames.Count || partNames.Count != partStockQuantities.Count)
            {
                ViewData["ErrorMessage"] = "Gönderilen parça verileri eksik veya hatalı. Lütfen tüm alanları doldurun.";
                return View(machine);
            }

            try
            {
                for (int i = 0; i < partIds.Count; i++)
                {
                    var partId = partIds[i];
                    var partName = partNames[i];
                    var partStockQuantity = partStockQuantities[i];

                    if (string.IsNullOrWhiteSpace(partName))
                    {
                        ViewData["ErrorMessage"] = "Parça adı boş olamaz.";
                        return View(machine);
                    }

                    if (partStockQuantity < 0)
                    {
                        ViewData["ErrorMessage"] = "Stok miktarı negatif olamaz.";
                        return View(machine);
                    }

                    var existingPart = machine.Parts.FirstOrDefault(p => p.Id == partId);
                    if (existingPart != null)
                    {
                        existingPart.Name = partName;
                        existingPart.StockQuantity = partStockQuantity;
                    }
                }

                _context.Update(machine);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new { id = machineId, page = page ?? 1 });
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = $"Bir hata oluştu: {ex.Message}";
                return View(machine);
            }
        }
    }
}