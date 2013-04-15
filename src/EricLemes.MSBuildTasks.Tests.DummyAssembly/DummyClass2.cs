using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EricLemes.MSBuildTasks.Tests.DummyAssembly
{
	public class DummyClass2
	{
		public void Test()
		{
			PrivateMethod();
		}

		private void PrivateMethod()
		{
			Console.WriteLine("Do something");
		}
	}
}
