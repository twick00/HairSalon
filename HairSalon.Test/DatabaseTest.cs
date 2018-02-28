using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace HairSalon.Test
{

    [TestClass]
    public class DataBaseTest : IDisposable
    {
        public DataBaseTest()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=tyler_wickline_test;";
        }

        public void Dispose()
        {
            //DeleteAll(); //This will delete all the values on the table between Test runs.
        }

        public void DeleteAll()
        {
            //Stylist.DeleteAll();
            //Client.DeleteAll();
        }
        //[TestMethod]
        public void RestoreDatabase()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = 
            @"INSERT INTO `stylist` (`id`, `name`) VALUES
                (NULL, 'Max Styles'),
                (NULL, 'Daniel Walls\r\n'),
                (NULL, 'Skyler Macdonald\r\n');
            INSERT INTO `client` (`id`, `name`) VALUES
                (NULL, 'Tyler Wickline'),
                (NULL, 'Waylon Dalton\r\n'),
                (NULL, 'Marcus Cruz\r\n')";
            cmd.ExecuteNonQuery(); 
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
        }
        [TestMethod]
        public void TestConnection_ClientTest_FirstID()
        {
            string name = "";
            int id = 0;
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM client WHERE name = 'Tyler Wickline';";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                name = rdr.GetString(1);
                id = rdr.GetInt32(0);
                Stylist newStylist = new Stylist(name, id);
            }
            
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
            Assert.AreNotEqual(0, id);
            Assert.AreEqual("Tyler Wickline", name);
        }
        [TestMethod]
        public void TestConnection_StylistTest_FirstID()
        {
            string name = "";
            int id = 0;
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM stylist WHERE name = 'Max Styles';";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                name = rdr.GetString(1);
                id = rdr.GetInt32(0);
                Stylist newStylist = new Stylist(name, id);
            }
            
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
            Assert.AreNotEqual(0, id);
            Assert.AreEqual("Max Styles", name);
        }
        [TestMethod]
        public void DeleteAll_RunThisTestLast_ReturnsZero()
        {
            int id = 0;
            string name = "";
            DeleteAll();
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM stylist;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                id = rdr.GetInt32(0);
                name = rdr.GetString(1);
            }
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
            Assert.AreEqual(0, id);
            Assert.AreEqual("", name);
        }
    }
}
