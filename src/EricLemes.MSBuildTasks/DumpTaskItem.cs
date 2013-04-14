using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Build.Utilities;
using Microsoft.Build.Framework;

namespace EricLemes.MSBuildTasks
{
	public class DumpTaskItem : Task
	{
		public ITaskItem[] InputFiles
		{
			get;
			set;
		}

		public override bool Execute()
		{
			for (int i = 0; i < InputFiles.Length - 1; i++)
			{
				ITaskItem item = InputFiles[i];
				Log.LogMessage((i + 1).ToString() + ": " + item.ItemSpec);
				foreach (string m in item.MetadataNames)
					Log.LogMessage(m + "=" + item.GetMetadata(m));
			}
			return true;
		}
	}
}
