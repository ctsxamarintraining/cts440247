using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace AssignmentFileIO
{
	public class EncryptDecryptFileText
	{
		string pathOfEncryptedFile;
		string  plainText;    // original plaintext

		string  passPhrase      = "Pas5pr@se";        // can be any string
		string  saltValue       = "s@1tValue";        // can be any string
		string  hashAlgorithm   = "SHA1";             // can be "MD5"
		int     passwordIterations= 2;                // can be any number
		string  initVector      = "@1B2c3D4e5F6g7H8"; // must be 16 bytes
		int     keySize         = 256;                // can be 192 or 128
		string cipherText;

		public void EncryptFileText (string input)
		{
			plainText = input;
			cipherText = EncryptandDecrypt.Encrypt
				(
					plainText,
					passPhrase,
					saltValue,
					hashAlgorithm,
					passwordIterations,
					initVector,
					keySize
				);
			Console.WriteLine(String.Format("Plaintext : {0}", plainText));

			Console.WriteLine(String.Format("Encrypted : {0}", cipherText));
			//Write the encrypted text to the file
			writeToFile(cipherText);
		}

		public void DecryptFileText () {
			cipherText = readFromFile (pathOfEncryptedFile);


			plainText = EncryptandDecrypt.Decrypt
				(
					cipherText,
					passPhrase,
					saltValue,
					hashAlgorithm,
					passwordIterations,
					initVector,
					keySize
				);

			Console.WriteLine(String.Format("Decrypted : {0}", plainText));
		}

		//To write the contents to the file
		private void writeToFile (string cipherText) {
			using (var textWriter = new StreamWriter ("EncryptedFile.txt")) {
				// write a line of text to the file
				textWriter.WriteLine (cipherText);
				pathOfEncryptedFile = Path.GetFullPath ("EncryptedFile.txt");
				Console.WriteLine ("Encrypted text file path: " + pathOfEncryptedFile);
			}
		}

		//To read the contents of the file
		private string readFromFile (string fileName) {
			return File.ReadAllText (fileName);
		}
	}


		public class EncryptandDecrypt
		{
			public static string Encrypt
			(
				string  plainText,
				string  passPhrase,
				string  saltValue,
				string  hashAlgorithm,
				int     passwordIterations,
				string  initVector,
				int     keySize
			)
			{
				byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
				byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
				byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
				PasswordDeriveBytes password = new PasswordDeriveBytes
					(
						passPhrase,
						saltValueBytes,
						hashAlgorithm,
						passwordIterations
					);
				byte[] keyBytes = password.GetBytes(keySize / 8);
				RijndaelManaged symmetricKey = new RijndaelManaged();
				symmetricKey.Mode = CipherMode.CBC;
				ICryptoTransform encryptor = symmetricKey.CreateEncryptor
					(
						keyBytes,
						initVectorBytes
					);
				MemoryStream memoryStream = new MemoryStream();
				CryptoStream cryptoStream = new CryptoStream
					(
						memoryStream,
						encryptor,
						CryptoStreamMode.Write
					);
				// Start encrypting.
				cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

				// Finish encrypting.
				cryptoStream.FlushFinalBlock();

				// Convert our encrypted data from a memory stream into a byte array.
				byte[] cipherTextBytes = memoryStream.ToArray();

				// Close both streams.
				memoryStream.Close();
				cryptoStream.Close();

				// Convert encrypted data into a base64-encoded string.
				string cipherText = Convert.ToBase64String(cipherTextBytes);

				// Return encrypted string.
				return cipherText;
			}
			public static string Decrypt
			(
				string  cipherText,
				string  passPhrase,
				string  saltValue,
				string  hashAlgorithm,
				int     passwordIterations,
				string  initVector,
				int     keySize
			)
			{
				byte[] initVectorBytes = Encoding.ASCII.GetBytes (initVector);
				byte[] saltValueBytes = Encoding.ASCII.GetBytes (saltValue);

				// Convert our ciphertext into a byte array.
				byte[] cipherTextBytes = Convert.FromBase64String (cipherText);
				PasswordDeriveBytes password = new PasswordDeriveBytes (
					passPhrase,
					saltValueBytes,
					hashAlgorithm,
					passwordIterations
				);
				byte[] keyBytes = password.GetBytes (keySize / 8);

				// Create uninitialized Rijndael encryption object.
				RijndaelManaged symmetricKey = new RijndaelManaged ();

				// It is reasonable to set encryption mode to Cipher Block Chaining
				// (CBC). Use default options for other symmetric key parameters.
				symmetricKey.Mode = CipherMode.CBC;

				// Generate decryptor from the existing key bytes and initialization 
				// vector. Key size will be defined based on the number of the key 
				// bytes.
				ICryptoTransform decryptor = symmetricKey.CreateDecryptor
					(
						keyBytes,
						initVectorBytes
					);

				// Define memory stream which will be used to hold encrypted data.
				MemoryStream memoryStream = new MemoryStream (cipherTextBytes);

				// Define cryptographic stream (always use Read mode for encryption).
				CryptoStream cryptoStream = new CryptoStream (
					memoryStream,
					decryptor,
					CryptoStreamMode.Read
				);
				byte[] plainTextBytes = new byte[cipherTextBytes.Length];

				// Start decrypting.
				int decryptedByteCount = cryptoStream.Read
					(
						plainTextBytes,
						0,
						plainTextBytes.Length
					);

				// Close both streams.
				memoryStream.Close ();
				cryptoStream.Close ();

				// Convert decrypted data into a string. 
				// Let us assume that the original plaintext string was UTF8-encoded.
				string plainText = Encoding.UTF8.GetString
					(
						plainTextBytes,
						0,
						decryptedByteCount
					);

				// Return decrypted string.   
				return plainText;
			}
		}
}

