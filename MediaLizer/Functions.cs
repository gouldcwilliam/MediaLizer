using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MediaLizer
{
	/// <summary>
	/// Global Static Functions
	/// </summary>
	public static class Functions
	{
		/// <summary>
		/// Gets the install path.
		/// </summary>
		/// <returns>
		/// The install path.
		/// </returns>
		public static string GetInstallPath()
		{
			return System.IO.Path.GetDirectoryName(GetBinaryName());
		}

		/// <summary>
		/// Gets the name of the binary.
		/// </summary>
		/// <returns>
		/// The binary name.
		/// </returns>
		public static string GetBinaryName()
		{
			return System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase.Replace("file:", "");
		}


		/// <summary>
		/// Gets the config path.
		/// </summary>
		/// <returns>
		/// The config path.
		/// </returns>
		public static string GetConfigPath()
		{
			return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Functions.GetPathSeparator();
		}


		/// <summary>
		/// Gets the path separator.
		/// </summary>
		/// <returns>
		/// The path separator.
		/// </returns>
		public static string GetPathSeparator()
		{
			if (Environment.OSVersion.ToString().ToLower().Contains("nix"))
			{
				return "/";
			}
			return @"\";
		}

		/// <summary>
		/// Gets the connection string.
		/// </summary>
		/// <returns>
		/// The connection string.
		/// </returns>
		public static string GetConnectionString(string Server, string User, string Password, string Database)
		{
			try
			{
				string value = string.Format("server={0};uid={1};", Configuration.Config.SQLServer, Configuration.Config.SQLUser);
				if (Configuration.Config.SQLPassword.Trim() != "")
				{
					value += string.Format("password={0};", Configuration.Config.SQLPassword);
				}
				if (Configuration.Config.SQLDatabase.Trim() != "")
				{
					value += string.Format("database={0};", Configuration.Config.SQLDatabase);
				}
				return value;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				Error("Unable to create a connection string from xml configuration.");
				return "";
			}
		}

		public static string GetConnectionString()
		{
			return GetConnectionString(Configuration.Config.SQLServer, Configuration.Config.SQLUser, Configuration.Config.SQLPassword, Configuration.Config.SQLDatabase);
		}


		/// <summary>
		/// Pause this instance.
		/// </summary>
		public static void Pause()
		{
			Console.Write("Press any key...");
			Console.ReadKey();
		}


		public static void Error(string Message, params object[] args)
		{
			System.ConsoleColor color = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(Message, args);
			Console.ForegroundColor = color;
		}


		/// <summary>
		/// Gets the yes no.
		/// </summary>
		/// <returns>
		/// True for y/yes False for n/no.
		/// </returns>
		/// <param name='QuestionText'>
		/// Question text to print before reading input.
		/// </param>
		/// <param name='args'>
		/// args used in formating the question string
		/// </param>
		public static bool YesNo(string QuestionText, params object[] args)
		{
			Console.Write(QuestionText, args);
			return YesNo();
		}
		/// <summary>
		/// Gets the yes no.
		/// </summary>
		/// <returns>
		/// True for y/yes False for n/no.
		/// </returns>
		/// <param name='QuestionText'>
		/// Question text to print before reading input.
		/// </param>
		public static bool YesNo(string QuestionText)
		{
			Console.Write(QuestionText);
			return YesNo();
		}
		/// <summary>
		/// Gets the yes no.
		/// </summary>
		/// <returns>
		/// The true for y/yes false for n/no.
		/// </returns>
		public static bool YesNo()
		{
			string input = Console.ReadLine();
			if (input.Replace(" ", "") == string.Empty)
			{
				return false;
			}
			switch (input.ToLower())
			{
				case "yes":
				case "y":
					return true;
				default:
					return false;
			}
		}



		/// <summary>
		/// Lists the path contents.
		/// </summary>
		/// <returns>
		/// The path contents.
		/// </returns>
		/// <param name='PathToList'>
		/// Path to list.
		/// </param>
		public static List<string> listPathContents(string PathToList)
		{
			List<string> value = new List<string>();

			if (System.IO.Directory.Exists(PathToList))
			{
				foreach (string item in System.IO.Directory.GetFileSystemEntries(PathToList))
				{
					value.Add(item);
				}
			}
			return value;
		}


		public static string ChangeConnectionSettings()
		{
			Console.WriteLine("Current Settings:\n\tServer: {0}\n\tUser: {1}\n\tPassword: {2}\n\tDatabase: {3}",
			                  Configuration.Config.SQLServer,
			                  Configuration.Config.SQLUser,
			                  Configuration.Config.SQLPassword,
			                  Configuration.Config.SQLDatabase);

			Console.Write("Enter Server: ");
			string server = Console.ReadLine();
			Console.Write("Enter User: ");
			string user = Console.ReadLine();
			Console.Write("Enter Password: ");
			string password = Console.ReadLine();
			Console.Write("Enter Database: ");
			string database = Console.ReadLine();

			return GetConnectionString(server, user, password, database); 
		}
			
	}
}

