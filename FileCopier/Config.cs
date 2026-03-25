using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCopier
{
    internal class Config
    {
        public string SourceFolder { get; set; }
        public string TargetFolder { get; set; }
        public string SourceFileName { get; set; }
        public string Template { get; set; }
        public string Prefix { get; set; }
        public string Id { get; set; }
    }
}
