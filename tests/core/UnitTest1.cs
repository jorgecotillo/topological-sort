using NUnit.Framework;
using VDC.Core;

namespace Tests
{
    public class Tests
    {
        private DirectedGraph _graph;

        [SetUp]
        public void Setup()
        {
            _graph = new DirectedGraph();
        }

        [Test]
        public void Test1()
        {
            var txt = System.IO.File.ReadAllText("C:\\source_code\\azure-virtual-datacenter\\topological-sort\\tests\\samples\\moduleConfigurations.json");
            _graph.Generate(txt);
            Assert.Pass();
        }
    }
}