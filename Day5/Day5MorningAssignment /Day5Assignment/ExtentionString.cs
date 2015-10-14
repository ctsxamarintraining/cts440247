using System;

namespace Day5Assignment
{
	public static class ExtentionString
	{
		public static int wordCount(this String input)
		{
			char[] delimiters = new char[] {' ', '\r', '\n' };
			return input.Split(delimiters,StringSplitOptions.RemoveEmptyEntries).Length; 
		}

		public static int characterCount(this String input)
		{
			char[] delimiters = new char[] {' ', '\r', '\n' };
			string[] strings = input.Split(delimiters,StringSplitOptions.RemoveEmptyEntries); 
			int noOfcharacters = 0;
			foreach (string s in strings) {
				noOfcharacters += s.Length; 
			}

			return noOfcharacters;
		}
	}
}

