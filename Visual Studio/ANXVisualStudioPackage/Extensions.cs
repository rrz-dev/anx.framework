using EnvDTE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANX.Framework.VisualStudio
{
    public static class Extensions
    {
        public static bool IsBuildAppDomain(this AppDomain appDomain)
        {
            return appDomain.FriendlyName.StartsWith("ANX Project");
        }

        public static Uri MakeRelativeUri(this Uri instance, IEnumerable<Uri> candidates)
        {
            if (candidates == null)
                throw new ArgumentNullException("candidates");

            Uri uri;
            instance.TryMakeRelativeUri(candidates, out uri);
            return uri;
        }

        public static Uri MakeRelativeUri(this Uri instance, params Uri[] candidates)
        {
            Uri uri;
            instance.TryMakeRelativeUri(candidates, out uri);
            return uri;
        }

        public static bool TryMakeRelativeUri(this Uri instance, IEnumerable<Uri> candidates, out Uri result)
        {
            if (candidates == null)
                throw new ArgumentNullException("candidates");

            if (!instance.IsAbsoluteUri)
            {
                result = instance;
                return true;
            }

            foreach (Uri uri in candidates)
            {
                if (uri.IsBaseOf(instance))
                {
                    result = uri.MakeRelativeUri(instance);
                    return true;
                }
            }

            result = instance;
            return false;
        }

        public static IEnumerable<SolutionConfiguration> AsEnumerable(this SolutionConfigurations configurations)
        {
            var enumerator = configurations.GetEnumerator();
            while (enumerator.MoveNext())
            {
                yield return (SolutionConfiguration)enumerator.Current;
            }
        }

        public static IEnumerable<SolutionContext> AsEnumerable(this SolutionContexts contexts)
        {
            var enumerator = contexts.GetEnumerator();
            while (enumerator.MoveNext())
            {
                yield return (SolutionContext)enumerator.Current;
            }
        }
    }
}
