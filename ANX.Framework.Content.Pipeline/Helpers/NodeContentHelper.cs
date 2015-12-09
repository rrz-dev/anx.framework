using ANX.Framework.Content.Pipeline.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Helpers
{
    internal static class NodeContentHelper
    {
        public static IEnumerable<T> EnumNodesOfType<T>(NodeContent node) where T : NodeContent
        {
            if (node == null)
                yield break;

            if (node.GetType() == typeof(T))
                yield return (T)node;

            foreach (var child in node.Children)
            {
                foreach (var childChild in EnumNodesOfType<T>(child))
                    yield return childChild;
            }
        }

        public static IEnumerable<NodeContent> EnumNodes(NodeContent node)
        {
            if (node == null)
                yield break;

            yield return node;

            foreach (var child in node.Children)
            {
                foreach (var childChild in EnumNodes(child))
                    yield return childChild;
            }
        }
    }
}
