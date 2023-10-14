using btth01.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace btth01.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class PostController : Controller
    {
        private readonly DataContext _context;
        public IActionResult Index()
        {
            return View();
        }
        public PostController(DataContext context)
        {
            _context=context;
        }
        public IActionResult Delete(int? id)
        {
            if (id==null ||id==0) {
                return NotFound();
            }
            var mn = _context.Menus.Find(id);
            if (mn==null) {
                return NotFound();
            }
            return View(mn);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var deleMenu = _context.Menus.Find(id);
            if (deleMenu==null) {
                return NotFound();
            }
            _context.Menus.Remove(deleMenu);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            var mnList = (from m in _context.Menus
                          select new SelectListItem()
                          {
                              Text=m.MenuName,
                              Value=m.MenuID.ToString()

                          }).ToList();
            mnList.Insert(0, new SelectListItem()
            {
                Text="----Select----",
                Value=string.Empty
            });
            ViewBag.mnList=mnList;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Post post)
        {
            if (ModelState.IsValid) {
                _context.Add(post);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int? id)
        {

            if (id == null || id == 0) {
                return NotFound();
            }
            var mn = _context.Menus.Find(id);
            if (mn == null) {
                return NotFound();
            }
            var mnList = (from m in _context.Menus
                          select new SelectListItem()

                          {
                              Text = m.MenuName,
                              Value = m.MenuID.ToString(),
                          }).ToList();
            mnList.Insert(0, new SelectListItem()
            {
                Text="----Select----",
                Value = string.Empty
            });
            ViewBag.mnList = mnList; return View(mn);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Menu menu)
        {
            if (ModelState.IsValid) {
                _context.Menus.Update(menu);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(menu);
        }
    }
}
