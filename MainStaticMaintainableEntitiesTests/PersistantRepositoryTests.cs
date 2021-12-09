using DataServices.Interfaces;
using DataServices.Providers;
using DataServices.SqlServerRepository;
using DataServices.SqlServerRepository.Models;
using MainStaticMaintainableEntities.ModuleAssembly;
using MainStaticMaintainableEntities.SiteAssembly;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
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
                    JsonValue=JsonConvert.SerializeObject(new Doctor() {Id = 2, Name = "Jaun"}) }
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