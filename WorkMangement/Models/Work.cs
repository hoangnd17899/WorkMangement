using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMangement
{
    public class Work
    {
        [Key]
        public Guid WorkId { get; set; }
        public string WorkName { get; set; }
        public string WorkDescription { get; set; }
        public List<Phase> WorkPhases { get; set; } = new List<Phase>();
    }
}
