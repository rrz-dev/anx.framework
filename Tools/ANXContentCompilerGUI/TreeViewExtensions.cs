#region Using Statements
using System;
using System.Windows.Forms;
using ANX.Framework.NonXNA.Development;
#endregion

// This file is part of the EES Content Compiler 4,
// © 2008 - 2012 by Eagle Eye Studios.
// The EES Content Compiler 4 is released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.ContentCompiler.GUI
{
    [Developer("SilentWarrior/Eagle Eye Studios")]
    [PercentageComplete(100)]
    [TestState(TestStateAttribute.TestState.Tested)]
    public static class TreeViewExtensions
    {
        /// <summary>
        /// Performs a recursive search on this TreeView's nodes and its child nodes. Returns the first item found.
        /// </summary>
        /// <param name="treeView">The TreeView</param>
        /// <param name="name">Name to search for</param>
        /// <returns>The first node matching the given criteria or null</returns>
        public static TreeNode RecursiveSearch(this TreeView treeView, String name)
        {
            foreach (TreeNode treeNode in treeView.Nodes)
            {
                if (treeNode.Name.Equals(name))
                    return treeNode;

                TreeNode retNode = treeNode.RecursiveSearch(name);
                if (retNode != null)
                {
                    return retNode;
                }
            }
            return null;
        }

        /// <summary>
        /// Performs a recursive search on this TreeNode's nodes and their child nodes. Returns the first item found.
        /// </summary>
        /// <param name="treeNode">The TreeNode</param>
        /// <param name="name">Name to search for</param>
        /// <returns>The first node matching the given criteria or null</returns>
        public static TreeNode RecursiveSearch(this TreeNode treeNode, String name)
        {
            foreach (TreeNode node in treeNode.Nodes)
            {
                if (node.Name.Equals(name))
                    return node;

                TreeNode ret = node.RecursiveSearch(name);
                if (ret != null)
                    return ret;
            }
            return null;
        }

        /// <summary>
        /// Recursively replaces all parts of the names/texts with new values.
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="old"></param>
        /// <param name="newString"></param>
        public static void RecursivelyReplacePartOfName(this TreeNode tree, string old, string newString)
        {
            tree.Name = tree.Name.Replace(old, newString);
            tree.Text = tree.Text.Replace(old, newString);
            foreach (TreeNode node in tree.Nodes)
            {
                node.RecursivelyReplacePartOfName(old, newString);
            }
        }
    }
}