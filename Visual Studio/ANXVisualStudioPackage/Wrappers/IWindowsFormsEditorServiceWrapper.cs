using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace ANX.Framework.VisualStudio
{
    class IWindowsFormsEditorServiceWrapper : IWindowsFormsEditorService
    {
        private class Proxy : MarshalByRefObject, IProxy
        {
            private IWindowsFormsEditorService service;

            public void Initialize(IWindowsFormsEditorService service)
            {
                this.service = service;
            }

            public override object InitializeLifetimeService()
            {
                return null;
            }

            public object OriginalInstance
            {
                get { return service; }
            }

            public Type WrapperType
            {
                get { return typeof(IWindowsFormsEditorServiceWrapper); }
            }

            public void CloseDropDown()
            {
                service.CloseDropDown();
            }

            public void DropDownControl(Control control)
            {
                service.DropDownControl(control);
            }

            public DialogResult ShowDialog(Form dialog)
            {
                return dialog.ShowDialog();
                //return service.ShowDialog(dialog);
            }
        }

        Proxy proxy;

        public static IProxy CreateProxy(IWindowsFormsEditorService service)
        {
            if (AppDomain.CurrentDomain.IsBuildAppDomain())
                throw new InvalidOperationException(string.Format("{0} proxies can only be created on the visual studio appDomain.", typeof(IWindowsFormsEditorServiceWrapper).Name));

            Proxy proxy = new Proxy();
            proxy.Initialize(service);

            return proxy;
        }

        public IWindowsFormsEditorServiceWrapper(IProxy proxy)
        {
            if (proxy == null)
                throw new ArgumentNullException("proxy");

            if (proxy.GetType() != typeof(Proxy))
                throw new ArgumentException(string.Format("The given proxy must be of type {0}.", typeof(Proxy).FullName));

            this.proxy = (Proxy)proxy;
        }

        public void CloseDropDown()
        {
            proxy.CloseDropDown();
        }

        public void DropDownControl(Control control)
        {
            //Controls can be marshalled by reference.
            proxy.DropDownControl(control);
        }

        public DialogResult ShowDialog(Form dialog)
        {
            return proxy.ShowDialog(dialog);
        }
    }
}
