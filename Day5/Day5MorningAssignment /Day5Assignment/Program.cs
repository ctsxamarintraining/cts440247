using System;

namespace Day5Assignment
{
	// Declare a delegate.
	delegate void AnonymousDelegate(string string1, string string2);

	class MainClass
	{
		public static void Main (string[] args)
		{
			//Question 1
			//Extention for string
			Console.WriteLine("\n**********Extension for string*******\n");
			string s = "It's a wonderful day";
			Console.WriteLine ("String is: " + s);
			Console.WriteLine ("No of words: " + s.wordCount());
			Console.WriteLine ("No of Caharcters: " + s.characterCount());

			//Question 2
			//Nullable type
			Console.WriteLine("\n**********Nullable types*******\n");
			int? nullableValue = null;
			Console.WriteLine ("Nullable value with null: " + nullableValue);
			nullableValue = 10;
			if (nullableValue.HasValue) {
				Console.WriteLine("Nullable value with value: " + nullableValue.Value);
			}

			//Question 3
			//Anonymous methods
			Console.WriteLine("\n**********Anonymous delegates*******\n");
			// Instantiate the delegate type using an anonymous method.
			AnonymousDelegate anonymousDelegate = delegate(string string1, string string2)
			{
				//Return the concatenated input string 
				System.Console.WriteLine("Anonymous delegate called.The concatenated reult is:\n" + (string1 + string2));
			};

			// Make anonymous delegate call with 2 strings.
			anonymousDelegate("New","York");



			//Question 4
			//Anonymous type with properties 
			var anonymousProperty = new { Name = "Shylaja", ID = "440247", City = "Chennai" };

			// Print the anonymous property.
			Console.WriteLine("\n**********Anonymous property*******\nName: " + anonymousProperty.Name + ", ID: " + anonymousProperty.ID + ", City: " + anonymousProperty.City);


			//Question 5
			//Partial classes
			Console.WriteLine("\n**********Partial classes*******\n");
			MainClassForFiles.MainClass ();

		}
	}
}
