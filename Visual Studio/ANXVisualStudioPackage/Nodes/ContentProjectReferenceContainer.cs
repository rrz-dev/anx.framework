using ANX.Framework.Build;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Project;
using Microsoft.VisualStudio.Project.Automation;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ANX.Framework.VisualStudio.Nodes;
using ANX.Framework.Content.Pipeline.Tasks.References;
using System.Diagnostics;

namespace ANX.Framework.VisualStudio.Nodes
{
    class ContentProjectReferenceContainer : ReferenceContainerNode
    {
        ContentProjectNode contentProjectNode;

        public bool Loading
        {
            get;
            set;
        }

        protected void SetProjectDirty()
        {
            if (Loading)
                return;

            this.ProjectMgr.SetProjectFileDirty(true);
        }

        public ContentProjectReferenceContainer(ContentProjectNode root)
            : base(root)
        {
            this.contentProjectNode = root;

            this.OnChildAdded += ContentProjectReferenceContainer_OnChildAdded;
            this.OnChildRemoved += ContentProjectReferenceContainer_OnChildRemoved;
        }

        public void LoadReferencesFromContentProject()
        {
            Loading = true;

            try
            {
                foreach (var item in contentProjectNode.ContentProject.References)
                {
                    ReferenceNode node = null;
                    if (item is AssemblyReference)
                    {
                        /*var element = new MsBuildProjectElement(this.ProjectMgr, AssemblyName.GetAssemblyName(item.AssemblyPath).FullName, ProjectFileConstants.ProjectReference);
                    
                        element.SetMetadata(ProjectFileConstants.Name, item.Name);
                        element.SetMetadata(ProjectFileConstants.HintPath, item.AssemblyPath);
                        element.SetMetadata(ProjectFileConstants.Private, false.ToString()); //By default, never copy the assembly to the output directory for content projects, even if it's not a framework assembly.
                        element.SetMetadata(ProjectFileConstants.AssemblyName, Path.GetFileName(item.AssemblyPath));
                        node = CreateReferenceNode(ProjectFileConstants.Reference, element);*/

                        var reference = (AssemblyReference)item;

                        node = CreateAssemblyReferenceNode(reference.Name, reference.AssemblyPath);
                    }
                    else if (item is GACReference)
                    {
                        var reference = (GACReference)item;

                        node = CreateAssemblyReferenceNode(reference.Name, reference.AssemblyName);
                    }
                    else if (item is ProjectReference)
                    {
                        var reference = (ProjectReference)item;

                        /*var element = new MsBuildProjectElement(this.ProjectMgr, reference.Include, ProjectFileConstants.ProjectReference);
                        element.SetMetadata(ProjectFileConstants.Project, reference.Guid.ToString());
                        element.SetMetadata(ProjectFileConstants.Name, item.Name);
                        element.SetMetadata(ProjectFileConstants.Private, false.ToString()); //By default, never copy the assembly to the output directory for content projects, even if it's not a framework assembly.
                        node = CreateReferenceNode(ProjectFileConstants.ProjectReference, element);*/

                        string path = string.Empty;
                        string uniqueName = string.Empty;
                        try
                        {
                            path = CommonUtils.GetAbsoluteFilePath(Path.GetDirectoryName(this.ProjectMgr.Url), reference.Include);
                        }
                        catch { }
                        try
                        {
                            uniqueName = reference.Guid.ToString("B") + "|" + CommonUtils.TrimUpPaths(reference.Include);
                        }
                        catch { }

                        VSCOMPONENTSELECTORDATA selectorData = new VSCOMPONENTSELECTORDATA()
                        {
                            bstrTitle = reference.Name,
                            bstrFile = path,
                            bstrProjRef = uniqueName
                        };
                        node = CreateProjectReferenceNode(selectorData);
                    }

                    if (node != null)
                    {


                        // Make sure that we do not want to add the item twice to the ui hierarchy
                        // We are using here the UI representation of the Node namely the Caption to find that out, in order to
                        // avoid different representation problems.
                        // Example :<Reference Include="EnvDTE80, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" />
                        //		  <Reference Include="EnvDTE80" />
                        bool found = false;
                        for (HierarchyNode n = this.FirstChild; n != null && !found; n = n.NextSibling)
                        {
                            if (String.Compare(n.Caption, node.Caption, StringComparison.OrdinalIgnoreCase) == 0)
                            {
                                found = true;
                            }
                        }

                        if (!found)
                        {
                            this.AddChild(node);
                        }
                    }
                }

                //After all references have been added, we can load them.
                this.LoadAssemblies(this.EnumReferences().ToArray());
            }
            finally
            {
                Loading = false;
            }
        }

