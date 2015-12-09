using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Tasks
{
    public interface IBuildCache
    {
        bool IsValid(BuildItem item, Uri outputFilePath);

        void Refresh(BuildItem item, Uri outputFilePath);

        void Clear();

        void Remove(Uri filePath);

        Uri ProjectHome
        {
            get;
            set;
        }
    }
}
