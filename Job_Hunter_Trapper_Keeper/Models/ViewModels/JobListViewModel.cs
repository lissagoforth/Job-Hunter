using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Hunter_Trapper_Keeper.Models.ViewModels
{
    public class JobListViewModel
    {
        public IEnumerable<JobViewModel> Jobs { get; set; }
    }
}
