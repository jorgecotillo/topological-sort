using System;
using System.Collections.Generic;

namespace VDC.Core.Models
{
    public class VertexModel 
    {
        public string Name { get; set; }
        public double Weight { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public string Predecessor { get; set; }
        public LinkedList<string> Adj { get; set; }
        public Color Color { get; set; }
    }

    public enum Color
    {
        NONE = 0,
        WHITE = 1,
        GRAY = 2,
        BLACK = 3
    } 

}