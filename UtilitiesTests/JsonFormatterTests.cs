using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using MainStaticMaintainableEntities;
using MainStaticMaintainableEntities.Site;

namespace Utilities.Tests
{
    [TestClass()]
    public class JsonFormatterTests
    {
        [TestMethod()]
        public void SerializeTest_ValidObject_ReturnsExpectedString()
        {
            // Arrange 
            var obj = new { Test = "MY" };

            // Act
            string myJson = JsonFormatter.Serialize(obj);
            string exp = "{\r\n  \"Test\": \"MY\"\r\n}";
            Assert.IsTrue(myJson == exp);
        }

        [TestMethod]
        public void SerializeTest_ValidDbEntity_ReturnsExpectedString()
        {
            // Arrange 
            var obj = new Site() { EntityName = "site", GuidId = "a"    };

            // Act
            string myJson = JsonFormatter.Serialize(obj);
            string exp = "{\r\n  \"Id\": 0,\r\n  \"GuidId\": \"a\",\r\n  \"EntityName\": \"site\",\r\n  \"CreateDate\": \"0001-01-01T00:00:00\"\r\n}";
            
            // Assert
            Assert.IsTrue(myJson == exp);
        }


        [TestMethod]
        public void DeserializeTest_ValidDbEntity_ReturnsExpectedString()
        {
            // Arrange 
            string exp = "{\r\n  \"Id\": 0,\r\n  \"GuidId\": \"a\",\r\n  \"EntityName\": \"site\",\r\n  \"CreateDate\": \"0001-01-01T00:00:00\"\r\n}";
            
            // Act
            var mySite = JsonFormatter.Deserialize<Site>(exp);
            var obj = new Site() { EntityName = "site", GuidId = "a" };

            // Assert
            Assert.IsTrue(mySite.GuidId == obj.GuidId);
            Assert.IsTrue(mySite.EntityName == obj.EntityName);
            Assert.IsTrue(mySite.Id == 0);
        }
    }
}