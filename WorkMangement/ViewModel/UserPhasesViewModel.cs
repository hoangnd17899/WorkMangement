using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMangement
{
    public class UserPhasesViewModel
    {
        public Guid PhaseId { get; set; }
        public Guid WorkId { get; set; }
        public string PhaseName { get; set; }
        public string WorkName { get; set; }
        public bool IsFinish { get; set; }
        public bool CanChange { get; set; }
    }
}
