using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ANX.ContentCompiler.GUI
{
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

                var retNode = treeNode.RecursiveSearch(name);
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

                var ret = node.RecursiveSearch(name);
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
