using HairSalon;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
    public class Client
    {
        private int _id;
        private string _name;
        private static List<Client> _allClients;
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

        public static Client FindClient(int id)
        {
            foreach(Client client in _allClients)
            {
                if (client._id == id)
                {
                    return client;
                }
            }
            return null;
        }
        public static void ChangeThisStylist(int clientID, int stylistID)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE client_stylist SET stylist_id = @newStylist WHERE client_id = @clientID;";
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
        public List<Stylist> GetStylist()
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
            _allClients = allClients;
            Client_Stylist.SetClient(allClients);
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
            return  allClients;
        }
    }
}