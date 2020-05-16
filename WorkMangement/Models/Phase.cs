using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public bool IsFinish { get; set; }
        #nullable enable
        // Thuộc tính không mapping với table trong database
        [NotMapped]
        public string? DisplayName { get; set; }
    }
}
