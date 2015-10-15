	using System;
	using System.IO;

	namespace AssignmentFileIO
	{
		public class RecursiveFolders
		{
			string fileContent = "this is the content for file ";
			string pathString;
			private int noOfLevels;
			string subFolder = "subFolder";
			public RecursiveFolders (int noOfLevels)
			{
				this.noOfLevels = noOfLevels;
			}

			public void createFolder () {
			if (!File.Exists ("RootFolder")) {
				//Create the main folder
				// Specify a name for your top-level folder. 
				string folderName = "RootFolder";
				Directory.CreateDirectory (folderName);
				pathString = Path.GetFullPath (folderName);
				Console.WriteLine ("Question 2 path: " + pathString);
				// Create two files inside the main folder.  
				string pathForFile1 = Path.Combine (pathString, "RootFolderFile1.txt");
				//Create and write into the file 1
				File.WriteAllText (pathForFile1, fileContent);
				string pathForFile2 = Path.Combine (pathString, "RootFolderFile2.txt");
				//Create and write into the file 1
				File.WriteAllText (pathForFile2, fileContent);
				Directory.CreateDirectory (Path.Combine (pathString, subFolder));
			}
				//Call the recursive copy
				recursiveFileCopy();
			}
			/*end of method*/	

			private void recursiveFileCopy () {
			for (int fileLevel = 0; fileLevel < noOfLevels; fileLevel++) {
				DirectoryCopy (pathString,Path.Combine (pathString, subFolder), true);
				pathString = Path.Combine (pathString, subFolder);
			}
		}


		private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
		{
			// Get the subdirectories for the specified directory.
			DirectoryInfo dir = new DirectoryInfo(sourceDirName);

			DirectoryInfo[] dirs = dir.GetDirectories();
			// If the destination directory doesn't exist, create it.
			if (!Directory.Exists(destDirName))
			{
				Directory.CreateDirectory(destDirName);
			}

			// Get the files in the directory and copy them to the new location.
			FileInfo[] files = dir.GetFiles();
			foreach (FileInfo file in files)
			{
				string temppath = Path.Combine(destDirName, file.Name);

				file.CopyTo(temppath, true);
			}

			// If copying subdirectories, copy them and their contents to new location.
			if (copySubDirs)
			{
				foreach (DirectoryInfo subdir in dirs)
				{
					string temppath = Path.Combine(destDirName, subdir.Name);
					DirectoryCopy(subdir.FullName, temppath, copySubDirs);
				}
			}
		}
		}
	}


