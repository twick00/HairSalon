using HairSalon;
using System.Linq;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
    public class Client_Stylist
    {
        private static Dictionary<int,List<int>> _stylist_client;
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
        public static List<Stylist> FindStylistByClientId(int ClientId)
        {
            System.Console.WriteLine("HERE1");
            GetClient_StylistRel();
            List<Stylist> newList = new List<Stylist>{};
            foreach(KeyValuePair<int, List<int>> relDict in _stylist_client)
            {
                if (relDict.Value.Contains(ClientId))
                {
                    newList.Add(Stylist.FindStylist(relDict.Key));
                }
            }
            return newList;
        } 
        public static List<Client> FindClientByStylistId(int StylistId)
        {
            System.Console.WriteLine("HERE2");
            GetClient_StylistRel();
            List<Client> newClientList = new List<Client>{};
            if(_stylist_client.ContainsKey(StylistId))
            {
                foreach(var temp in _stylist_client[StylistId])
                {
                    newClientList.Add(Client.FindClient(temp));
                }
                return newClientList;
            }   
            else {
                return null;
            }
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
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
            _stylist_client = newClient_Stylist;
            return newClient_Stylist;
        }
    }
}