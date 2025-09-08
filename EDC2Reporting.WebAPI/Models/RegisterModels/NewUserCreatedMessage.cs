using DataServices.SqlServerRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDC2Reporting.WebAPI.Models.RegisterModels
{
    public class NewUserCreatedMessage
    {
        public Investigator Invite { get; set; }
        public string DestinationEmail { get; set; }

        public readonly string From = "Clinical Trail Auto Messaging Service";
        public readonly string Subject = "new user created ";

        public string Body { get
            {
                if (Invite == null)
                    return "Error. Msg invalid";
                // else
                return string.Format("User {0} created.\nEmail {1}",
                    Invite.FirstName + " " + Invite.LastName,
                    Invite.Email);
            }
        }
    }
}
