using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMangement
{
    public class Phase
    {
        [Key]
        public Guid PhaseId { get; set; }
        public string PhaseName { get; set; }
        public Guid WorkId { get; set; }
        public Guid? EmployeeId { get; set; }
    }
}
