using HairSalon;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
    public class Stylist
    {
        private int _id;
        private string _name;
        public int Id { get => _id; }
        public string Name { get => _name; }
        public Stylist(string name, int id = 0, bool save = false)
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
            cmd.CommandText = @"INSERT INTO stylist (name) VALUES (@stylistName);";
            MySqlParameter stylistName = new MySqlParameter("@stylistName", this._name);
            cmd.Parameters.Add(stylistName);
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
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM `stylist`;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
        }
        public void DeleteThis()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM `stylist` WHERE id = @thisId;";
            MySqlParameter thisId = new MySqlParameter("@thisId", this._id);
            cmd.Parameters.Add(thisId);
            cmd.ExecuteNonQuery();
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
        }

        public static Stylist FindStylist(int id)
        {
            int ID = 0;
            string name = "";
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM stylist WHERE id = @StylistId;";
            MySqlParameter stylistId = new MySqlParameter("@StylistId", id);
            cmd.Parameters.Add(stylistId);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                ID = rdr.GetInt32(0);
                name = rdr.GetString(1);
            }
            Stylist newStylist = new Stylist(name, ID);
            return newStylist;
        }
        public List<Client> GetClients()//Referenced in Index.cshtml
        {
            List<Client> newClients = new List<Client>{};
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT client.* FROM client JOIN client_stylist ON (client.id = client_stylist.client_id) JOIN stylist ON (client_stylist.stylist_id = stylist.id) WHERE client_stylist.stylist_id = @ThisID;";
            MySqlParameter thisId = new MySqlParameter("@ThisID", this._id);
            cmd.Parameters.Add(thisId);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                Client newClient = new Client(name, id);
                newClients.Add(newClient);
            }
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
            return newClients;
        }
        public static List<Stylist> GetAllStylist()
        {
            List<Stylist> allStylists = new List<Stylist> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM stylist;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                string name = rdr.GetString(1);
                int id = rdr.GetInt32(0);
                Stylist newStylist = new Stylist(name, id);
                allStylists.Add(newStylist);
            }
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
            return allStylists;
        }
        public void ChangeName(string newName)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE stylist SET name = @StylistNewName WHERE id = @StylistId;";
            MySqlParameter stylistName = new MySqlParameter("@StylistNewName", newName);
            MySqlParameter stylistId = new MySqlParameter("@StylistId", this._id);
            cmd.Parameters.Add(stylistName);
            cmd.Parameters.Add(stylistId);
            cmd.ExecuteNonQuery();
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
        }
    }
}