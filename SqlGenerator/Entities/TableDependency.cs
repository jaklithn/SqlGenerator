using System.Collections.Generic;

namespace SqlGenerator.Entities
{
    public class TableDependency
    {
        public string Foreign { get; set; }
        public List<string> Related { get; set; }

        public TableDependency()
        {
            Related = new List<string>();
        }
    }
}