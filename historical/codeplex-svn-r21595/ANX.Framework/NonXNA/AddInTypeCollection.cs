using System;
using System.Collections.Generic;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA
{
	internal class AddInTypeCollection : IEnumerable<AddIn>
	{
		private List<AddIn> availableSystems;

		#region Public
		public string PreferredName
		{
			get;
			set;
		}
		
		public bool PreferredLocked
		{
			get;
			private set;
		}
		#endregion

		#region Constructor
		public AddInTypeCollection()
		{
			availableSystems = new List<AddIn>();
		}
		#endregion

		#region Add
		public void Add(AddIn addIn)
		{
			availableSystems.Add(addIn);
		}
		#endregion

		#region Sort
		public void Sort()
		{
			availableSystems.Sort();
		}
		#endregion

		#region Lock
		public void Lock()
		{
			PreferredLocked = true;
		}
		#endregion

		#region GetDefaultCreator
		public T GetDefaultCreator<T>(AddInType addInType) where T : class, ICreator
		{
            Sort();

			if (String.IsNullOrEmpty(PreferredName))
			{
                for (int i = 0; i < availableSystems.Count; i++)
                {
                    T candidate = availableSystems[i].Instance as T;
                    if (candidate != null && candidate.IsSupported)
                    {
                        return candidate;
                    }
                }

				throw new AddInLoadingException(String.Format(
					"Couldn't get default {0} because there are no " +
					"registered and supported {0}s available! Make sure you referenced a {0} library " +
					"in your project or one is laying in your output folder!", addInType));
			}
			else
			{
				foreach (AddIn addin in availableSystems)
					if (addin.Name.Equals(PreferredName, StringComparison.CurrentCultureIgnoreCase))
						return addin.Instance as T;

				throw new AddInLoadingException(String.Format(
					"Couldn't get default {0} '{1}' because it was not found in the list of registered creators!",
					addInType, PreferredName));
			}

			throw new AddInLoadingException(String.Format("Couldn't find a DefaultCreator of type '{0}'!", typeof(T).FullName));
		}
		#endregion

        public IEnumerator<AddIn> GetEnumerator()
        {
            return availableSystems.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return availableSystems.GetEnumerator();
        }
    }
}
