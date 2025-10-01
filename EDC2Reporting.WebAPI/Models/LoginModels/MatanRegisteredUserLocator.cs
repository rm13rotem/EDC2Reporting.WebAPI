using DataServices.SqlServerRepository;
using DataServices.SqlServerRepository.Models;
using Edc2Reporting.AuthenticationStartup.Areas.Identity;
using Edc2Reporting.AuthenticationStartup.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;

namespace EDC2Reporting.WebAPI.Models.LoginModels
{
    public class MatanRegisteredUserLocator
    {
        private static AppIdentityDbContext _identityDb;

        public MatanRegisteredUserLocator(AppIdentityDbContext identityDb )
        {
            _identityDb = identityDb;
        }
        public static string GetAllMatanEmailsAsString()
        {
            var emails = _identityDb.Users
                  .Where(i => string.IsNullOrWhiteSpace(i.Email) == false &&
                  i.QuickLookId.Contains("matan")).Select(x=>x.Email).ToArray();
            string allEmails = string.Join(";", emails);
            return allEmails;
        }
    }
}
