using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Build.Utilities;
using Microsoft.Build.Framework;
using System.Reflection;
using FluentCodeMetrics.Core;

namespace EricLemes.MSBuildTasks
{
	public class ComputeCyclomaticComplexity : Task
	{
		public ITaskItem[] InputFiles
		{
			get;
			set;
		}

		private Dictionary<MethodInfo, ComputeCyclomaticComplexityMethod> _processed = new Dictionary<MethodInfo, ComputeCyclomaticComplexityMethod>();

		private List<ComputeCyclomaticComplexityMethod> _output = new List<ComputeCyclomaticComplexityMethod>();
		public List<ComputeCyclomaticComplexityMethod> Output
		{
			get { return _output; }
		}

		private bool _showSummary = true;
		public bool ShowSummary
		{
			get { return _showSummary; }
			set { ShowSummary = value; }
		}

		private bool _showDetails = true;
		public bool ShowDetails
		{
			get { return _showDetails; }
			set { _showDetails = value; }
		}

		private int _detailsMinCC = 2;
		public int DetailsMinCC
		{
			get { return _detailsMinCC; }
			set { _detailsMinCC = value; }
		}

		private void ComputeAssembly(Assembly assembly)
		{
			foreach (Type t in assembly.GetTypes())
				ComputeType(t);
		}

		private void ComputeType(Type t)
		{
			foreach (MethodInfo mi in t.GetMethods(BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly))
			{
				if (_processed.ContainsKey(mi))
					continue;

				ComputeCyclomaticComplexityMethod computeCcMethod = new ComputeCyclomaticComplexityMethod();
				computeCcMethod.MethodInfo = mi;
				computeCcMethod.CyclomaticComplexity = mi.ComputeCc();
				Output.Add(computeCcMethod);

				_processed.Add(mi, computeCcMethod);
			}
		}

		public override bool Execute()
		{
			foreach (ITaskItem i in InputFiles)
			{
				string fullPath = i.GetMetadata("FullPath");
				Assembly a = Assembly.LoadFrom(fullPath);
				ComputeAssembly(a);
			}
			
			if (_showSummary)
				DisplaySummary();
			if (_showDetails)
				DisplayDetails();

			return true;
		}

		public void DisplaySummary()
		{
			Log.LogMessage("");
			Log.LogMessage("Summary of Cyclomatic Complexity");
			Log.LogMessage("================================");
			Log.LogMessage("> 100: " + _output.Where(m => m.CyclomaticComplexity > 100).Count().ToString());
			Log.LogMessage("100 > x > 50: " + _output.Where(m => m.CyclomaticComplexity < 100 && m.CyclomaticComplexity > 50).Count().ToString());
			Log.LogMessage("50 > x > 40: " + _output.Where(m => m.CyclomaticComplexity < 50 && m.CyclomaticComplexity > 40).Count().ToString());
			Log.LogMessage("40 > x > 30: " + _output.Where(m => m.CyclomaticComplexity < 40 && m.CyclomaticComplexity > 30).Count().ToString());
			Log.LogMessage("30 > x > 20: " + _output.Where(m => m.CyclomaticComplexity < 30 && m.CyclomaticComplexity > 20).Count().ToString());
			Log.LogMessage("20 > x > 10: " + _output.Where(m => m.CyclomaticComplexity < 20 && m.CyclomaticComplexity > 10).Count().ToString());
			Log.LogMessage("10 > x > 5: " + _output.Where(m => m.CyclomaticComplexity < 10 && m.CyclomaticComplexity > 5).Count().ToString());
			Log.LogMessage("5: " + _output.Where(m => m.CyclomaticComplexity == 5).Count().ToString());
			Log.LogMessage("4: " + _output.Where(m => m.CyclomaticComplexity == 4).Count().ToString());
			Log.LogMessage("3: " + _output.Where(m => m.CyclomaticComplexity == 3).Count().ToString());
			Log.LogMessage("2: " + _output.Where(m => m.CyclomaticComplexity == 2).Count().ToString());
		}

		public void DisplayDetails()
		{
			List<ComputeCyclomaticComplexityMethod> rankedList = _output.Where(m => m.CyclomaticComplexity >= _detailsMinCC).
				OrderByDescending(m => m.CyclomaticComplexity).ToList<ComputeCyclomaticComplexityMethod>();

			Log.LogMessage("");
			Log.LogMessage("Detailed Cyclomatic Complexity (CC: Assembly, Type, Method)");
			Log.LogMessage("===========================================================");
			foreach(ComputeCyclomaticComplexityMethod m in rankedList){
				Log.LogMessage(m.CyclomaticComplexity.ToString() + ": " + m.MethodInfo.ReflectedType.Assembly.ManifestModule.Name + ", " +
					m.MethodInfo.ReflectedType.Name + ", " + m.MethodInfo.Name);
			}
		}
	}

	public class ComputeCyclomaticComplexityMethod
	{
		public MethodInfo MethodInfo
		{
			get;
			set;
		}

		public int CyclomaticComplexity
		{
			get;
			set;
		}
	}
}
