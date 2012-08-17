using System.Collections.Generic;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace HLSLParser
{
	public class EffectFile
	{
		#region Public
		public string Source
		{
			get;
			private set;
		}

		public List<Variable> Variables
		{
			get;
			private set;
		}

		public List<Structure> Structures
		{
			get;
			private set;
		}

		public List<TypeDef> TypeDefs
		{
			get;
			private set;
		}

		public List<Technique> Techniques
		{
			get;
			private set;
		}

		public List<EffectBuffer> Buffers
		{
			get;
			private set;
		}

		public List<Sampler> Samplers
		{
			get;
			private set;
		}

		public List<Method> Methods
		{
			get;
			private set;
		}
		#endregion

		#region Constructor
		internal EffectFile(string setSource)
		{
			Source = setSource;
			Variables = new List<Variable>();
			Structures = new List<Structure>();
			TypeDefs = new List<TypeDef>();
			Techniques = new List<Technique>();
			Buffers = new List<EffectBuffer>();
			Samplers = new List<Sampler>();
			Methods = new List<Method>();
		}
		#endregion
	}
}
