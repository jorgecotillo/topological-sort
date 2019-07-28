using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using VDC.Core.Models;

namespace VDC.Core
{
    public class DirectedGraph
    {
        public List<Vertex> Vertices { get; } = new List<Vertex>();
        public List<Vertex> TopologicalSort { get; private set; } = new List<Vertex>();
        private LinkedList<Vertex> _TopologicalSort { get; } = new LinkedList<Vertex>();
        int Time { get; set; }
        public int GraphSize 
        {
            get 
            {
                return Vertices.Count;
            }
        }

        public void Generate(string jsonGraph) 
        {
            try
            {
                var moduleConfigurations = 
                    JsonConvert.DeserializeObject<List<GraphModel>>(
                        jsonGraph);

                foreach (var moduleConfiguration in moduleConfigurations)
                {
                    // Add to vertices list
                    var vertex = new Vertex(moduleConfiguration.Name)
                    {
                        Color = Color.White
                    };
                    
                    AddVertex(vertex);
                }

                foreach (var moduleConfiguration in moduleConfigurations)
                {
                    // Let's analyze the dependencies and add them 
                    // into the Edges list of a vertex
                    if(moduleConfiguration.DependsOn != null && 
                       moduleConfiguration.DependsOn.Count > 0)
                    {
                        var childVertex = 
                                Vertices
                                    .Where(v => v.Name.Equals(
                                        moduleConfiguration.Name, 
                                        StringComparison.InvariantCultureIgnoreCase))
                                    .FirstOrDefault();
                        foreach (var dependency in moduleConfiguration.DependsOn)
                        {
                            // Find the module configuration in the list 
                            // of vertices
                            var parentVertex = 
                                Vertices
                                    .Where(v => v.Name.Equals(
                                        dependency, 
                                        StringComparison.InvariantCultureIgnoreCase))
                                    .FirstOrDefault();
                            
                            // Let's add edge like follows:
                            // parentVertex -> to -> childVertex
                            parentVertex.AddEdge(childVertex);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        private void AddVertex(Vertex vertex)
        {
            try
            {
                if(!Vertices.Any(v => v.Name.Equals(vertex.Name)))
                {
                    Vertices.Add(vertex);
                }
            }
            catch
            {
                throw;
            }
        }

        public void DFS()
        {
            foreach (var vertex in Vertices)
            {
                if(vertex.Color == Color.White)
                {
                    DFS_Visit(vertex);
                }
            }

            TopologicalSort = 
                _TopologicalSort.OrderByDescending(i => i.EndTime).ToList();
        }

        private void DFS_Visit(Vertex vertex)
        {
            Time++;
            vertex.StartTime = Time;
            vertex.Color = Color.Gray;
            foreach (var edge in vertex.Edges)
            {
                if (edge.Color == Color.White)
                {
                    DFS_Visit(edge);
                }
                else if(edge.Color == Color.Gray)
                {
                    throw new Exception($"Cyclic dependency found. Vertex: {vertex.Name} and Edge: {edge.Name}");
                }
                else
                {
                    continue;
                }
            }
            vertex.Color = Color.Black;
            Time++;
            vertex.EndTime = Time;

            _TopologicalSort.AddFirst(vertex);
        }
    }
}
