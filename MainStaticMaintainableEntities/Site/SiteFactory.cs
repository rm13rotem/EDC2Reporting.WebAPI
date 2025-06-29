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
        private IRepository<Investigator> doctorRepository;
        private IRepository<Country> countryRepository;
        private IRepository<City> cityRepository;

        public SiteFactory(RepositoryOptions options)
        {
            RepositoryLocator<Investigator> repositoryLocator = new RepositoryLocator<Investigator>();
            doctorRepository = repositoryLocator.GetRepository(options.RepositoryType);
            RepositoryLocator<Country> countryRepositoryLocator = new RepositoryLocator<Country>();
            countryRepository = countryRepositoryLocator.GetRepository(options.RepositoryType);
            RepositoryLocator<City> cityRepositoryLocator = new RepositoryLocator<City>();
            cityRepository = cityRepositoryLocator.GetRepository(options.RepositoryType);
        }

        public Investigator LoadDoctorById(int siteManagerId)
        {
            var doctor = doctorRepository.GetById(siteManagerId);
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
