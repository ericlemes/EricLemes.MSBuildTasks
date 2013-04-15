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

		private List<ComputeCyclomaticComplexityAssembly> _output = new List<ComputeCyclomaticComplexityAssembly>();
		public List<ComputeCyclomaticComplexityAssembly> Output
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

		private void ComputeAssembly(Assembly assembly)
		{
			ComputeCyclomaticComplexityAssembly a = new ComputeCyclomaticComplexityAssembly();
			a.Assembly = assembly;
			Output.Add(a);

			foreach (Type t in assembly.GetTypes())
			{
				ComputeCyclomaticComplexityType computeCcType = new ComputeCyclomaticComplexityType();
				computeCcType.Type = t;
				a.Types.Add(computeCcType);

				ComputeType(t, computeCcType);
			}
		}

		private void ComputeType(Type t, ComputeCyclomaticComplexityType computeCcType)
		{
			foreach (MethodInfo mi in t.GetMethods(BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly))
			{
				ComputeCyclomaticComplexityMethod computeCcMethod = new ComputeCyclomaticComplexityMethod();
				computeCcMethod.MethodInfo = mi;
				computeCcMethod.CyclomaticComplexity = mi.ComputeCc();
				computeCcType.Methods.Add(computeCcMethod);
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

			return true;
		}
	}

	public class ComputeCyclomaticComplexityAssembly
	{
		public Assembly Assembly
		{
			get;
			set;
		}

		private List<ComputeCyclomaticComplexityType> _types = new List<ComputeCyclomaticComplexityType>();
		public List<ComputeCyclomaticComplexityType> Types
		{
			get { return _types; }			
		}
	}

	public class ComputeCyclomaticComplexityType
	{
		public Type Type
		{
			get;
			set;
		}

		private List<ComputeCyclomaticComplexityMethod> _methods = new List<ComputeCyclomaticComplexityMethod>();
		public List<ComputeCyclomaticComplexityMethod> Methods
		{
			get { return _methods; }
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
