using Microsoft.VisualStudio.TestTools.UnitTesting;
using MainStaticMaintainableEntities.VisitGroup;
using System;
using System.Collections.Generic;
using System.Text;
using MainStaticMaintainableEntities.VisitAssembly;

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
            vg.Visits.Add(new Visit());

            Assert.IsTrue(vg.Visits.Count > 0); // TODO : add
        }
    }
}