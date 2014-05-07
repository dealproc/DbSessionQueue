namespace Test {
	using System;
	public class Constants {
		public static readonly string DATA_FOLDER = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\Midnight Oil Systems\\DbSessionQueue\\";
		public static readonly string DATABASE_FILE = DATA_FOLDER + "payments.db3";
		public static readonly string CONNECTION_STRING = string.Format("Version=3;UTF16Encoding=True;Data Source={0};", DATABASE_FILE);
		//public static readonly string CONNECTION_STRING_CREATE = CONNECTION_STRING + "New=True;";
	}
}