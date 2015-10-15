using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace AssignmentFileIO
{
	//Sample class to be serialized
	public class Employee
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}

	public class JsonSerialization
	{
		public JsonSerialization ()
		{
		}

		public void serialize () {
			Employee serializableClass = new Employee
			{FirstName = "Shylaja",
				LastName = "R"};
			
			//Serialize the object
			DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer (typeof(Employee));
			//Using memory stream to print in the console
			MemoryStream memoryStream = new MemoryStream();
			jsonSerializer.WriteObject (memoryStream, serializableClass);
			//Print the serialized object
			memoryStream.Position = 0;
			StreamReader streamReader = new StreamReader (memoryStream);
			Console.WriteLine ("JSON VALUE IS: " + streamReader.ReadToEnd());

			//Save the Json text to the file
			using (FileStream file = new FileStream ("json.json", FileMode.Create, FileAccess.Write)) {
				memoryStream.WriteTo (file);
			}

			streamReader.Close ();
			memoryStream.Close ();
		}

		public string deserialize() {
			DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(Employee));
			//Read the contents of the JSON file
			string contents = File.ReadAllText ("json.json");
			//Deserialize the text
			MemoryStream memoryStream = new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(contents));

			Employee emp = (Employee)js.ReadObject(memoryStream);
			memoryStream.Close();

			return emp.FirstName + emp.LastName;
		}
			
	}
}

