using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using HairSalon.Models;
using System;

namespace HairSalon.Tests
{
    [TestClass]
    public class ClientTest : IDisposable
    {
        public void DatabaseTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=3306;database=tyler_wickline_test;";
        }

        public void Dispose()
        {
            Client.DeleteAll();
            Stylist.DeleteAll();
        }

        [TestMethod]
        public void GetAllClients_DatabaseEmpty_0()
        {
            int result = Client.GetAllClients().Count;
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GetAllStylists_DatabaseEmpty_0()
        {
            int result = Stylist.GetAllStylist().Count;
            Assert.AreEqual(0, result);
        }

        
    }
}
