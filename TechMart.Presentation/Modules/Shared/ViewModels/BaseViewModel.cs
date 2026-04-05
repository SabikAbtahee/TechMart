using System;
using System.Collections.Generic;
using System.Text;

namespace TechMart.Presentation.Modules.Shared.ViewModels
{
    public class BaseViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
