using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EricLemes.MSBuildTasks.Tests.DummyAssembly
{
	public class DummyClass
	{
		public void SimpleMethod()
		{
			Console.WriteLine("Something");
			Console.WriteLine("Something");
			Console.WriteLine("Something");
			Console.WriteLine("Something");
			Console.WriteLine("Something");
		}

		public void SimpleMethod2(bool input)
		{
			if (input)
				Console.WriteLine("Blah");
			else
				Console.WriteLine("Blah2");
		}
	}
}
