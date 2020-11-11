using System;
using System.Collections.Generic;

namespace Kanban.Models
{
    public partial class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Issue> Issues { get; set; } = new List<Issue>();
    }
}
