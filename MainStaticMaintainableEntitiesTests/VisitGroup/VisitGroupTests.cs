using Microsoft.VisualStudio.TestTools.UnitTesting;
using MainStaticMaintainableEntities.VisitGroup;
using System;
using System.Collections.Generic;
using System.Text;

namespace MainStaticMaintainableEntities.VisitGroup.Tests
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
            vg.AddVisitById(1);

            Assert.IsTrue(vg.Visits.Count > 0); // TODO : add
        }
    }
}