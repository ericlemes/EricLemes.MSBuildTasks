using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Build.Framework;

namespace EricLemes.MSBuildTasks.Tests.Mock
{
	public class BuildEngineMock : IBuildEngine
	{
		public bool BuildProjectFile(string projectFileName, string[] targetNames, System.Collections.IDictionary globalProperties, System.Collections.IDictionary targetOutputs)
		{
			throw new NotImplementedException();
		}

		public int ColumnNumberOfTaskNode
		{
			get { throw new NotImplementedException(); }
		}

		public bool ContinueOnError
		{
			get { throw new NotImplementedException(); }
		}

		public int LineNumberOfTaskNode
		{
			get { throw new NotImplementedException(); }
		}

		public void LogCustomEvent(CustomBuildEventArgs e)
		{
			throw new NotImplementedException();
		}

		public void LogErrorEvent(BuildErrorEventArgs e)
		{
			throw new NotImplementedException();
		}

		public void LogMessageEvent(BuildMessageEventArgs e)
		{
			Console.WriteLine(e.Message);
		}

		public void LogWarningEvent(BuildWarningEventArgs e)
		{
			throw new NotImplementedException();
		}

		public string ProjectFileOfTaskNode
		{
			get { throw new NotImplementedException(); }
		}
	}
}
