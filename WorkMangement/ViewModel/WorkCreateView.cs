using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMangement
{
    public class WorkCreateView
    {
        [Required(ErrorMessage ="Vui lòng nhập tên dự án")]
        public string WorkName { get; set; }
        public string WorkDescription { get; set; }
    }
}
