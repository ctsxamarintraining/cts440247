using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Linq;

namespace AssignmentFileIO
{
	public class ByteArray
	{
		string SourceFile;
		string destinationFile;

		public ByteArray (string sourceFile, string destinationFile)
		{
			this.SourceFile = sourceFile;
			this.destinationFile = destinationFile;
		}

		public void write1MBOfContentToFile () {

			//If the source file isnt already present

			if (!File.Exists(SourceFile)) {
				//using (var textWriter = new StreamWriter (SourceFile)) {
				byte[] data = new byte[8];//[1 * 1024 * 1024];
				Random random = new Random();
				random.NextBytes(data);
				string ip = Display(data);
				File.WriteAllText(SourceFile, ip);
				Console.WriteLine ("Question 3 full path of the source file: " + Path.GetFullPath (SourceFile));
			}
			readFromTheFile (Path.GetFullPath (SourceFile));
		}

		private string Display(byte[] array)
		{
			Console.WriteLine ("Writing file");
			string randomData = "";
			// Loop through and display bytes in array.
			foreach (byte value in array)
			{
				randomData += value ; 
			}
			return randomData; 
		}

		void readFromTheFile (string filePath) {

			using (var textWriter = new StreamWriter (destinationFile))

			using(Stream source = File.OpenRead(filePath)) {
				byte[] buffer = new byte[4];//[500];
				string content = "";
				int bytesRead;
				int bytesToRead = buffer.Length;
				int totalBytesRead = 0;
				while((bytesRead = source.Read(buffer, 0, bytesToRead)) > 0) {
//					dest.Write(buffer, 0, bytesRead);
					content += Encoding.UTF8.GetString(buffer);
					totalBytesRead += bytesRead;
					bytesToRead = (int)((source.Length - totalBytesRead) > buffer.Length  ? buffer.Length  : (source.Length - totalBytesRead));
				}
				textWriter.WriteLine (content);
				Console.WriteLine ("Question 3 full path of the destination file: " + Path.GetFullPath (destinationFile));
			}
		}
	}
}

