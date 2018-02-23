using HairSalon;
using System.Linq;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
    public class Client_Stylist
    {
        private static Dictionary<int,List<int>> _client_stylist;
        private static List<Stylist> _allStylists;
        private static List<Client> _allClients;
        public static void SetClient( List<Client> allClients)
        {
            _allClients = allClients;
        }
        public static void SetStylist(List<Stylist> allStylists)
        {
            _allStylists = allStylists;
        }
        public static List<Stylist> GetStylists()
        {
            return _allStylists;
        }
        public static List<Client> GetClients()
        {
            return _allClients;
        }
         public static Dictionary<int,List<int>> GetClient_StylistRel()
        {
            Dictionary<int,List<int>> newClient_Stylist = new Dictionary<int, List<int>>{};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM client_stylist;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int stylist_id = rdr.GetInt32(0);
                int client_id = rdr.GetInt32(1);
                if (!newClient_Stylist.ContainsKey(key:stylist_id))
                {
                    newClient_Stylist.Add(stylist_id, new List<int>{client_id});
                }
                else
                {
                    newClient_Stylist[stylist_id].Add(client_id);
                }
            }
            _client_stylist = newClient_Stylist;
            return newClient_Stylist;
        }
    }
}