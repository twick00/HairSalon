using HairSalon;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
    public class Client
    {
        private int _id;
        private string _name;
        public int Id { get => _id;}
        private int _Id { set => _id = value;}
        public string Name { get => _name;}
        private string _Name { set => _name = value; }
        public Client(string name, int id, bool save = false)
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
            cmd.CommandText = @"INSERT INTO client (name) VALUES (@clientName);";
            MySqlParameter clientName = new MySqlParameter();
            clientName.ParameterName = "@clientName";
            clientName.Value = this.Name;
            cmd.Parameters.Add(clientName);
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
            cmd.CommandText = @"DELETE FROM `client`;";
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
            cmd.CommandText = @"DELETE FROM `client`  WHERE id = @thisId;";
            MySqlParameter thisId = new MySqlParameter("@thisId", this._id);
            cmd.Parameters.Add(thisId);
            cmd.ExecuteNonQuery();
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
        }

        public static Client FindClient(int id)
        {
            int ID = 0;
            string name = "";
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM client WHERE id = @ClientId;";
            MySqlParameter clientId = new MySqlParameter("@ClientId", id);
            cmd.Parameters.Add(clientId);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                ID = rdr.GetInt32(0);
                name = rdr.GetString(1);
            }
            Client newClient = new Client(name, id);
            return newClient;
        }
        public static void ChangeThisStylist(int clientID, int stylistID)
        {
            System.Console.WriteLine("---------------------------------------");
            System.Console.WriteLine(clientID);
            System.Console.WriteLine(stylistID);
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO client_stylist (client_id, stylist_id) VALUES (@clientID, @newStylist) ON DUPLICATE KEY UPDATE stylist_id = @newStylist;";
            MySqlParameter newStylistId = new MySqlParameter("@newStylist", stylistID);
            MySqlParameter clientId = new MySqlParameter("@clientId", clientID);
            cmd.Parameters.Add(newStylistId);
            cmd.Parameters.Add(clientId);
            cmd.ExecuteNonQuery();
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
        }
        public void NewClientStylistRel(int stylistId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO `client_stylist` (`client_id`, `stylist_id`) VALUES (@clientId, @newStylist);";
            MySqlParameter newStylistId = new MySqlParameter("@newStylist", stylistId);
            MySqlParameter clientId = new MySqlParameter("@clientId", _id);
            cmd.Parameters.Add(newStylistId);
            cmd.Parameters.Add(clientId);
            cmd.ExecuteNonQuery();
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
        }
        public List<Stylist> GetStylist() //Referenced in Details.cshtml
        {
            List<Stylist> newStylists = new List<Stylist>{};
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT stylist.* FROM stylist JOIN client_stylist ON (stylist.id = client_stylist.stylist_id) JOIN client ON (client_stylist.client_id = client.id) WHERE client_stylist.client_id = @ThisID;";
            MySqlParameter thisId = new MySqlParameter();
            thisId.ParameterName = "@ThisID";
            thisId.Value = this._id;
            cmd.Parameters.Add(thisId);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                Stylist newStylist = new Stylist(name, id);
                newStylists.Add(newStylist);
            }
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
            return newStylists;
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
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
            return  allClients;
        }
    }
}