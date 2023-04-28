using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SOZ_project.Enums;
using SOZ_project.Models;
using SOZ_project.Queries;

namespace SOZ_project.Controllers
{
    public class MissingPersonsListController : BaseController
    {
        
        private readonly ReportsDbContext _context;

        public MissingPersonsListController(ReportsDbContext context)
        {
            _context = context;
            
        }

        public async Task<ActionResult> Index()
        {
            var reports = (await Mediator.Send(new GetReports.Query())).Value;
            return View(reports);
        }

       
        public ActionResult Details(int id)
        {
            var report = Mediator.Send(new GetReport.Query{ Id=id}).Result.Value;
            return View(report);
        }

        [Authorize(Roles ="admin")]
        public ActionResult Create()
        {
            return View(new ReportModel());
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ReportModel model)
        {

            await Mediator.Send(new CreateReport.Query { report = model });
            return RedirectToAction(nameof(Index));  
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(int id)
        {
            var report = await _context.Reports.FindAsync(id);
            return View(report);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ReportModel reportModel)
        {
            await Mediator.Send(new UpdateReport.Query { report = reportModel });
            return RedirectToAction(nameof(Index));
  
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var report = Mediator.Send(new GetReport.Query { Id = id }).Result.Value;
            return View(report);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            await Mediator.Send(new DeleteReport.Query { Id = id });
            return RedirectToAction(nameof(Index));
           
        }
    }
}
