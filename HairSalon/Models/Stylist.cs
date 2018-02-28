using HairSalon;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
    public class Stylist
    {
        private int _id;
        private string _name;
        private static List<Stylist> _allStylists;
        private List<Client> _linkedClients;
        public int Id { get => _id; }
        private int _Id { set => _id = value; }
        public string Name { get => _name; }
        private string _Name { set => _name = value; }

        public Stylist(string name, int id = 0, bool save = false)
        {
            this._Name = name;
            this._Id = id;
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
            MySqlParameter stylistName = new MySqlParameter();
            stylistName.ParameterName = "@stylistName";
            stylistName.Value = this.Name;
            cmd.Parameters.Add(stylistName);
            cmd.ExecuteNonQuery();
            _Id = (int) cmd.LastInsertedId;
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
            foreach(Stylist stylist in _allStylists)
            {
                if (stylist._id == id)
                {
                    return stylist;
                }
            }
            return null;
        }
        public List<Client> GetClients()
        {
            List<Client> newClients = new List<Client>{};
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT client.* FROM client JOIN client_stylist ON (client.id = client_stylist.client_id) JOIN stylist ON (client_stylist.stylist_id = stylist.id) WHERE client_stylist.stylist_id = @ThisID;";
            MySqlParameter thisId = new MySqlParameter();
            thisId.ParameterName = "@ThisID";
            thisId.Value = this._id;
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
            _allStylists = allStylists;
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
            return allStylists;
        }
    }
}