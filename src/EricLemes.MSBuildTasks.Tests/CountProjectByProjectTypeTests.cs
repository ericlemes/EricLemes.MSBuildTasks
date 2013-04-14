using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using EricLemes.MSBuildTasks.Tests.Resources;
using System.Xml;

namespace EricLemes.MSBuildTasks.Tests
{
	[TestClass]
	public class CountProjectByProjectTypeTests 
	{
		private Stream _projectFileStream;

		[TestInitialize]
		public void Initialize()
		{
			_projectFileStream = new MemoryStream();			
			StreamWriter sw = new StreamWriter(_projectFileStream);
			sw.Write(TestResources.testproject);
			_projectFileStream.Seek(0, SeekOrigin.Begin);
		}

		[TestMethod]
		public void TestGetProjectType()
		{
			Assert.AreEqual("Library", CountProjectsByProjectType.GetProjectType(_projectFileStream));
		}

		[TestMethod]
		[ExpectedException(typeof(XmlException))]
		public void TestGetProjectTypeEmptyStream()
		{
			MemoryStream ms = new MemoryStream();
			CountProjectsByProjectType.GetProjectType(ms);
		}

		[TestMethod]
		[ExpectedException(typeof(XmlException))]
		public void TestGetProjectTypeInvalidStream()
		{
			MemoryStream ms = new MemoryStream();
			StreamWriter sw = new StreamWriter(ms);
			sw.WriteLine("blah");
			sw.WriteLine("blah");
			sw.WriteLine("blah");
			CountProjectsByProjectType.GetProjectType(ms);
		}
	}
}
