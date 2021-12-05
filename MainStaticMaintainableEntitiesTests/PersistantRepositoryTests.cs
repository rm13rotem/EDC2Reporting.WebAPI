using Microsoft.VisualStudio.TestTools.UnitTesting;
using MainStaticMaintainableEntities;
using System;
using System.Collections.Generic;
using System.Text;
using MainStaticMaintainableEntities.SiteAssembly;
using System.Linq;
using MainStaticMaintainableEntities.ModuleAssembly;
using DataServices.Interfaces;
using DataServices.Providers;
using DataServices.SqlServerRepository;

namespace MainStaticMaintainableEntities.Tests
{
    [TestClass()]
    public class PersistantRepositoryTests
    {
        [TestMethod()]
        public void GetAllTest_GetsValidList_ReturnsAListOfSites()
        {
            // Arrange
            var myList = new List<IPersistentEntity>() {
                new Site() { Id = 1, Name ="abc"   },
                new Doctor() {Id = 2, Name = "Jaun"}
            };

            //Act 
            FromDbRepository<Site> persistantRepository = new FromDbRepository<Site>(new EdcDbContext());
            var sites = persistantRepository.GetAll();
            
            Assert.IsTrue(sites.Count() == 1);
        }

        [TestMethod()]
        public void Upsert_NewModule_ReturnsTrue()
        {
            // Arrange
            var myList = new List<IPersistentEntity>() {
                new Site() { Name ="abc"   },
                new Doctor() {Name = "Jaun"},
                new Module() { Name = "Inclusion/Exclusion"}
            };
            var connStr = "Data Source=LAPTOP-6ORP7UKE\\SQLEXPRESS;Database=EDC_Dev;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            //Act 
            FromDbRepository<Site> persistantRepository = new FromDbRepository<Site>(new EdcDbContext(connStr));
            var sites = persistantRepository.GetAll();

            Assert.IsTrue(sites.Count() >= 1);
        }
    }
}