using System.IO;
using NUnit.Framework;
using VDC.Core;

namespace VDC.Tests {
    public class DigraphTests {
        private DirectedGraph _graph;
        private string _moduleConfigurations;
        [SetUp]
        public void Setup () {
            _graph = new DirectedGraph ();
            _moduleConfigurations = 
                File.ReadAllText ("C:\\source_code\\azure-virtual-datacenter\\topological-sort\\tests\\samples\\moduleConfigurations.json");
            
        }

        [Test]
        public void GenerateGraph () 
        {
            _graph.Generate(_moduleConfigurations);
            Assert.NotNull(_graph.Vertices);
            Assert.IsNotEmpty(_graph.Vertices);
        }

        [Test]
        public void DFS () 
        {
            _graph.Generate(_moduleConfigurations);
            _graph.DFS();
            Assert.NotNull(_graph.Vertices);
            _graph.Vertices.ForEach(v => {
                Assert.IsTrue(v.Color == Color.Black);
            });
        }

        [Test]
        public void Topological_Sort () 
        {
            _graph.Generate(_moduleConfigurations);
            _graph.DFS();
            Assert.NotNull(_graph.TopologicalSort);
            Assert.IsNotEmpty(_graph.TopologicalSort);
            _graph.Vertices.ForEach(v => {
                Assert.IsTrue(v.Color == Color.Black);
            });
        }
    }
}