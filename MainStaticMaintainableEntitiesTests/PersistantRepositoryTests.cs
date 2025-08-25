using DataServices.Interfaces;
using DataServices.Providers;
using DataServices.SqlServerRepository;
using DataServices.SqlServerRepository.Models;
using DataServices.SqlServerRepository.Models.CrfModels;
using MainStaticMaintainableEntities.SiteAssembly;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MainStaticMaintainableEntities.Tests
{
    [TestClass()]
    public class PersistantRepositoryTests
    {
        [TestMethod()]
        public void GetAllTest_GetsValidList_ReturnsAListOfSites()
        {
            // Arrange
            var myList = new List<PersistentEntity>() {
                new PersistentEntity() { Id = 1, Name ="abc"  , EntityName="Site", 
                    JsonValue=JsonConvert.SerializeObject(new Site() { Id=2,  Name="HYMC" }) },
                new PersistentEntity() { Id = 2, Name ="abc"  , EntityName="Doctor",
                    JsonValue=JsonConvert.SerializeObject(new Investigator() {Id = Guid.NewGuid().ToString(), FirstName = "Jaun"}) }
            };

            //Act 
            IEnumerable<PersistentEntity> iList = myList;
            FromDbRepository<Site> persistantRepository = new FromDbRepository<Site>(iList);
            var sites = persistantRepository.GetAll();
            
            Assert.IsTrue(sites.Count() == 1);
        }

        [TestMethod()]
        public void Upsert_NewModule_ReturnsTrue()
        {
            // Arrange
            //var myList = new List<CrfPage>() { 
            //    new Site() { Name ="abc"   },
            //    new Investigator() {FirstName = "Jaun"},
            //    new CrfPage() { Name = "Inclusion/Exclusion"}
            //};
            var connStr = "Data Source=LAPTOP-6ORP7UKE\\SQLEXPRESS;Database=EDC_Dev;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            //Act 
            FromDbRepository<Site> persistantRepository = new FromDbRepository<Site>(new EdcDbContext(connStr));
            var sites = persistantRepository.GetAll();

            Assert.IsTrue(sites.Count() >= 1);
        }
    }
}