using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Text;
using NUnit.Framework;
using AccessManagApp;

namespace AccessManagementAppTests
{
    public class NodeExtensionsTests
    {
        [Test]
        public void Test()
        {
            // Test tree:
            // 
            // 1
            // +-2
            //   +-3
            //   +-4
            // +-5
            //   +-6
            //   +-7
            //
            var lastNode = new Node(7);
            var tree = new Node(1,
                new Node(
                    2,
                    new Node(3),
                    new Node(4)),
                new Node(
                    5,
                    new Node(6),
                    lastNode));

            // Expected output:
            //
            // 7
            // 6
            // 5
            // 4
            // 3
            // 2
            // 1
            //
            var n = lastNode;
            while (n != null)
            {
                Console.WriteLine(n.Data);
                n = n.Previous();
            }

            // Test
            n = lastNode;
            Assert.AreEqual(7, n.Data);
            n = n.Previous();
            Assert.AreEqual(6, n.Data);
            n = n.Previous();
            Assert.AreEqual(5, n.Data);
            n = n.Previous();
            Assert.AreEqual(4, n.Data);
            n = n.Previous();
            Assert.AreEqual(3, n.Data);
            n = n.Previous();
            Assert.AreEqual(2, n.Data);
            n = n.Previous();
            Assert.AreEqual(1, n.Data);
            n = n.Previous();
            Assert.IsNull(n);
        }

        [Test]
        public void Previous_TreeContainsOnlyOneElement_ReturnNull()
        {
            var root = new Node(1);
            var previousNode = root.Previous();
            Assert.IsNull(previousNode);
        }


        [Test]
        public void Previous_DifferentInputs_ReturnsPreviousNode()
        {
            // Test tree:
            // 
            // 1
            // +-2
            //   +-3
            //   +-4
            // +-5
            //   +-6
            //   +-7
             //+-11         // +-10
                  //+-12        //  +-8
                  //+-13        //  +-9
            //
            var node12 = new Node(12);
            var node13 = new Node(13);

            var node10 = new Node(10);

            var tree = new Node(1,
                new Node(
                    2,
                    new Node(3),
                    new Node(4)),
                new Node(
                    5,
                    new Node(6),
                    new Node(7)),
                 new Node(
                    11,
                    node12,
                    node13),
                 node10
                 );

            var expectedNode = node12;
            var cur = node13.Previous();

            Assert.IsTrue(cur.Equals(expectedNode));

            expectedNode = node13;
            cur = node10.Previous();

            Assert.IsTrue(cur.Equals(expectedNode));
        }
    }
}
