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
        public int Id { get => _id; }
        private int _Id { set => _id = value; }
        public string Name { get => _name;}
        private string _Name { set => _name = value;}

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
            Client_Stylist.SetStylist(allStylists);
            return allStylists;
        }
    }
}