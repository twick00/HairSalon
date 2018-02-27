using HairSalon;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
    public class Client
    {
        private int _id = 0;
        private string _name;
        private static List<Client> _allClients;
        private List<Stylist> _linkedStylists;
        public int Id { get => _id;}
        private int _Id { set => _id = value;}
        public string Name { get => _name;}
        private string _Name { set => _name = value; }

        public Client(string name, bool save = false)
        {
            this._Name = name;
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

        public List<Stylist> linkedStylists()
        {
            _linkedStylists = Client_Stylist.FindStylistByClientId(this._id);//Issue before here
            return _linkedStylists;
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
                Client newClient = new Client(name);
                allClients.Add(newClient);
            }
            _allClients = allClients;
            Client_Stylist.SetClient(allClients);
            return  allClients;
        }
    }
}