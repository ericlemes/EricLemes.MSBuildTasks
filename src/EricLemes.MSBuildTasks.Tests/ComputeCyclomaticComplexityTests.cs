using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Build.Framework;
using EricLemes.MSBuildTasks.Tests.Mock;
using System.IO;
using System.Reflection;

namespace EricLemes.MSBuildTasks.Tests
{
	[TestClass]
	public class ComputeCyclomaticComplexityTests
	{
		private ITaskItem[] _taskItems;
		private IBuildEngine _buildEngine;

		[TestInitialize]
		public void Initialize()
		{
			_buildEngine = new BuildEngineMock();

			Console.WriteLine("Blah blah blah " + Assembly.GetExecutingAssembly().Location);

			_taskItems = new TaskItemMock[1];
			_taskItems[0] = new TaskItemMock(@"EricLemes.MSBuildTasks.Tests.DummyAssembly.dll").AddMetadata("FullPath", 
				Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\\EricLemes.MSBuildTasks.Tests.DummyAssembly.dll");
		}

		[TestMethod]
		public void TestComputeCyclomaticComplexityForAssembly()
		{
			ComputeCyclomaticComplexity computeCC = new ComputeCyclomaticComplexity();
			computeCC.BuildEngine = _buildEngine;
			computeCC.InputFiles = _taskItems;
			computeCC.Execute();
			Assert.AreEqual(1, computeCC.Output.Count);
			Assert.AreEqual("EricLemes.MSBuildTasks.Tests.DummyAssembly.dll", computeCC.Output[0].Assembly.ManifestModule.Name);
			Assert.AreEqual(2, computeCC.Output[0].Types.Count);
			Assert.AreEqual("DummyClass", computeCC.Output[0].Types[0].Type.Name);
			Assert.AreEqual(2, computeCC.Output[0].Types[0].Methods.Count);
			Assert.AreEqual("SimpleMethod", computeCC.Output[0].Types[0].Methods[0].MethodInfo.Name);
			Assert.AreEqual(1, computeCC.Output[0].Types[0].Methods[0].CyclomaticComplexity);
			Assert.AreEqual("SimpleMethod2", computeCC.Output[0].Types[0].Methods[1].MethodInfo.Name);
			Assert.AreEqual(2, computeCC.Output[0].Types[0].Methods[1].CyclomaticComplexity);
			Assert.AreEqual("DummyClass2", computeCC.Output[0].Types[1].Type.Name);
			Assert.AreEqual(2, computeCC.Output[0].Types[1].Methods.Count);
			Assert.AreEqual("Test", computeCC.Output[0].Types[1].Methods[0].MethodInfo.Name);
			Assert.AreEqual(1, computeCC.Output[0].Types[1].Methods[0].CyclomaticComplexity);
			Assert.AreEqual("PrivateMethod", computeCC.Output[0].Types[1].Methods[1].MethodInfo.Name);
			Assert.AreEqual(1, computeCC.Output[0].Types[1].Methods[1].CyclomaticComplexity);
		}
	}
}
