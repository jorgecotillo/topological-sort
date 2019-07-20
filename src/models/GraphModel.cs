using System;
using System.Collections.Generic;

namespace VDC.Core.Models
{
    public class GraphModel 
    {
        public string Name { get; set; }
        public List<string> DependsOn { get; set; }
    }
}