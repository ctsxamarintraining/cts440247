using System;
using Mono.Data.Sqlite;
using System.IO;
using System.Data;
using System.Collections;
using System.Collections.Generic;

namespace RSSFeedApp
{
	public class Sqlite
	{

		public Sqlite ()
		{
		}

		public static SqliteConnection GetConnection()
		{
			var documents = Environment.GetFolderPath (
				Environment.SpecialFolder.Personal);
			string db = Path.Combine (documents, "RSSFeedDB.db3");
			Console.WriteLine (db);
			bool exists = File.Exists (db);
			if (!exists)
				SqliteConnection.CreateFile (db);
			var conn = new SqliteConnection("Data Source=" + db);
			if (!exists) {
				var commands = new[] {
					"CREATE TABLE FeedLinks (Subscribed INTEGER NOT NULL, FeedLinks ntext)",
					//enter sample data
					"INSERT INTO FeedLinks (Subscribed, FeedLinks) VALUES (0, 'http://feeds.feedburner.com/RayWenderlich')",
					"INSERT INTO FeedLinks (Subscribed, FeedLinks) VALUES (0, 'http://feeds.feedburner.com/vmwstudios')",
					"INSERT INTO FeedLinks (Subscribed, FeedLinks) VALUES (0, 'http://idtypealittlefaster.blogspot.com/feeds/posts/default')",
					"INSERT INTO FeedLinks (Subscribed, FeedLinks) VALUES (0, 'http://www.71squared.com/feed/')",
					"INSERT INTO FeedLinks (Subscribed, FeedLinks) VALUES (0, 'http://cocoawithlove.com/feeds/posts/default')",
					"INSERT INTO FeedLinks (Subscribed, FeedLinks) VALUES (0, 'http://feeds2.feedburner.com/brandontreb')",
					"INSERT INTO FeedLinks (Subscribed, FeedLinks) VALUES (0, 'http://feeds.feedburner.com/CoryWilesBlog')",
					"INSERT INTO FeedLinks (Subscribed, FeedLinks) VALUES (0, 'http://geekanddad.wordpress.com/feed/')",
					"INSERT INTO FeedLinks (Subscribed, FeedLinks) VALUES (0, 'http://iphonedevelopment.blogspot.com/feeds/posts/default')",
					"INSERT INTO FeedLinks (Subscribed, FeedLinks) VALUES (0, 'http://karnakgames.com/wp/feed/')",
					"INSERT INTO FeedLinks (Subscribed, FeedLinks) VALUES (0, 'http://kwigbo.com/rss')",
					"INSERT INTO FeedLinks (Subscribed, FeedLinks) VALUES (0, 'http://shawnsbits.com/feed/')",
					"INSERT INTO FeedLinks (Subscribed, FeedLinks) VALUES (0, 'http://pocketcyclone.com/feed/')",
					"INSERT INTO FeedLinks (Subscribed, FeedLinks) VALUES (0, 'http://www.alexcurylo.com/blog/feed/')",
					"INSERT INTO FeedLinks (Subscribed, FeedLinks) VALUES (0, 'http://feeds.feedburner.com/maniacdev')",
					"INSERT INTO FeedLinks (Subscribed, FeedLinks) VALUES (0, 'http://feeds.feedburner.com/macindie')",
				};
				conn.Open ();
				foreach (var cmd in commands) {
					using (var c = conn.CreateCommand()) {
						c.CommandText = cmd;
						c.CommandType = CommandType.Text;
						c.ExecuteNonQuery ();
					}
				}
				conn.Close ();
			}
			return conn;
		}

		public static List<Hashtable> getData (bool followingFeed) {
			
			List<Hashtable> dbItems = new List<Hashtable>();

			var documents = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			string db = Path.Combine (documents, "RSSFeedDB.db3");
			db = "Data Source=" + db;
			using(SqliteConnection con = new SqliteConnection(db))
			{

				con.Open();
				string stm;

				//To fetch only the feeds that are followed by the user
				if (followingFeed) {
					stm = "SELECT * FROM FeedLinks WHERE Subscribed =  '0'";
				} else {
					//To fetch all feeds
					stm = "SELECT * FROM FeedLinks";
				}

				using (SqliteCommand cmd = new SqliteCommand(stm, con))
				{
					using (SqliteDataReader rdr = cmd.ExecuteReader())
					{
						while (rdr.Read()) 
						{
							Console.WriteLine(rdr.GetInt32(0) + " " 
								+ rdr.GetString(1));
							Hashtable dict = new Hashtable();
							dict.Add("subscription", rdr.GetInt32(0));
							dict.Add ("link", rdr.GetString(1));
							dbItems.Add (dict);
						}         
					}
				}

				con.Close();   
			}

			return dbItems;
		}

		public static void updateFeed (string link, int subscriptionStatus) {

			string update = "UPDATE FeedLinks Set Subscribed  = " + subscriptionStatus + " WHERE FeedLinks = '" + link + "'";
			var documents = Environment.GetFolderPath (
				Environment.SpecialFolder.Personal);
			string db = Path.Combine (documents, "RSSFeedDB.db3");
//			db = "Data Source=" + db;
			bool exists = File.Exists (db);
			if (exists) {
				var conn = new SqliteConnection("Data Source=" + db);
				var commands = new[] {
					update
				};
				conn.Open ();
				foreach (var cmd in commands) {
					using (var c = conn.CreateCommand()) {
						c.CommandText = cmd;
						c.CommandType = CommandType.Text;
						c.ExecuteNonQuery ();
					}
				}
				conn.Close ();

			}
		}
	}
}

