using System;

namespace ExceptionHandling
{
	class MainClass
	{
		
		public static void Main (string[] args)
		{
			int[] numberArray = new int[10];
			int i = 0;
			try {
				while (i >= 0) {
					Console.WriteLine ("enter a number: ");
					int number = Convert.ToInt32 (Console.ReadLine ());
					numberArray [i] = number;
					i++;
					//Remove empty entries in the array for displaying
					int[] array = new int[i];
					Array.Copy(numberArray,array,i);
					string numbers = string.Join (",", array);
					Console.WriteLine ("the array is: " + numbers);
				}
			}
			 catch (IndexOutOfRangeException indexEx) {
				Console.WriteLine ("Maximum input limit of arrays reached." + indexEx.Message);
			} catch (FormatException formatEx) {
				Console.WriteLine ("Please enter integer only. " + formatEx.Message);
			} catch (Exception ex) {
				Console.WriteLine ("Default exception. " + ex.Message);
			}
//			Console.ReadLine ();
		}
	}
}
