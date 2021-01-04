using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Datingsida.Models;
using Datingsida.DataAccess;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Identity;


namespace Datingsida.Controllers
{
    public class ProfileController : Controller
    {
        private readonly DatingDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly UserManager<IdentityUser> _userManager;

        public ProfileController(DatingDbContext context, IWebHostEnvironment hostEnvironment, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _userManager = userManager;
        }
        

        // GET: Profile
        public async Task<IActionResult> Index()
        {
            
            //hämtar nuvarande användare (all data från Identity)
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser != null)
            {
                ViewBag.showVisitLink = ViewData["ShowVisitLink"] = false;
                ViewBag.showLinks = ViewData["ShowLinks"] = true; 
                List<ProfileModel> allProfiles = await _context.Profiles.ToListAsync();
                foreach (ProfileModel profile in allProfiles) 
                { 
                    if(currentUser.Id == profile.OwnerId)
                    {
                        List<ProfileModel> lst = new List<ProfileModel>();
                        lst.Add(profile);
                        IEnumerable<ProfileModel> enumerableprofile = lst;

                        return View(enumerableprofile);
                    }
                }
                
                return RedirectToAction("Create","Profile");
                
            }
            else
            {
                return NotFound();
            }
        }

        // GET: Profile/Visit
        public async Task<IActionResult> Visit(int? id)
        {

             if (id != null)
            {
                ViewBag.showVisitLink = ViewData["ShowVisitLink"] = false;
                ViewBag.showLinks = ViewData["ShowLinks"] = false;
                var profileModel = await _context.Profiles
                .FirstOrDefaultAsync(m => m.Id == id);
                if (profileModel == null)
                {
                    return NotFound();
                }
                List<ProfileModel> list = new List<ProfileModel>();
                list.Add(profileModel);
                IEnumerable<ProfileModel> enumerableprofile = list;
                return View(enumerableprofile);
            }
            else
            {
                return NotFound();
            }
        }
        // GET: Profile/Search
        public async Task<IActionResult> Search()
        {
            ViewBag.showVisitLink = ViewData["ShowVisitLink"] = true;
            ViewBag.showLinks = ViewData["ShowLinks"] = false;
            return View(await _context.Profiles.ToListAsync());
        }
        // POST: Profile/SearchResults
        public async Task<IActionResult> SearchResults(String SearchTerm)
        {
            ViewBag.showVisitLink = ViewData["ShowVisitLink"] = true;
            ViewBag.showLinks = ViewData["ShowLinks"] = false;
            return View("Search",await _context.Profiles.Where(profile => profile.FirstName.Contains(SearchTerm) ||
                                                                          profile.LastName.Contains(SearchTerm)).ToListAsync());
        }
      

        // GET: Profile/Create
        
        public IActionResult Create()
        {
            return View();
        }

        // POST: Profile/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Age,Gender,Sexuality,ImageFile,Presentation")] ProfileModel profileModel)
        {
            if (ModelState.IsValid)
            {
                //Vi sparar vår bild till foldern Image i wwwroot
                string wwwPath = _hostEnvironment.WebRootPath;
                string file = Path.GetFileNameWithoutExtension(profileModel.ImageFile.FileName);
                string extension = Path.GetExtension(profileModel.ImageFile.FileName);
                profileModel.ImageFilepath = file = file + DateTime.Now.ToString("yyMMddss") + extension;
                string path = Path.Combine(wwwPath + "/Image/", file);
                using (var fileStream = new FileStream(path,FileMode.Create))
                {
                    await profileModel.ImageFile.CopyToAsync(fileStream);
                }

                // hämtar inloggade användares id
                profileModel.OwnerId = _userManager.GetUserId(User);
                profileModel.IsActive = true;

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
            //Vi sparar namnet på bilden och använder den i Edit POST
            var imageFilepath = profileModel.ImageFilepath;
            TempData["imageFilepath"] = imageFilepath;
            return View(profileModel);
        }

        // POST: Profile/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,[Bind("Id,FirstName,LastName,Age,Gender,Sexuality,ImageFile,Presentation")] ProfileModel profileModel)
        {
            if (id != profileModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string wwwPath = _hostEnvironment.WebRootPath;
                    string file = Path.GetFileNameWithoutExtension(profileModel.ImageFile.FileName);
                    string extension = Path.GetExtension(profileModel.ImageFile.FileName);
                    profileModel.ImageFilepath = file = file + DateTime.Now.ToString("yyMMddss") + extension;
                    string newPath = Path.Combine(wwwPath+"/Image/", file);

                    //Här får vi namnet på den gamla bilden från Edit Get.
                    string oldImageFilepath = TempData["imageFilepath"].ToString();
                    string oldPath = Path.Combine(wwwPath + "/Image/", oldImageFilepath);

                    if (!newPath.Equals(oldPath))
                    {
                        using (var fileStream = new FileStream(newPath, FileMode.Create))
                        {
                            await profileModel.ImageFile.CopyToAsync(fileStream);
                        }

                        System.IO.File.Delete(oldPath);
                    }
                    profileModel.OwnerId = _userManager.GetUserId(User);
                    profileModel.IsActive = true;

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

            // Ta bort bild from mappen Image.
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "image", profileModel.ImageFilepath);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);

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
