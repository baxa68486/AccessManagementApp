using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccessManagApp
{
    public static class NodeExtensions
    {
        public static Node Previous(this Node node)
        {
            if (node == null)
                return null;

            //root
            if (node.Parent == null)
            {
                return null;
            }

            var parent = node.Parent;

            // TODO Implement extension method here
            bool found = false;
            /*
             * It could be optimizeed using IndexOf(), but conversion children ToList() costs O(n)
             * 
            var ind = parent.Children.ToList().IndexOf(node);
            if (ind == 0)
                return parent;
            */

            return GetPreviousNode(node, parent, ref found);
        }

        private static Node GetPreviousNode(Node node, Node prev, ref bool found)
        {
            foreach(var child in prev.Children)
            {
                if (found)
                    return prev;
                else if (child.Equals(node))
                {
                    found = true;
                    return prev;
                }
                else if (child.Children.Count() > 0)
                {
                    prev = GetPreviousNode(node, child, ref found);
                }
                else
                {
                    prev = child;
                }
            }
            
            return prev;
        }
    }
}
