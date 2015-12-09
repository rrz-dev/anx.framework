using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Project;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ANX.Framework.VisualStudio.Controls
{
    class BrowseFolderDialog : CommonDialog
    {
        public override void Reset()
        {
            
        }

        public string Title
        {
            get;
            set;
        }

        public string SelectedPath
        {
            get;
            set;
        }

        public string RootFolder
        {
            get;
            set;
        }

        protected override bool RunDialog(IntPtr hwndOwner)
        {
            if (this.Site == null)
                throw new InvalidOperationException("No object for Site set.");

            IVsUIShell shellService = this.Site.GetService(typeof(SVsUIShell)) as IVsUIShell;

            if (shellService == null)
                throw new InvalidOperationException(string.Format("Can't find {0} service.", typeof(IVsUIShell)));

            string selectedPath = this.SelectedPath;
            if (selectedPath == null)
                selectedPath = string.Empty;

            try
            {
                Uri selectedPathUri = new Uri(selectedPath, UriKind.RelativeOrAbsolute);
                if (!selectedPathUri.IsAbsoluteUri)
                    selectedPathUri = new Uri(new Uri(RootFolder), selectedPathUri);

                selectedPath = selectedPathUri.LocalPath;
            }
            catch { }

            if (selectedPath.Length > NativeMethods.MAX_PATH)
                throw new InvalidOperationException(string.Format("SelectedPath is longer than the native limit of {0}.", NativeMethods.MAX_PATH));



            VSBROWSEINFOW[] browseInfo = new VSBROWSEINFOW[1];

            var info = browseInfo[0];

            info.hwndOwner = hwndOwner;
            info.pwzDlgTitle = this.Title;
            info.pwzInitialDir = selectedPath;
            info.nMaxDirName = (uint)NativeMethods.MAX_PATH;
            info.lStructSize = (uint)Marshal.SizeOf(typeof(VSBROWSEINFOW));

            IntPtr path = Marshal.AllocCoTaskMem(NativeMethods.MAX_PATH);
            try
            {
                info.pwzDirName = path;

                browseInfo[0] = info;

                int result = shellService.GetDirectoryViaBrowseDlg(browseInfo);

                if (result == VSConstants.S_OK)
                {
                    SelectedPath = Marshal.PtrToStringAuto(browseInfo[0].pwzDirName);

                    return true;
                }
                else if (result == VSConstants.OLE_E_PROMPTSAVECANCELLED)
                {
                    return false;
                }
                else
                {
                    ErrorHandler.ThrowOnFailure(result);
                    return false;
                }
            }
            finally
            {
                if (path != IntPtr.Zero)
                {
                    Marshal.FreeCoTaskMem(path);
                }
            }
        }
    }
}
