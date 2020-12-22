using MainStaticMaintainableEntities.Site;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            string myJson = JsonFormatter.ToJson<object>(obj);
            string exp = "{\"$type\":\"<>f__AnonymousType0`1[[System.String, System.Private.CoreLib]], UtilitiesTests\",\"Test\":\"MY\"}";
            Assert.IsTrue(myJson == exp);
        }

        [TestMethod]
        public void SerializeTest_ValidDbEntity_ReturnsExpectedString()
        {
            // Arrange 
            var obj = new Site() { EntityName = "site", GuidId = "a"    };

            // Act
            string myJson = JsonFormatter.ToJson<Site>(obj);
            string exp = 
            "{\"$type\":\"MainStaticMaintainableEntities.Site.Site, MainStaticMaintainableEntities\",\"Id\":0,\"GuidId\":\"a\",\"EntityName\":\"site\",\"CreateDate\":\"0001-01-01T00:00:00\"}";
            // Assert
            Assert.IsTrue(myJson == exp);
        }


        [TestMethod]
        public void SerializeTest_ValidDbEntity_FlipAndBackReturnsExpectedObject()
        {
            // Arrange 
            var obj = new Site() { EntityName = "site", GuidId = "a" };

            // Act
            string myJson = JsonFormatter.ToJson<Site>(obj);
            var exp = JsonFormatter.FromJson<Site>(myJson);

            // Assert
            Assert.IsTrue(obj.EntityName == exp.EntityName);
            Assert.IsTrue(obj.GuidId == exp.GuidId);
        }


        [TestMethod]
        public void DeserializeTest_ValidDbEntity_ReturnsExpectedString()
        {
            // Arrange 
            string exp = "{\r\n  \"Id\": 0,\r\n  \"GuidId\": \"a\",\r\n  \"EntityName\": \"site\",\r\n  \"CreateDate\": \"0001-01-01T00:00:00\"\r\n}";
            
            // Act
            var mySite = JsonFormatter.FromJson<Site>(exp);
            var obj = new Site() { EntityName = "site", GuidId = "a" };

            // Assert
            Assert.IsTrue(mySite.GuidId == obj.GuidId);
            Assert.IsTrue(mySite.EntityName == obj.EntityName);
            Assert.IsTrue(mySite.Id == 0);
        }
    }
}