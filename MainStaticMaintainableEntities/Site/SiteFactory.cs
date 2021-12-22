using DataServices.Interfaces;
using DataServices.Providers;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace MainStaticMaintainableEntities.SiteAssembly
{
    public class SiteFactory : ISiteFactory
    {
        private IRepository<Doctor> doctorRepository;
        
        public SiteFactory(RepositoryOptions options)
        {
            RepositoryLocator<Doctor> repositoryLocator = new RepositoryLocator<Doctor>();
            doctorRepository = repositoryLocator.GetRepository(options.RepositoryType);
        }

        public Doctor LoadDoctorById(int siteManagerId)
        {
            var doctor = doctorRepository.GetById(siteManagerId);
            return doctor;
        }
    }
}
