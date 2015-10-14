using System;

namespace Day5Assignment
{
	public class MainClassForFiles
	{
		
		public static void MainClass()
		{
			//Example
			//Add the two values in partialclass1 and pass the result to the partial class two to square it.
			int result = PartialClass.PartialClass1 (2,3);
			Console.WriteLine ("Final result is: " + PartialClass.PartialClass2 (result));
		}
	}
}

