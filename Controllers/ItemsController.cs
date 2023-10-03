using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using aluguel.Models;
using aluguel.ViewModel;
using aluguel.Areas.Identity.Data;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace aluguel.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ILogger<ItemsController> _logger;
        private readonly ApplicationDbContext Context;
        private readonly IWebHostEnvironment WebHostEnvironment;

        public ItemsController(ILogger<ItemsController> logger, ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            Context = context;
            WebHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var model = await Context.Items
                            .Where(a => a.iduser == HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)
                            .ToListAsync();
            return View(model);

        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || Context.Items == null)
            {
                return NotFound();
            }

            var item = await Context.Items
                .FirstOrDefaultAsync(m => m.id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ItemViewModel vm)
        {
            string stringFileName = UploadFile(vm);
            var item = new Item
            {
                nmitem = vm.nmitem,
                dsitem = vm.dsitem,
                dtcriacao = vm.dtcriacao = DateTime.Now,
                snativo = vm.snativo,
                iduser = vm.iduser,
                imagem = stringFileName

            };
            Context.Items.Add(item);
            Context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || Context.Items == null)
            {
                return NotFound();
            }

            var item = await Context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nmitem,dsitem,dtcriacao,snativo,imagem,iduser")] Item item)
        {
            if (id != item.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Context.Update(item);
                    await Context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || Context.Items == null)
            {
                return NotFound();
            }

            var item = await Context.Items
                .FirstOrDefaultAsync(m => m.id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (Context.Items == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Items'  is null.");
            }
            var item = await Context.Items.FindAsync(id);
            if (item != null)
            {
                Context.Items.Remove(item);
            }
            
            await Context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private string UploadFile(ItemViewModel vm)
        {
            string fileName = null;
            if (vm.imagem != null)
            {
                string uploadDir = Path.Combine(WebHostEnvironment.WebRootPath, "imagens");
                fileName = Guid.NewGuid().ToString() + "-" + vm.imagem.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    vm.imagem.CopyTo(fileStream);
                }
            }
            return fileName;
        }

        private bool ItemExists(int id)
        {
          return (Context.Items?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
