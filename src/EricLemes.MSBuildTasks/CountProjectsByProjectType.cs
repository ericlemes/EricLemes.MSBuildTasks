using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Build.Utilities;
using System.IO;
using System.Xml;
using Microsoft.Build.Framework;

namespace EricLemes.MSBuildTasks
{
	public class CountProjectsByProjectType : Task
	{
		public ITaskItem[] InputFiles
		{
			get;
			set;
		}

		private Dictionary<string, int> _output = new Dictionary<string, int>();
		public Dictionary<string, int> Output
		{
			get { return _output; }
		}

		public static string GetProjectType(Stream st)
		{
			XmlTextReader rd = new XmlTextReader(st);
			using (rd)
			{
				if (!rd.IsStartElement())
					return "";
				rd.ReadStartElement("Project");
				rd.ReadToNextSibling("PropertyGroup");
				rd.ReadStartElement("PropertyGroup");
				rd.ReadToNextSibling("OutputType");
				return rd.ReadString();
			}
		}

		public override bool Execute()
		{
			foreach (ITaskItem i in InputFiles)
			{
				string file = i.GetMetadata("FullPath");
				FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
				string projectType = GetProjectType(fs);

				if (!_output.ContainsKey(projectType))
					_output.Add(projectType, 0);
				_output[projectType] += 1;
			}

			Log.LogMessage("Output");
			Log.LogMessage("======");
			foreach (KeyValuePair<string, int> p in _output)
				Log.LogMessage(p.Key + ": " + p.Value.ToString());

			return true;
		}
	}
}
