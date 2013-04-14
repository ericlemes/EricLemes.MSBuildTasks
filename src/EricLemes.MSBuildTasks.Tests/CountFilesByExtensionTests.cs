using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Build.Framework;
using EricLemes.MSBuildTasks.Tests.Mock;
using Microsoft.Build.Utilities;

namespace EricLemes.MSBuildTasks.Tests
{
	[TestClass]
	public class CountFilesByExtensionTests
	{
		private ITaskItem[] _taskItems;
		private IBuildEngine _buildEngine;

		[TestInitialize]
		public void Initialize()
		{
			_buildEngine = new BuildEngineMock();

			_taskItems = new TaskItemMock[3];
			_taskItems[0] = new TaskItemMock(@"..\src\EricLemes.MSBuildTasks.sln").
				AddMetadata("FullPath", @"C:\SandBox\EricLemes.MSBuildTasks\src\EricLemes.MSBuildTasks.sln").
				AddMetadata("RootDir", @"C:\").
				AddMetadata("Filename", @"EricLemes.MSBuildTasks").
				AddMetadata("Extension", @".sln").
				AddMetadata("RelativeDir", @"..\src\").
				AddMetadata("Directory", @"SandBox\EricLemes.MSBuildTasks\src\").
				AddMetadata("RecursiveDir", "").
				AddMetadata("Identity", @"..\src\EricLemes.MSBuildTasks.sln").
				AddMetadata("ModifiedTime", "2013-04-13 20:36:44.6254104").
				AddMetadata("CreatedTime", "2013-04-13 20:24:44.4392181").
				AddMetadata("AccessedTime", "2013-04-13 20:24:44.4392181");
			_taskItems[1] = new TaskItemMock(@"..\src\EricLemes.MSBuildTasks.suo").
				AddMetadata("FullPath", @"C:\SandBox\EricLemes.MSBuildTasks\src\EricLemes.MSBuildTasks.suo").
				AddMetadata("RootDir", @"C:\").
				AddMetadata("Filename", @"EricLemes.MSBuildTasks").
				AddMetadata("Extension", ".suo").
				AddMetadata("RelativeDir", @"..\src\").
				AddMetadata("Directory", @"SandBox\EricLemes.MSBuildTasks\src\").
				AddMetadata("RecursiveDir", "").
				AddMetadata("Identity", @"..\src\EricLemes.MSBuildTasks.suo").
				AddMetadata("ModifiedTime", "2013-04-13 20:36:44.7510000").
				AddMetadata("CreatedTime", "2013-04-13 20:24:44.4452185").
				AddMetadata("AccessedTime", "2013-04-13 20:24:44.4452185");
			_taskItems[2] = new TaskItemMock(@"..\src\EricLemes.MSBuildTasks2.sln").
				AddMetadata("FullPath", @"C:\SandBox\EricLemes.MSBuildTasks\src\EricLemes.MSBuildTasks2.sln").
				AddMetadata("RootDir", @"C:\").
				AddMetadata("Filename", @"EricLemes.MSBuildTasks").
				AddMetadata("Extension", @".sln").
				AddMetadata("RelativeDir", @"..\src\").
				AddMetadata("Directory", @"SandBox\EricLemes.MSBuildTasks\src\").
				AddMetadata("RecursiveDir", "").
				AddMetadata("Identity", @"..\src\EricLemes.MSBuildTasks.sln").
				AddMetadata("ModifiedTime", "2013-04-13 20:36:44.6254104").
				AddMetadata("CreatedTime", "2013-04-13 20:24:44.4392181").
				AddMetadata("AccessedTime", "2013-04-13 20:24:44.4392181");
		}

		[TestMethod]
		public void TestCountFilesByExtension()
		{						
			CountFilesByExtension task = new CountFilesByExtension();
			TaskLoggingHelper _loggingHelper = new TaskLoggingHelper(task);
			task.BuildEngine = _buildEngine;

			task.InputFiles = _taskItems;
			task.Execute();
			Assert.IsTrue(task.Output.ContainsKey(".sln"));
			Assert.IsTrue(task.Output.ContainsKey(".suo"));
			Assert.AreEqual(2, task.Output[".sln"]);
			Assert.AreEqual(1, task.Output[".suo"]);
		}
	}
}
