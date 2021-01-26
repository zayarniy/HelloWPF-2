//Нужно скачать и установить MySQL Connector(как-то так)
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDatabase
{
    public class Data
    {
        int id;
        string data1;
        string data2;

        public int ID { get => id; }
        public string Data1 { get => data1; }
        public string Data2 { get => data2; }

        public Data(int id, string data1, string data2)
        {
            this.id = id;
            this.data1 = data1;
            this.data2 = data2;
        }

        public override string ToString()
        {
            return id + " " + data1 + " " + data2;
        }
    }

    public class Database
    {
        public string ConnectionString { get; private set; }
        public MySqlConnection Connection { get; private set; }
        public List<Data> list { get; set; } = new List<Data>();

        public void InitializeDB()
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = "mysql100.1gb.ru";
            builder.UserID = "gb_lyceum2020";
            builder.Database = "gb_lyceum2020";
            builder.Password = "c8bdd2cf56";
            ConnectionString = builder.ToString();
            Connection = new MySqlConnection(ConnectionString);
            Connection.Open();
            Console.WriteLine(ConnectionString);
        }

        public void ReadDataToList()
        {
            if (Connection == null || (!(Connection.State == System.Data.ConnectionState.Open)))
            {
                Console.WriteLine("Connection doesn't open");
                return;
            }
            string query = "SELECT * FROM systemnumber";
            MySqlCommand cmd = new MySqlCommand(query, Connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = (int)reader["id"];
                string data1 = reader["Text"].ToString();
                string data2 = reader["Code"].ToString();
                Data data = new Data(id, data1, data2);
                list.Add(data);
                Console.WriteLine(data);
            }
            reader.Close();
        }

        public void InsertData(string data1, string data2)
        {
            if (Connection == null || (!(Connection.State == System.Data.ConnectionState.Open)))
            {
                Console.WriteLine("Connection doesn't open");
                return;
            }
            string query = System.String.Format("INSERT INTO systemnumber(Text,Code) VALUES ('{0}','{1}')", data1, data2);
            MySqlCommand cmd = new MySqlCommand(query, Connection);
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data inserted");
        }


    }
}

