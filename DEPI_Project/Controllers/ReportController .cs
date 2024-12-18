﻿using DEPI_Project.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DEPI_Project.Controllers
{
	[Authorize(Roles = "Admin")]
	public class ReportController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ReportController(ApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<IActionResult> Index()
        {
            var appDbContextcs = _context.Reports.Include(b => b.Product).Include(x => x.ReportedBy).ToList();
            return View(appDbContextcs);

        }
        [HttpPost]
        public IActionResult ConfirmReport(int reportId)
        {
            var rep = _context.Reports.Where(x => x.Id == reportId).FirstOrDefault(); ;
            rep.Status = "Confirmed";
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult RejectReport(int reportId)
        {
            var rep = _context.Reports.Where(x => x.Id == reportId).FirstOrDefault();
            rep.Status = "Rejected";
            _context.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}