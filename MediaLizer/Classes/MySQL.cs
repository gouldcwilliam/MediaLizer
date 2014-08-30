using System;

using MySql.Data.MySqlClient;

namespace MediaLizer.Classes
{
	/// <summary>
	/// MySQL specific SQL class.
	/// </summary>
	public class MySQL : Classes.SQL
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MediaLizer.Classes.MySQL"/> class.
		/// </summary>
		/// <param name='ConnectionString'>
		/// Connection string.
		/// </param>
		public MySQL(string sConnectionString)
		{
			ConnectionString = sConnectionString;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="MediaLizer.Classes.MySQL"/> class.
		/// </summary>
		public MySQL()
		{
			ConnectionString = string.Empty;
		}


		/// <summary>
		/// The connection.
		/// </summary>
		MySqlConnection Connection;


		/// <summary>
		/// Gets the connection string.
		/// </summary>
		/// <value>
		/// The connection string.
		/// </value>
		public string ConnectionString{ get; set; }



		//


		/// <summary>
		/// Tests the connection.
		/// </summary>
		/// <returns>
		/// The connection.
		/// </returns>
		public bool TestConnection()
		{
			try
			{
				Connection = new MySqlConnection(ConnectionString);
				Connection.Open();
				Connection.Close();
				return true;
			} 
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				Functions.Error("Failed to connect using: {0}", ConnectionString);
				return false;
			}
			finally
			{
				Connection.Close();
			}
		}
	}
}

