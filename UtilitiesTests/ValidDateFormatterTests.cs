using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities.Tests
{
    [TestClass()]
    public class ValidDateFormatterTests
    {
        [TestMethod()]
        public void DeserializeFrom_N_DaysTest_From1_ShouldReturn02Jan1970()
        {
            // Arrange
            var expected = new DateTime(1970, 1, 2);

            // Act
            var observed = ValidDateFormatter.DeserializeFrom_N_Days("1");

            // Assert
            Assert.IsTrue(expected == observed);
        }

        [TestMethod()]
        public void SerializeToDaysFrom01Jan1970Test_Get02Jan1970_return1()
        {
            // Assert
            Assert.IsTrue(ValidDateFormatter.SerializeToDaysFrom01Jan1970(new DateTime(1970, 1, 2)) == "1");
        }

        public void SerializeToDaysFrom01Jan1970Test_GetTommorow_returnnull()
        {
            // Assert
            Assert.IsTrue(ValidDateFormatter.SerializeToDaysFrom01Jan1970(DateTime.Now.AddDays(1)) == null);
        }

        [TestMethod()]
        public void SerializeToDaysFrom01Jan1970Test_Get02Jan2970_returnNULL()
        {
            // Assert
            Assert.IsTrue(ValidDateFormatter.SerializeToDaysFrom01Jan1970(new DateTime(2970, 1, 2)) == null);
        }

        [TestMethod()]
        public void SerializeToDaysFrom01Jan1970Test_Get02Jan1870_returnNULL()
        {
            // Assert
            Assert.IsTrue(ValidDateFormatter.SerializeToDaysFrom01Jan1970(new DateTime(1870, 1, 2)) == null);
        }
    }
}