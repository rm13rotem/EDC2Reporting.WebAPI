using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDC2Reporting.WebAPI.Models;
using EDC2Reporting.WebAPI.Models.LoginModels;
using EDC2Reporting.WebAPI.Models.RegisterModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMq;
using SessionLayer;

namespace EDC2Reporting.WebAPI.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ISessionWrapper session;
        private readonly IOptionsSnapshot<RabbitMqOptions> rabbitSettings;
        private readonly IFanoutPublisher rabbitManger;
        public RegisterController(ISessionWrapper sessionWrapper, IOptionsSnapshot<RabbitMqOptions> options, IFanoutPublisher fanoutPublisher)
        {
            session = sessionWrapper;
            rabbitSettings = options;
            rabbitManger = fanoutPublisher;
        }
        public IActionResult InviteAdministrator(RegisterViewModel invite)
        {
            // If not administrator - goto home/error
            if (SessionInvalid() || session.CurrentUser.RoleName != "Administrator")
                return RedirectToAction("Error", "Home");

            // notify by alert email all existing administrators

            if (IsModelInvalid(invite))
                return View(invite);

            // else
            // 1. Create the UserAndMailHimToConfirm;
            // TODO - RegisterManager.CreateUser(invite)

            // 2.send mail to CTL
            if (!IsSuccesfulAlertCurrentUserThatNewUserCreated(invite))
            {
                return View(invite);
            }

            return RedirectToAction("Index", "Home");

        }

        public IActionResult InviteClinicalTrialLeader(QuickLookIdSetupRegister invite)
        {
            if (SessionInvalid())
            return RedirectToAction("Error", "Home");

            if ((session.CurrentUser.RoleName != "Admin" &&
                session.CurrentUser.RoleName != "ClinicalTrailLeader"))
                return RedirectToAction("Error", "Home");

            if (IsModelInvalid(invite))
                return View(invite);

            // else
            // 1. Create the UserAndMailHimToConfirm;
            // TODO - RegisterManager.CreateUser(invite)

            // 2.send mail to CTL
            if (!IsSuccesfulAlertCurrentUserThatNewUserCreated(invite))
            {
                return View(invite);
            }
                
            return RedirectToAction("Index", "Home");
        }


        public IActionResult InviteSiteManager(QuickLookIdSetupRegister invite)
        {
            // If not admin or CTL or Site Manager - got to home error

            if (IsModelInvalid(invite))
                return View(invite);

            // else
            // 1. Create the UserAndMailHimToConfirm;
            // TODO - RegisterManager.CreateUser(invite)

            // 2.send mail to CTL
            if (!IsSuccesfulAlertCurrentUserThatNewUserCreated(invite))
            {
                return View(invite);
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult InviteUser(QuickLookIdSetupRegister invite)
        {
            if (SessionInvalid())
                return RedirectToAction("Error", "Home");

            if ((session.CurrentUser.RoleName != "Admin" &&
                session.CurrentUser.RoleName != "ClinicalTrailLeader" &&
                session.CurrentUser.RoleName != "SiteManager"
                ))
                return RedirectToAction("Error", "Home");

            // Send Mail
            return View();
        }
        
        private bool IsSuccesfulAlertCurrentUserThatNewUserCreated(QuickLookIdSetupRegister invite)
        {
            var newUserMsg = new NewUserCreatedMessage()
            {
                DestinationEmail = session.CurrentUser.Email,
                Invite = invite
            };
            var jsonMessage = JsonConvert.SerializeObject(newUserMsg);
            return rabbitManger.TrySendMessage("MailOutExchange", "MailOutQueue", jsonMessage);
        }

        public bool SessionInvalid()
        {
            return session == null ||
                session.CurrentUser == null ||
                session.CurrentUser.ExperimentId == 0;
        }

        public bool IsModelInvalid(QuickLookIdSetupRegister invite)
        {
          return  string.IsNullOrEmpty(invite.Email) ||
                string.IsNullOrEmpty(invite.FirstName) ||
                string.IsNullOrEmpty(invite.LastName);
        }
    }
}
