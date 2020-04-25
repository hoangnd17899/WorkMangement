using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMangement
{
    public class PhaseEditView
    {
        public Guid PhaseId { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên công việc")]
        public string PhaseName { get; set; }
        public Guid WorkId { get; set; }
        public Guid? EmployeeId { get; set; }
    }
}
