using System;
using MySql.Data.MySqlClient;
using Gamesite.Models;

namespace Gamesite.Models
{

    public class DB
    {

      public static MySqlConnection Connection()
      {
          MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
          return conn;
      }

    }

}
