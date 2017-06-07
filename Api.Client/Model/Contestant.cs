using System;
using System.Collections.Generic;

namespace Nexosis.Api.Client.Model
{
    public class Contestant 
    {
        public Algorithm Algorithm { get; set; }
        public List<JobMetric> Metrics { get; set; }

        public List<Uri> Links { get; set; }
    }

    public class JobMetric
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public double Value { get; set; }
    }

    public class Algorithm
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
