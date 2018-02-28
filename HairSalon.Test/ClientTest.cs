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
            
        }

        [TestMethod]
        public void TestTest()
        {
            List<List<Stylist>> newListOfLists = new List<List<Stylist>>{};
             List<Client> newClientList = new List<Client>{};
             newClientList = Client.GetAllClients();
             foreach(var client in newClientList)
             {
                 newListOfLists.Add(client.GetStylist());
             }
             foreach(var List in newListOfLists)
             {
                 foreach(var stylist in List)
                 {
                     System.Console.WriteLine(stylist.Name);
                 }
             }

        }
        [TestMethod]
        public void TestClient()
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
    }
}