﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Build.Framework;

namespace EricLemes.MSBuildTasks.Tests.Mock
{
	public class TaskItemMock : ITaskItem
	{
		private string _itemSpec;
		private Dictionary<string, string> _metadatas = new Dictionary<string, string>();

		public System.Collections.IDictionary CloneCustomMetadata()
		{
			Dictionary<string, string> clone = new Dictionary<string,string>();
			foreach(KeyValuePair<string, string> pair in _metadatas)			
			{
				clone.Add(pair.Key, pair.Value);
			}
			return clone;
		}

		public void CopyMetadataTo(ITaskItem destinationItem)
		{
			throw new NotImplementedException();
		}

		public string GetMetadata(string metadataName)
		{
			return _metadatas[metadataName];
		}

		public string ItemSpec
		{
			get {return _itemSpec;}
			set {_itemSpec = value;}			
		}

		public int MetadataCount
		{
			get { return _metadatas.Count;}			
		}

		public System.Collections.ICollection MetadataNames
		{
			get { return _metadatas.Keys; }
		}

		public void RemoveMetadata(string metadataName)
		{
			_metadatas.Remove(metadataName);
		}

		public void SetMetadata(string metadataName, string metadataValue)
		{
			if (_metadatas.ContainsKey(metadataName))
				_metadatas[metadataName] = metadataValue;
			else
				_metadatas.Add(metadataName, metadataValue);
		}

		public TaskItemMock(string itemSpec)
		{
			_itemSpec = itemSpec;
		}

		public TaskItemMock AddMetadata(string name, string value)
		{
			if (_metadatas.ContainsKey(name))
				_metadatas[name] = value;
			else
				_metadatas.Add(name, value);
			return this;
		}
	}
}
