using MainStaticMaintainableEntities.VisitAssembly;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MainStaticMaintainableEntities.VisitGroupAssembley;

namespace MainStaticMaintainableEntities.VisitGroupAssembley1.Tests
{
    [TestClass()]
    public class VisitGroupTests
    {
        [TestMethod()]
        public void AddVisitByIdTest()
        {
            // Arrange
            VisitGroup vg = new VisitGroup();
            
            // Act
            vg.Visits.Add(new Visit());

            Assert.IsTrue(vg.Visits.Count > 0); // TODO : add
        }
    }
}