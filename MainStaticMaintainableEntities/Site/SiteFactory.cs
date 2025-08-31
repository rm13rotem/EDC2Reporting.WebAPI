using DataServices.Interfaces;
using DataServices.Providers;
using DataServices.SqlServerRepository.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace MainStaticMaintainableEntities.SiteAssembly
{
    public class SiteFactory : ISiteFactory
    {
        //private IRepository<Investigator> doctorRepository;
        private IRepository<Country> countryRepository;
        private IRepository<City> cityRepository;

        public SiteFactory(RepositoryOptions options)
        {
            //RepositoryLocator<Investigator> repositoryLocator = RepositoryLocator<Investigator>.Instance;
            //doctorRepository = repositoryLocator.GetRepository(options.RepositoryType);
            RepositoryLocator<Country> countryRepositoryLocator = RepositoryLocator<Country>.Instance;
            countryRepository = countryRepositoryLocator.GetRepository(options.RepositoryType);
            RepositoryLocator<City> cityRepositoryLocator = RepositoryLocator<City>.Instance;
            cityRepository = cityRepositoryLocator.GetRepository(options.RepositoryType);
        }

        public Investigator LoadDoctorById(int siteManagerId)
        {
            Investigator doctor = null; // TODO - doctorRepository.GetById(siteManagerId);
            return doctor;
        }

        public City LoadCityById(int siteManagerId)
        {
            var city = cityRepository.GetById(siteManagerId);
            return city;
        }
        public Country LoadCountryById(int siteManagerId)
        {
            var country = countryRepository.GetById(siteManagerId);
            return country;
        }
    }
}
