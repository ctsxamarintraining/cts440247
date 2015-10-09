using System;

namespace AssignmentGarbageCollection
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			long totalMemoryAvailable = GC.GetTotalMemory (true);
			Console.WriteLine("Total memory before reserving: " + totalMemoryAvailable);
			long memorySpaceAvble1 = GC.GetTotalMemory (false);
			{
				//Initialize an array and make it unreachable
				string[] unreachableArray = new string[totalMemoryAvailable];
				unreachableArray = null;
			}
			Console.WriteLine("Total memory after reserving unreachable array: " + memorySpaceAvble1);
			GC.Collect();
			Console.WriteLine("Total memory after freeing: " + GC.GetTotalMemory(true));

		}
	}
}
