using HairSalon;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
    public class Stylist
    {
        private int _id;
        private string _name;
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
            MySqlParameter stylistName = new MySqlParameter();
            stylistName.ParameterName = "@stylistName";
            stylistName.Value = this._name;
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
            return allStylists;
        }
    }
}