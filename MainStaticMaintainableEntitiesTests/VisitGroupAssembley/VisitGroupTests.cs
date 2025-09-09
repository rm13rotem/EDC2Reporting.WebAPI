using DataServices.SqlServerRepository.Models.VisitAssembley;
using DataServices.SqlServerRepository.Models.VisitGroup;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataServices.VisitGroupAssembley1.Tests
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