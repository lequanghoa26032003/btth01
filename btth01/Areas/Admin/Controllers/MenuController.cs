using btth01.Models;
using btth01.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace btth01.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class MenuController : Controller
    {
        private readonly DataContext _context;
        public MenuController(DataContext context)
        {
            _context=context;
        }
        public IActionResult Index()
        {
            var mnList = _context.Menus.OrderBy(m => m.MenuID).ToList();
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");

            return View(mnList);
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
                              Text = m.MenuName,
                              Value = m.MenuID.ToString(),
                          }).ToList();
            mnList.Insert(0, new SelectListItem()
            {
                Text="--Select----",
                Value = "0"
            });
            ViewBag.mnList = mnList;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Menu menu)
        {
            if (ModelState.IsValid) {
                _context.Menus.Add(menu);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(menu);
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