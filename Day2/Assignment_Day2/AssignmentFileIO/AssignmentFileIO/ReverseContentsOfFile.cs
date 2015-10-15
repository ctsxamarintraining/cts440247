//Question 1
using System;
using System.IO;
using System.Text;

namespace AssignmentFileIO
{
	public class ReverseContentsOfFile
	{
		string SourceFile;
		string destinationFile;
		string fileContent = "First line\nSecond line\nThird line\nFourth line\nFifth line";
		//The contructor accepts source and destination file names
		public ReverseContentsOfFile (string sourceFile, string destinationFile)
		{
			this.SourceFile = sourceFile;
			this.destinationFile = destinationFile;
		}
		public bool ReverseContentsInNewFile () {
			//If the file exists extract the source contents 
			if (File.Exists (SourceFile)) {
				//Write the source content to the destination
				writeToDestinationFile ();
			} else {
				Console.WriteLine ("Sorry the source file doesn't exist.\nCreating the source file...");
				// create a writer and open the file
				using (var textWriter = new StreamWriter (SourceFile)) {
					// write a line of text to the file
					textWriter.WriteLine (fileContent);
					Console.WriteLine ("Question1 full path of the source file: " + Path.GetFullPath (SourceFile));
				}
				writeToDestinationFile ();
			}
			return true;
		}


		private void writeToDestinationFile () {
			//Read the source content
			string content = File.ReadAllText (SourceFile);
			Console.WriteLine ("Current content of file:");
			Console.WriteLine (content);
			//Reverse the source content
			if (content != null) {
				var lines = content.Split (new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
				Array.Reverse (lines);
				StringBuilder builder = new StringBuilder ();
				foreach (var line in lines) {
					builder.Append (line);
					builder.Append ("\n");
				}
				string reversedLine = builder.ToString ();
				Console.WriteLine (reversedLine);

				//Write the source contents to the destination file
				using (var textWriter = new StreamWriter (destinationFile)) {
					// write a line of text to the file
					textWriter.WriteLine (reversedLine);
					Console.WriteLine ("Question1 full path of the destination file: " + Path.GetFullPath (destinationFile));
				}
			}
		}
	}
}

