using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace MediaLizer
{
	[XmlRoot()]
	public class Configuration
	{
		[XmlIgnore()]
		public static Configuration
			Config;
		[XmlIgnore()]
		public static string
			ConfigFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/MediaLizer/Config.xml";

		[XmlElement("Server")]
		public string
			SQLServer = "127.0.0.1";
		[XmlElement("User")]
		public string
			SQLUser = Environment.UserName;
		[XmlElement("Password")]
		public string
			SQLPassword = "";
		[XmlElement("Database")]
		public string
			SQLDatabase = "";
		[XmlElement("Contents")]
		public string
			Table_Contents = "";
		[XmlElement("Paths")]
		public string
			Table_Paths = "";

		public Configuration(string FileName)
		{
			ConfigFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/MediaLizer/" + FileName;

			if (!System.IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/MediaLizer"))
			{
				System.IO.Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/MediaLizer");
			}
			//DescriptionContains.Add("good");
			//DescriptionContains.Add("rust");
		}
		public Configuration()
		{
			Configuration.Config = new Configuration("Config.xml");
		}

		//[XmlIgnore()]
		public static bool Save()
		{
			XmlSerializer s;
			System.IO.TextWriter w;
			try
			{
				s = new XmlSerializer(typeof(Configuration));
				w = new System.IO.StreamWriter(ConfigFile);
				s.Serialize(w, Configuration.Config);
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				Functions.Error("Unable to save settings to: {0}", ConfigFile);
				return false;
			}
			finally
			{
				w.Close();
			}

		}
		//[XmlIgnore()]
		public static bool Load()
		{
			if (System.IO.File.Exists(ConfigFile))
			{
				XmlSerializer s;
				System.IO.TextReader tr;
				try
				{
					s = new XmlSerializer(typeof(Configuration));
					tr = new System.IO.StreamReader(ConfigFile);
					Configuration.Config = (Configuration)s.Deserialize(tr);
					tr.Close();
					return true;
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					Functions.Error("Exception while unserializing configuration.");
					return false;
				}
				finally
				{
					tr.Close();
				}
			}
			else
			{
				Functions.Error("Could not find the configuration file.  Using the default.");
				Configuration.Config = new Configuration();
				return false;
			}

		}


	}
}
