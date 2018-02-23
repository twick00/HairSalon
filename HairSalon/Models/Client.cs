using HairSalon;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
    public class Client
    {
        private int _id;
        private string _name;
        public Client(string name, int id = 0, bool save = false)
        {
            this._name = name;
            this._id = id;
            if (save)
            {
            Save();
            }
        }
        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO client (name) VALUES (@clientName);";
            MySqlParameter clientName = new MySqlParameter();
            clientName.ParameterName = "@clientName";
            clientName.Value = this._name;
            cmd.Parameters.Add(clientName);
            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
        }
        public static void DeleteAll()
        {
            
        }
        public static List<Client> GetAllClients()
        {
            List<Client> allClients = new List<Client> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM client;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                string name = rdr.GetString(1);
                int id = rdr.GetInt32(0);
                Client newClient = new Client(name, id);
                allClients.Add(newClient);
            }
            return  allClients;
        }
    }
}