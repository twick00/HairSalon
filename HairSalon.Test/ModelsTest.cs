using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using HairSalon.Models;
using MySql.Data.MySqlClient;
using System;
namespace HairSalon.Test
{
    [TestClass]
    public class ClientTest : IDisposable
    {
        public ClientTest()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=tyler_wickline_test;";
        }
        public void Dispose()
        {
            Client.DeleteAll();
            Stylist.DeleteAll();
        }

        [TestMethod]
        public void ClientSave_CheckQuantity_ReturnsTrue()
        {
            int id = 0;
            string name = "";
            Client newClient = new Client("Joe Schmo", id, true);
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM client WHERE name = 'Joe Schmo';";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                name = rdr.GetString(1);
                id = rdr.GetInt32(0);
            }

            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
            Assert.AreEqual(id, newClient.Id);
            Assert.AreEqual(name, newClient.Name);
        }
        [TestMethod]
        public void StylistSaveThenDelete_CheckQuantity_ReturnsNone()
        {
            int id = 0;
            string name = "";
            Stylist newStylist = new Stylist("Joe Schmo", id, true);
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM stylist WHERE name = 'Joe Schmo';";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                name = rdr.GetString(1);
                id = rdr.GetInt32(0);
            }

            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
            int stylistQuantity = (Stylist.GetAllStylist()).Count;
            Assert.AreEqual(1, stylistQuantity);
            Assert.AreEqual(id, newStylist.Id);
            Assert.AreEqual(name, newStylist.Name);
            Stylist.DeleteAll();
            stylistQuantity = (Stylist.GetAllStylist()).Count;
            Assert.AreEqual(0, stylistQuantity);
        }
    }
}