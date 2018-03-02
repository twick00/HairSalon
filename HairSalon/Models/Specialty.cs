using HairSalon;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
    public class Specialty
    {
        private int _id;
        private string _specialtyName;
        public int Id {get => _id;}
        public string SpecialtyName {get=> _specialtyName;}
        public Specialty (string SpecialtyName, int Id, bool save = false)
        {
            this._specialtyName = SpecialtyName;
            this._id = Id;
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
            cmd.CommandText = @"INSERT INTO specialty (specialty_name) VALUES (@SpecialtyName);";
            MySqlParameter specialtyName = new MySqlParameter("@SpecialtyName", this._specialtyName);
            cmd.Parameters.Add(specialtyName);
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
            cmd.CommandText = @"DELETE FROM `specialty`;";
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
            cmd.CommandText = @"DELETE FROM `specialty` WHERE id = @thisId;";
            MySqlParameter thisId = new MySqlParameter("@thisId", this._id);
            cmd.Parameters.Add(thisId);
            cmd.ExecuteNonQuery();
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
        }
        public static Specialty FindSpecialty(int id)
        {
            int ID = 0;
            string specialtyName = "";
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM specialty WHERE id = @SpecialtyId;";
            MySqlParameter specialtyId = new MySqlParameter("@SpecialtyId", id);
            cmd.Parameters.Add(specialtyId);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                ID = rdr.GetInt32(0);
                specialtyName = rdr.GetString(1);
            }
            Specialty newSpecialty = new Specialty(specialtyName, ID);
            return newSpecialty;
        }
        public static List<Specialty> GetAllSpecialties()
        {
            List<Specialty> allSpecialties = new List<Specialty>{};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM specialty;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                string specialtyName = rdr.GetString(1);
                int id = rdr.GetInt32(0);
                Specialty newSpecialty = new Specialty(specialtyName, id);
                allSpecialties.Add(newSpecialty);
            }
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
            return allSpecialties;
        }
        public void ChangeSpecialtyName(string newSpecialtyName)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE specialty SET specialty_name = @SpecialtyNewName WHERE id = @SpecialtyId;";
            MySqlParameter SpecialtyName = new MySqlParameter("@SpecialtyNewName", newSpecialtyName);
            MySqlParameter SpecialtyId = new MySqlParameter("@SpecialtyId", this._id);
            cmd.Parameters.Add( SpecialtyName);
            cmd.Parameters.Add(SpecialtyId);
            cmd.ExecuteNonQuery();
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
        }
        public void NewSpecialtyStylistRel(int stylistId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO specialty_stylist (specialty_id, stylist_id) VALUES (@SpecialtyId, @StylistId);";
            MySqlParameter SpecialtyId = new MySqlParameter("@SpecialtyId", this._id);
            MySqlParameter StylistId = new MySqlParameter("@StylistId", stylistId);
            cmd.Parameters.Add(SpecialtyId);
            cmd.Parameters.Add(StylistId);
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
            cmd.CommandText = @"SELECT stylist.* FROM stylist JOIN specialty_stylist ON (stylist.id = specialty_stylist.stylist_id) JOIN specialty ON (specialty_stylist.specialty_id = specialty.id) WHERE specialty.id = @ThisId;";
            MySqlParameter thisId = new MySqlParameter("@ThisId",this._id);
            cmd.Parameters.Add(thisId);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
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
    }
}