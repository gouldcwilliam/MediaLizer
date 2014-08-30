using System;
using System.Configuration;
using System.Collections.Generic;

namespace MediaLizer
{
	class MainClass
	{
		/// <summary>
		/// The entry point of the program, where the program control starts and ends.
		/// </summary>
		/// <param name='args'>
		/// The command-line arguments.
		/// </param>
		public static void Main(string[] args)
		{
			Configuration.Load();
			Configuration.Save();

			Classes.MySQL sql = new Classes.MySQL(Functions.GetConnectionString());



			// END
			if (!Environment.OSVersion.ToString().ToLower().Contains("unix"))
			{
				Functions.Pause();
			}
		}
	}
}
