using System;

namespace AssignmentFileIO
{
	

	class MainClass
	{
		public static void Main (string[] args)
		{
			//Question 1
			//Reverse the content of the first file and store in a new file
			Console.WriteLine ("\n*********Reverse the content of the source file in the new file***********\n");
			ReverseContentsOfFile reverseContents = new ReverseContentsOfFile ("Question1sourceFile.txt", "Question1destinationFile.txt");
			Console.WriteLine (reverseContents.ReverseContentsInNewFile ());

			//Question 2
			//Recursively copy the contents of the parent folder into the child folder
			Console.WriteLine ("\n******Recursively copy the contents of the parent folder into the child folder*******\n");
			RecursiveFolders recursiveCopy = new RecursiveFolders(1);
			recursiveCopy.createFolder ();

			//Question 3
			//Streamreader using byte array
			Console.WriteLine ("\n*********Streamreader using byte array***********\n");
			ByteArray byteArray = new ByteArray ("Question3Source.txt", "Question3Destination.txt");
			byteArray.write1MBOfContentToFile ();

			//Question 4
			//Encrypt and decrypt the file text
			Console.WriteLine ("\n*********Encrypt and decrypt the file text***********\n");
			Console.WriteLine ("Enter the input for file");
			string input = Console.ReadLine ();
			EncryptDecryptFileText encryptFileText = new EncryptDecryptFileText ();
			encryptFileText.EncryptFileText (input);
			encryptFileText.DecryptFileText ();

			//Question 5
			//Serialize and deserialize a class object to json
			Console.WriteLine ("\n*********Serialize and deserialize a class object to json***********\n");
			JsonSerialization serialize = new JsonSerialization();
			serialize.serialize ();
			Console.WriteLine ("\nThe deserialized value is: " + serialize.deserialize());
		}
	}
}
