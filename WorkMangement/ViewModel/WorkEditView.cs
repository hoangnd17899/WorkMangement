﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMangement
{
    public class WorkEditView
    {
        public Guid WorkId { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên dự án")]
        [DisplayName("Tên công việc")]
        public string WorkName { get; set; }
        [DisplayName("Mô tả")]
        public string WorkDescription { get; set; }
        public List<Phase> WorkPhases { get; set; }
    }
}
