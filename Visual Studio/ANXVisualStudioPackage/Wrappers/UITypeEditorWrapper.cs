using ANX.Framework.Build;
using ANX.Framework.VisualStudio.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Linq;
using System.Text;

namespace ANX.Framework.VisualStudio
{
    public class UITypeEditorWrapper : UITypeEditor
    {
        private class Proxy : MarshalByRefObject, IProxy
        {
            private UITypeEditor editor;
            //Don't use WrappedConverters, we work on the same appDomain as them.
            private TypeConverter converter;
            private IProxy converterProxy;

            public Proxy()
            {
                if (!AppDomain.CurrentDomain.IsBuildAppDomain())
                    throw new InvalidOperationException("Proxies can only be created from inside the build AppDomain.");
            }

            public override object InitializeLifetimeService()
            {
                return null;
            }

            public TypeConverter Converter
            {
                get { return converter; }
            }

            public IProxy ConverterProxy
            {
                get { return converterProxy; }
            }

            public void Initialize(UITypeEditor editor, IProxy converterProxy)
            {
                if (editor == null)
                    throw new ArgumentNullException("editor");

                if (converterProxy == null)
                    throw new ArgumentNullException("converterProxy");

                this.editor = editor;
                this.converterProxy = converterProxy;
                this.converter = new WrappedConverter(converterProxy);
            }

            public object EditValue(IProxy context, string value)
            {
                var wrappedContext = new TypeDescriptorContextWrapper(context);

                return editor.EditValue(wrappedContext, wrappedContext, this.converter.ConvertFromString(wrappedContext, CultureInfo.CurrentUICulture, value));
            }

            public UITypeEditorEditStyle GetEditStyle(IProxy context)
            {
                return editor.GetEditStyle(new TypeDescriptorContextWrapper(context));
            }

            public bool GetPaintValueSupported(IProxy context)
            {
                return editor.GetPaintValueSupported(new TypeDescriptorContextWrapper(context));
            }

            public bool IsDropDownResizable
            {
                get
                {
                    return editor.IsDropDownResizable;
                }
            }

            public void PaintValue(PaintValueEventArgsMarshallable e)
            {
                var wrappedContext = new TypeDescriptorContextWrapper(e.ContextProxy);

                editor.PaintValue(new PaintValueEventArgs(wrappedContext, converter.ConvertFromString(wrappedContext, CultureInfo.CurrentUICulture, e.Value), e.Graphics, e.Bounds));
            }

            public object OriginalInstance
            {
                get { return editor; }
            }

            public Type WrapperType
            {
                get { return typeof(UITypeEditorWrapper); }
            }
        }

        [Serializable]
        private class PaintValueEventArgsMarshallable
        {
            public PaintValueEventArgsMarshallable(IProxy contextProxy, string value, System.Drawing.Graphics graphics, System.Drawing.Rectangle bounds)
            {
                this.ContextProxy = contextProxy;
                this.Value = value;
                this.Graphics = graphics;
                this.Bounds = bounds;
            }

            public System.Drawing.Rectangle Bounds
            {
                get;
                private set;
            }

            public IProxy ContextProxy
            {
                get;
                private set;
            }

            public System.Drawing.Graphics Graphics
            {
                get;
                private set;
            }

            public string Value
            {
                get;
                private set;
            }
        }

        /// <summary>
        /// Creates a new instance of the proxy for UITypeEditors.
        /// Should only be called from inside the BuildAppDomain
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="converter"></param>
        /// <returns></returns>
        public static IProxy CreateProxy(UITypeEditor editor, IProxy converter)
        {
            if (!AppDomain.CurrentDomain.IsBuildAppDomain())
                throw new InvalidOperationException("Proxies for editors can only be created from inside the BuildAppDomain.");

            var proxy = new Proxy();
            proxy.Initialize(editor, converter);

            return proxy;
        }

        private Proxy proxy;
        private WrappedConverter converter;

        public UITypeEditorWrapper(UITypeEditor editor, BuildAppDomain appDomain, WrappedConverter converter)
        {
            if (editor == null)
                throw new ArgumentNullException("editor");

            if (appDomain == null)
                throw new ArgumentNullException("appDomain");

            if (converter == null)
                throw new ArgumentNullException("converter");

            using (var domainLock = appDomain.Aquire())
            {
                this.proxy = domainLock.CreateInstanceAndUnwrap<Proxy>();
            }
            this.proxy.Initialize(editor, converter.ProxyInstance);
            this.converter = converter;
        }

        public UITypeEditorWrapper(UITypeEditor editor, WrappedConverter converter)
        {
            if (editor == null)
                throw new ArgumentNullException("editor");

            if (converter == null)
                throw new ArgumentNullException("converter");

            this.proxy = new Proxy();

            this.proxy.Initialize(editor, converter.ProxyInstance);
            this.converter = converter;
        }

        public UITypeEditorWrapper(IProxy proxy)
        {
            if (proxy == null)
                throw new ArgumentNullException("proxy");

            if (proxy.GetType() != typeof(Proxy))
                throw new ArgumentException(string.Format("The given proxy must be of type {0}.", typeof(Proxy).FullName));

            this.proxy = (Proxy)proxy;
            this.converter = new WrappedConverter(this.proxy.ConverterProxy);
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            var contextProxy = TypeDescriptorContextWrapper.CreateProxy(context, converter.ProxyInstance);

            return proxy.EditValue(contextProxy, this.converter.ConvertToString(context, CultureInfo.CurrentUICulture, value));
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return proxy.GetEditStyle(TypeDescriptorContextWrapper.CreateProxy(context, converter.ProxyInstance));
        }

        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            return proxy.GetPaintValueSupported(TypeDescriptorContextWrapper.CreateProxy(context, converter.ProxyInstance));
        }

        public override bool IsDropDownResizable
        {
            get
            {
                return proxy.IsDropDownResizable;
            }
        }

        public override void PaintValue(PaintValueEventArgs e)
        {
            var contextProxy = TypeDescriptorContextWrapper.CreateProxy(e.Context, converter.ProxyInstance);

            proxy.PaintValue(new PaintValueEventArgsMarshallable(contextProxy, converter.ConvertToString(e.Context, CultureInfo.CurrentUICulture, e.Value), e.Graphics, e.Bounds));
        }
    }
}
