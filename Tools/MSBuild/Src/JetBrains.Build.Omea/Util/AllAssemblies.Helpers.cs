﻿/// <copyright company="JetBrains">
/// Copyright © 2003-2008 JetBrains s.r.o.
/// You may distribute under the terms of the GNU General Public License, as published by the Free Software Foundation, version 2 (see License.txt in the repository root folder).
/// </copyright>

// The partial helpers file for the autogenerated AllAssemblies.cs file
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace JetBrains.Build.AllAssemblies
{
	public partial class AllAssembliesXml
	{
		#region Attributes

		/// <summary>
		/// Gets the XSD.
		/// </summary>
		public static XmlSchema XmlSchema
		{
			get
			{
				using(Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("JetBrains.Build.AllAssemblies.AllAssemblies.xsd"))
					return XmlSchema.Read(stream, null);
			}
		}

		#endregion

		#region Operations

		public static AllAssembliesXml Load(Stream stream)
		{
			var settings = new XmlReaderSettings();
			settings.ValidationType = ValidationType.Schema;
			settings.Schemas.Add(XmlSchema);

			// Load the AllAssembliesXml, validating against the schema
			AllAssembliesXml retval;
			using(XmlReader xmlrValidating = XmlReader.Create(stream, settings))
				retval = (AllAssembliesXml)new XmlSerializer(typeof(AllAssembliesXml)).Deserialize(xmlrValidating);

			// Nullref guard
			if((retval.ItemGroup == null))
				retval.ItemGroup = new ItemGroupXml[] {};

			// Quick-validate the assembly names
			var mapAssemblyNames = new Dictionary<string, bool>();
			foreach(ItemGroupXml group in retval.ItemGroup)
			{
				if(group.AllAssemblies == null)
					continue;
				foreach(AssemblyXml assemblyxml in group.AllAssemblies)
				{
					if(string.IsNullOrEmpty(assemblyxml.Include))
						throw new InvalidOperationException(string.Format("Each assembly must specify its name in the Item's Include attribute."));
					if((assemblyxml.Include.IndexOf('*') >= 0) || (assemblyxml.Include.IndexOf('?') >= 0))
						throw new InvalidOperationException(string.Format("The assembly name “{0}” is not valid. The Include attribute must not contain wildcards.", assemblyxml.Include));
					if(mapAssemblyNames.ContainsKey(assemblyxml.Include))
						throw new InvalidOperationException(string.Format("Duplicate assembly “{0}”.", assemblyxml.Include));
					mapAssemblyNames.Add(assemblyxml.Include, true);
				}
			}

			return retval;
		}

		public static AllAssembliesXml LoadFrom(string filename)
		{
			if(filename == null)
				throw new ArgumentNullException("filename");
			if(!File.Exists(filename))
				throw new InvalidOperationException(string.Format("The specified file “{0}” does not exist.", filename));

			using(var stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
				return Load(stream);
		}

		#endregion
	}

	public partial class AssemblyXml
	{
	}
}