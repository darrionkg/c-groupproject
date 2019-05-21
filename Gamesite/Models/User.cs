using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;

namespace Gamesite.Models
{
    public class User
    {
        public string Username {get; set;}
        public string Email {get; set;}
        public string Password {get; set;}
        public int Id {get; set;}
        public bool ActiveUser {get; set;}

        public User(string username, string email, string password, int id = 0)
        {
            Username = username;
            Email = email;
            Password = password;
            Id = id;
            ActiveUser = false;
        }

        public void Save()
        {
          MySqlConnection conn = DB.Connection();
          conn.Open();
          MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"INSERT INTO user (email, password, username) VALUES ('"+Email+"', '"+Password+"', '"+Username+"');";
          cmd.ExecuteNonQuery();
          Id = (int) cmd.LastInsertedId;
          conn.Close();
          if (conn != null)
          {
            conn.Dispose();
          }
        }

        public static User Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM user WHERE id = '"+id+"';";
            int userId = 0;
            string userUsername = "";
            string userEmail = "";
            string userPassword = "";
            while(rdr.Read())
            {
                userId = rdr.GetInt32(0);
                userUsername = rdr.GetString(1);
                userEmail = rdr.GetString(2);
                userPassword = rdr.GetString(3);
            }
            User newUser = new User(userUsername, userEmail, userPassword, userId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return newUser;
        }

        //Overloaded find method using email and password
        public static User Find(string email, string password)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM user WHERE email = '"+email+"' AND password = '"+password+"';";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int userId = 0;
            string userUsername = "";
            string userEmail = "";
            string userPassword = "";
            while(rdr.Read())
            {
                userId = rdr.GetInt32(0);
                userUsername = rdr.GetString(1);
                userEmail = rdr.GetString(2);
                userPassword = rdr.GetString(3);
            }
            User newUser = new User(userUsername, userEmail, userPassword, userId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return newUser;
        }

        public bool ValidateEmail()
        {
          MySqlConnection conn = DB.Connection();
          conn.Open();
          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"SELECT * FROM user WHERE email = '"+Email+"';";
          var rdr = cmd.ExecuteReader() as MySqlDataReader;
          while(rdr.Read())
          {
              string userEmail = rdr.GetString(1);
              if(userEmail == Email)
              {
                return true;
              }
          }
          return false;
        }

        public bool ValidatePassword()
        {
          if(Password.Length < 5)
          {
            return false;
          }
          else if(!(Password.Any(char.IsUpper)))
          {
            return false;
          }
          return true;
        }

        // If a duplicate is found, return false. FALSE = INVALID
        public bool CheckDuplicateUsername()
        {
          MySqlConnection conn = DB.Connection();
          conn.Open();
          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"SELECT * FROM user WHERE username = '"+Username+"' OR email ='"+Email+"';";
          var rdr = cmd.ExecuteReader() as MySqlDataReader;
          while(rdr.Read())
          {
              string email = rdr.GetString(1);
              string username = rdr.GetString(3);
              if(username == Username || email == Email)
              {
                return false;
              }
          }
          return true;
        }

        // forgot Username
        // forgot password


        // private static List<Country> _instances = new List<Country> {};
        // private string _countryName;
        // private int _countryPopulation;
        // private string _countryRegion;
        // private string _countryContinent;
        // private string _countryCode;
        //
        // public Country(string countryName, int countryPopulation, string countryRegion, string countryContinent, string countryCode)
        // {
        //     _countryName = countryName;
        //     _countryPopulation = countryPopulation;
        //     _countryRegion = countryRegion;
        //     _countryContinent = countryContinent;
        //     _countryCode = countryCode;
        //     _instances.Add(this);
        // }
        //
        //
        // public string CountryName { get => _countryName; set => _countryName = value;}
        // public int CountryPopulation { get => _countryPopulation; set => _countryPopulation = value;}
        // public string CountryRegion { get => _countryRegion; set => _countryRegion = value;}
        // public string CountryContinent { get => _countryContinent; set => _countryContinent = value;}
        // public string CountryCode { get => _countryCode; set => _countryCode = value;}
        //
        // public static void ClearAll()
        // {
        //     _instances.Clear();
        // }
        //
        // public static List<Country> GetAll()
        // {
        //     List<Country> allCountries = new List<Country> {};
        //     MySqlConnection conn = DB.Connection();
        //     conn.Open();
        //     MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        //     cmd.CommandText = @"SELECT Name, Population, Region, Continent, Code FROM country ORDER BY Name;";
        //     MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        //     while(rdr.Read())
        //     {
        //         string countryName = rdr.GetString(0);
        //         int countryPopulation = rdr.GetInt32(1);
        //         string countryRegion = rdr.GetString(2);
        //         string countryContinent = rdr.GetString(3);
        //         string countryCode = rdr.GetString(4);
        //         Country newCountry = new Country(countryName, countryPopulation, countryRegion, countryContinent, countryCode);
        //         allCountries.Add(newCountry);
        //     }
        //     conn.Close();
        //     if (conn != null)
        //     {
        //         conn.Dispose();
        //     }
        //     return allCountries;
        // }

    }
}