        void ContentProjectReferenceContainer_OnChildAdded(object sender, HierarchyNodeEventArgs e)
        {
            this.SetProjectDirty();

            //Wait with loading the refernces until the project finished loading.
            if (this.Loading)
                return;

            LoadReference((ReferenceNode)e.Child);
        }

        void LoadReference(ReferenceNode node)
        {
            if (node is AnxAssemblyReferenceNode)
            {
                var assemblyReference = (AnxAssemblyReferenceNode)node;
                assemblyReference.ItemNode.SetMetadata(ProjectFileConstants.Private, false.ToString()); //By default, never copy the assembly to the output directory for content projects, even if it's not a framework assembly.

                if (assemblyReference.IsValid)
                {
                    //Keep the ui thread responsive while the tasks battle over the lock for the appDomain.
                    LoadAssemblies(assemblyReference);
                }
            }
            else if (node is AnxProjectReferenceNode)
            {
                var projectReference = (AnxProjectReferenceNode)node;
                projectReference.ItemNode.SetMetadata(ProjectFileConstants.Private, false.ToString()); //By default, never copy the assembly to the output directory for content projects, even if it's not a framework assembly.

                if (File.Exists(projectReference.ReferencedProjectOutputPath))
                {
                    LoadAssemblies(projectReference);
                }
            }
        }
        
        void ContentProjectReferenceContainer_OnChildRemoved(object sender, HierarchyNodeEventArgs e)
        {
            this.SetProjectDirty();

            this.RefreshAssemblies(new List<HierarchyNode>(new [] { e.Child }));
        }

        public void RefreshAssemblies(IEnumerable<HierarchyNode> exclude = null)
        {
            using (var buildDomain = this.contentProjectNode.BuildAppDomain.Aquire())
            {
                buildDomain.Unload();
                buildDomain.Initialize(this.contentProjectNode.ProjectGuid.ToString());
            }

            List<ReferenceNode> referencesToRefresh = new List<ReferenceNode>();
            foreach (ReferenceNode reference in this.EnumReferences())
            {
                if (exclude != null && exclude.Contains(reference))
                    continue;

                if (reference is AnxAssemblyReferenceNode)
                {
                    var assemblyReference = (AnxAssemblyReferenceNode)reference;
                    assemblyReference.ItemNode.SetMetadata(ProjectFileConstants.Private, false.ToString()); //By default, never copy the assembly to the output directory for content projects, even if it's not a framework assembly.

                    if (assemblyReference.IsValid)
                    {
                        referencesToRefresh.Add(reference);
                    }
                }
                else if (reference is AnxProjectReferenceNode)
                {
                    var projectReference = (AnxProjectReferenceNode)reference;
                    projectReference.ItemNode.SetMetadata(ProjectFileConstants.Private, false.ToString()); //By default, never copy the assembly to the output directory for content projects, even if it's not a framework assembly.

                    if (File.Exists(projectReference.ReferencedProjectOutputPath))
                    {
                        referencesToRefresh.Add(reference);
                    }
                }
            }

            if (referencesToRefresh.Count > 0)
            {
                LoadAssemblies(referencesToRefresh.ToArray());
            }
        }

