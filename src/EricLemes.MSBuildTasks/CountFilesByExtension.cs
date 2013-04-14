using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Build.Utilities;
using Microsoft.Build.Framework;

namespace EricLemes.MSBuildTasks
{
	public class CountFilesByExtension : Task
	{
		public ITaskItem[] InputFiles
		{
			get;
			set;
		}

		private Dictionary<string, int> _output = new Dictionary<string, int>();
		public Dictionary<string, int> Output
		{
			get {return _output;}
		}	

		public override bool Execute()
		{			
			foreach (ITaskItem i in InputFiles)
			{
				string ext = i.GetMetadata("Extension");
				if (!_output.ContainsKey(ext))
					_output.Add(ext, 0);
				_output[ext] += 1;
			}

			Log.LogMessage("Output");
			Log.LogMessage("======");
			foreach (KeyValuePair<string, int> p in _output)
				Log.LogMessage(p.Key + ": " + p.Value.ToString());

			return true;
		}
	}
}
