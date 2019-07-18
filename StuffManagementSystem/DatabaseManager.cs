using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace StuffManagementSystem
{
    public class Thing
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public string Place { get; set; }
    }

    public class Place
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Group { get; set; }
    }

    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }


    public static class DatabaseManager
    {
        public const string FileName = "StuffManagementSystem.sqlite";
        private static SQLiteConnection conn = new SQLiteConnection("Data Source=" + FileName + ";Version=3;");

        static DatabaseManager()
        {
            if (File.Exists(FileName))
            {
                conn.Open();
            }
            else
            {
                SQLiteConnection.CreateFile("StuffManagementSystem.sqlite");

                conn.Open();

                // set up database
                new SQLiteCommand("create table Things (id INT PRIMARY KEY, name TEXT, desc TEXT, count INT, place INT)", conn).ExecuteNonQuery();
                new SQLiteCommand("create table Places (id INT PRIMARY KEY, name TEXT, desc TEXT, groupId INT)", conn).ExecuteNonQuery();
                new SQLiteCommand("create table Groups (id INT PRIMARY KEY, name TEXT, desc TEXT)", conn).ExecuteNonQuery();

                new SQLiteCommand("insert into Groups values(1,'Default','Default group')", conn).ExecuteNonQuery();
                new SQLiteCommand("insert into Places values(1,'Default','Default place', 1)", conn).ExecuteNonQuery();
            }
        }

        public static IEnumerable<Thing> Things
        {
            get
            {
                SQLiteDataReader reader = new SQLiteCommand("select * from Things", conn).ExecuteReader();
                while (reader.Read())
                {
                    yield return new Thing
                    {
                        Name = (string)reader["name"],
                        Description = (string)reader["desc"],
                        Id = (int)reader["id"],
                        Count = (int)reader["count"],
                        Place = Places.First(i => i.Id == (int)reader["place"]).Name
                    };
                }
            }
        }
    

        public static IEnumerable<Place> Places
        {
            get
            {
                SQLiteDataReader reader = new SQLiteCommand("select * from Places", conn).ExecuteReader();
                while (reader.Read())
                {
                    yield return new Place
                    {
                        Name = (string)reader["name"],
                        Description = (string)reader["desc"],
                        Id = (int)reader["id"],
                        Group = reader["groupId"].ToString()
                    };
                }
            }
        }

        public static IEnumerable<Group> Groups
        {
            get
            {
                SQLiteDataReader reader = new SQLiteCommand("select * from Groups", conn).ExecuteReader();
                while (reader.Read())
                {
                    yield return new Group
                    {
                        Name = (string)reader["name"],
                        Description = (string)reader["desc"],
                        Id = (int)reader["id"],
                    };
                }
            }
        }

        public static void AddThing(Thing thing)
        {
            int id = 1;
            for (; Things.Any(i => i.Id == id); id++);
            SQLiteCommand cmd = new SQLiteCommand("insert into Things values (@id,@name,@desc,@count,@place)", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@name", thing.Name);
            cmd.Parameters.AddWithValue("@desc", thing.Description);
            cmd.Parameters.AddWithValue("@count", thing.Count);
            cmd.Parameters.AddWithValue("@place", Places.First(i => i.Name == thing.Place).Id);
            cmd.ExecuteNonQuery();
        }

        public static void AddPlace(Place place)
        {
            int id = 1;
            for (; Places.Any(i => i.Id == id); id++) ;
            SQLiteCommand cmd = new SQLiteCommand("insert into Places values (@id,@name,@desc,@group)", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@name", place.Name);
            cmd.Parameters.AddWithValue("@desc", place.Description);
            cmd.Parameters.AddWithValue("@group", place.Group);
            cmd.ExecuteNonQuery();
        }

        public static void AddGroup(Group group)
        {
            int id = 1;
            for (; Groups.Any(i => i.Id == id); id++) ;
            SQLiteCommand cmd = new SQLiteCommand("insert into Groups values (@id,@name,@desc)", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@name", group.Name);
            cmd.Parameters.AddWithValue("@desc", group.Description);
            cmd.ExecuteNonQuery();
        }
    }
}