        public Task LoadAssemblies(params ReferenceNode[] references)
        {
            return Task.Run(() =>
            {
                var allReferences = this.EnumNodesOfType<AnxAssemblyReferenceNode>().ToArray();

                using (var buildDomain = this.contentProjectNode.BuildAppDomain.Aquire())
                {
                    Dictionary<string, string> assemblyNamesAndPaths = new Dictionary<string, string>();
                    foreach (var reference in references)
                    {
                        //Running asynchronously, so at least write to console.
                        //And we also don't want to stop at the first reference that creates problems.
                        try
                        {
                            string path = null;
                            string name = null;
                            if (reference is AnxAssemblyReferenceNode)
                            {
                                var assemblyReference = (AnxAssemblyReferenceNode)reference;
                                assemblyReference.RefreshReference();

                                if (assemblyReference.AssemblyName == null)
                                    continue;

                                path = assemblyReference.FullPath;
                                name = assemblyReference.AssemblyName.FullName;

                                if (string.IsNullOrEmpty(path))
                                    path = name;
                            }
                            else if (reference is AnxProjectReferenceNode)
                            {
                                var projectReference = (AnxProjectReferenceNode)reference;
                                projectReference.RefreshReference();

                                path = projectReference.ReferencedProjectOutputPath;
                                if (File.Exists(path))
                                {
                                    name = AssemblyName.GetAssemblyName(path).FullName;
                                }
                            }

                            if (name != null && path != null && File.Exists(path))
                            {
                                foreach (var assemblyName in buildDomain.Proxy.GetReferencedAssemblies(path))
                                {
                                    if (!assemblyNamesAndPaths.ContainsKey(assemblyName.FullName))
                                    {
                                        string assemblyIdentifier = assemblyName.FullName;

                                        foreach (var referenceEntry in allReferences)
                                        {
                                            if (referenceEntry.AssemblyName == null || !File.Exists(referenceEntry.Url))
                                                continue;

                                            //Strong assembly name, try to do an exact match for these.
                                            //If it hasn't a strong name, just match assemblies with the same name and don't care about the version.
                                            if (referenceEntry.AssemblyName.GetPublicKey() != null)
                                            {
                                                if (referenceEntry.AssemblyName.FullName == assemblyName.FullName)
                                                {
                                                    assemblyIdentifier = referenceEntry.Url;
                                                    break;
                                                }
                                            }
                                            else if (referenceEntry.AssemblyName.Name == assemblyName.Name)
                                            {
                                                assemblyIdentifier = referenceEntry.Url;
                                                break;
                                            }
                                        }

                                        assemblyNamesAndPaths[assemblyName.FullName] = assemblyIdentifier;
                                    }
                                }

                                assemblyNamesAndPaths[name] = path;
                            }
                        }
                        catch (Exception exc)
                        {
                            Debugger.Break();
                            Console.WriteLine(exc.Message);
                        }
                    }

                    try
                    {
                        var callback = new LoadAssembliesCallback(this);
                        buildDomain.Proxy.LoadProjectAssemblies(assemblyNamesAndPaths.Values, buildDomain.SearchPaths, buildDomain.Redirects, callback.ErrorCallback);
                    }
                    catch (Exception exc)
                    {
                        Debugger.Break();
                        Console.WriteLine(exc.Message);
                    }
                }
            });
        }

        class LoadAssembliesCallback : MarshalByRefObject
        {
            ContentProjectReferenceContainer parent;

            internal LoadAssembliesCallback(ContentProjectReferenceContainer parent)
            {
                this.parent = parent;
            }

            public void ErrorCallback(string assembly, Exception exc)
            {
                ErrorLoggingHelper loggingHelper = new ErrorLoggingHelper(parent.contentProjectNode, parent.contentProjectNode.ErrorListProvider);

                loggingHelper.LogWarning(null, null, null, assembly, -1, -1, exc.Message);
            }
        }

        protected override AssemblyReferenceNode CreateAssemblyReferenceNode(ProjectElement element)
        {
            throw new NotSupportedException();
        }

        protected override AssemblyReferenceNode CreateAssemblyReferenceNode(string name, string fileName)
        {
            return new AnxAssemblyReferenceNode(this.contentProjectNode, name, fileName);
        }

        protected override ProjectReferenceNode CreateProjectReferenceNode(ProjectElement element)
        {
            throw new NotSupportedException();
        }

        protected override ProjectReferenceNode CreateProjectReferenceNode(VSCOMPONENTSELECTORDATA selectorData)
        {
            return new AnxProjectReferenceNode(this.contentProjectNode, selectorData.bstrTitle, selectorData.bstrFile, selectorData.bstrProjRef);
        }
    }
}
