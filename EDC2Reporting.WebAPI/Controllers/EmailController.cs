using DataServices.SqlServerRepository;
using DataServices.SqlServerRepository.Models;
using Edc2Reporting.AuthenticationStartup.Areas.Identity;
using Edc2Reporting.AuthenticationStartup.Data;
using EDC2Reporting.WebAPI.Models.LoginModels;
using MailClientLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace EDC2Reporting.WebAPI.Controllers
{
    [Authorize(Roles ="Admin")]
    public class EmailController : Controller
    {
        private readonly EdcDbContext _context;
        private readonly AppIdentityDbContext _identityDb;
        private readonly IMailClientSender _sender;
        public EmailController(EdcDbContext context, IMailClientSender mailClient, AppIdentityDbContext identityDb)
        {
            _sender = mailClient;
            _context = context;
            _identityDb = identityDb;
        }

        // 1. Index - with filters
        public async Task<IActionResult> Index(string toFilter, bool? isSent)
        {
            var query = _context.Emails.AsQueryable();

            if (!string.IsNullOrWhiteSpace(toFilter))
                query = query.Where(e => e.To.Contains(toFilter));

            if (isSent.HasValue)
                query = query.Where(e => e.IsSent == isSent.Value);

            var emails = await query.ToListAsync();
            return View(emails);
        }

        // 2. Create (GET)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // 2. Create (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmailModel model)
        {
            if (!string.IsNullOrEmpty(model.To)
                && ModelState.IsValid)
            {
                _sender.TryInsertIntoQueue(model);
                _sender.SendAllPendingEmails();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // 3. Send pending emails
        public IActionResult SendAll()
        {
            _sender.SendAllPendingEmails();
            return RedirectToAction(nameof(Index));
        }

        // 3. Send hardCoded email to Matan
        public IActionResult SendMatan()
        {
            var model = new EmailModel
            {
                To = "rm13rotem@gmail.com"
            };
            model.Subject = "פרוטוקולים של ועד מתן והודעות";
            var html = System.IO.File.ReadAllText(@"C:\Users\rm13r\Downloads\LetterToMatan.htm");
            model.Body = html;
            model.IsBodyHtml = true;
            model.From = model.To;
            model.CC = "rm13rotem@gmail.com";
            MatanRegisteredUserLocator ll = new MatanRegisteredUserLocator(_identityDb);
            model.BCC = "aditikmeron@gmail.com"; // MatanRegisteredUserLocator.GetAllMatanEmailsAsString();

            var fileNames = new string[] { }; // { "forest1", "פרוטוקול ישיבת ועד 06-2025", "2025-07-13- פרוטוקול ישיבת ועד מתן מס 5" };
            for (int i = 0; i < fileNames.Length; i++)
            {
                fileNames[i] = @"C:\Users\rm13r\Downloads\" + fileNames[i];
            }
            if (fileNames.Length > 0)
                model.Attachment = string.Join(";", fileNames);
            _sender.TryInsertIntoQueue(model);
            _sender.SendAllPendingEmails();
            return RedirectToAction(nameof(Index));
        }

        // In Controller - -> Index should show all emails sent. (I message to all with BCC)
        // what is now Index Action will be called EmailToMatan action and GeneralEmailTest action
    }
}
