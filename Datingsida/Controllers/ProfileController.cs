using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Datingsida.Data;
using Datingsida.Models;
using Microsoft.AspNetCore.Authorization;
using Datingsida.DataAccess;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Datingsida.Controllers
{
    public class ProfileController : Controller
    {
        private readonly DatingDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProfileController(DatingDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Profile
        public async Task<IActionResult> Index()
        {
            return View(await _context.Profiles.ToListAsync());
        }

        // GET: Profile/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profileModel = await _context.Profiles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profileModel == null)
            {
                return NotFound();
            }

            return View(profileModel);
        }

        // GET: Profile/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Profile/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Age,Gender,Sexuality,ImageFile,Presentation,IsActive")] ProfileModel profileModel)
        {
            if (ModelState.IsValid)
            {
                //Vi sparar vår bild till foldern Image i wwwroot
                string wwwPath = _hostEnvironment.WebRootPath;
                string file = Path.GetFileNameWithoutExtension(profileModel.ImageFile.FileName);
                string extension = Path.GetExtension(profileModel.ImageFile.FileName);
                profileModel.ImageFilepath = file = file + DateTime.Now.ToString("yymmddss") + extension;
                string path = Path.Combine(wwwPath + "/Image/", file);
                using (var fileStream = new FileStream(path,FileMode.Create))
                {
                    await profileModel.ImageFile.CopyToAsync(fileStream);
                }

                //Lägger till det vi sparat
                _context.Add(profileModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(profileModel);
        }

        // GET: Profile/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profileModel = await _context.Profiles.FindAsync(id);
            if (profileModel == null)
            {
                return NotFound();
            }
            return View(profileModel);
        }

        // POST: Profile/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Age,Gender,Sexuality,ImageFilepath,Presentation,IsActive")] ProfileModel profileModel)
        {
            if (id != profileModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profileModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfileModelExists(profileModel.Id))
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
            return View(profileModel);
        }

        // GET: Profile/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profileModel = await _context.Profiles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profileModel == null)
            {
                return NotFound();
            }

            return View(profileModel);
        }

        // POST: Profile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profileModel = await _context.Profiles.FindAsync(id);
            _context.Profiles.Remove(profileModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfileModelExists(int id)
        {
            return _context.Profiles.Any(e => e.Id == id);
        }
    }
}
