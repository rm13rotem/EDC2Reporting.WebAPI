using Microsoft.VisualStudio.TestTools.UnitTesting;
using MainStaticMaintainableEntities;
using System;
using System.Collections.Generic;
using System.Text;
using MainStaticMaintainableEntities.SiteAssembly;
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
            var myList = new List<PersistantEntity>() {
                new Site() { Id = 1, Name ="abc"   },
                new Doctor() {Id = 2, Name = "Jaun"}
            };

            //Act 
            PersistantRepository persistantRepository = new PersistantRepository();
            var sites = persistantRepository.GetAll<Site>(myList);

            Assert.IsTrue(sites.Count() == 1);
        }

        [TestMethod()]
        public void Upsert_NewModule_ReturnsTrue()
        {
            // Arrange
            var myList = new List<PersistantEntity>() {
                new Site() { Name ="abc"   },
                new Doctor() {Name = "Jaun"},
                new Module() {}
            };

            //Act 
            PersistantRepository persistantRepository = new PersistantRepository();
            var sites = persistantRepository.GetAll<Site>(myList);

            Assert.IsTrue(sites.Count() == 1);
        }
    }
}