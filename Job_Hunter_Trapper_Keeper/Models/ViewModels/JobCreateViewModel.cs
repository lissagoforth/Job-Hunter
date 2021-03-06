﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Hunter_Trapper_Keeper.Models.ViewModels
{
    public class JobCreateViewModel
    {
        public int Id { get; set; }

        public string Company { get; set; }

        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }

        [Display(Name = "Job Notes")]
        public string Note { get; set; }
    }
}

