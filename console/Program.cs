using System;
using VDC.Core;
using Newtonsoft.Json;

namespace console
{
    class Program
    {
        static void Main(string[] args)
        {
            var txt = System.IO.File.ReadAllText("C:\\source_code\\azure-virtual-datacenter\\topological-sort\\tests\\samples\\moduleConfigurations.json");
            var graph = new DirectedGraph();
            graph.Generate(txt);
            
            Console.WriteLine(JsonConvert.SerializeObject(graph.Vertices));
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine(Environment.NewLine);
            
            graph.DFS();

            Console.WriteLine(JsonConvert.SerializeObject(graph.Vertices));

            Console.WriteLine(JsonConvert.SerializeObject(graph.Vertices));
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine(Environment.NewLine);

            Console.WriteLine(JsonConvert.SerializeObject(graph.TopologicalSort));
        }
    }
}
