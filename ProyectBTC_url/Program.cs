using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Linq;
using System.Threading;
using System.Timers;
using System.Threading.Tasks;
using System.Collections.Generic;
using Capas;

namespace ProyectBTC_url
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            //MySql.Data.MySqlClient.MySqlConnection conn;
            //string myConnectionString;

            //myConnectionString = "server=localhost;uid=root;" +
            //    "pwd=root;database=cambiosdb";

            //try
            //{
            //    conn = new MySql.Data.MySqlClient.MySqlConnection
            //    {
            //        ConnectionString = myConnectionString
            //    };
            //    conn.Open();
            //}
            //catch (MySql.Data.MySqlClient.MySqlException ex)
            //{

            //}
            DL dl = new DL();
            var a = dl.xxAsync(null).Result;


            Console.WriteLine("Hello World!");
        }
    }
}
