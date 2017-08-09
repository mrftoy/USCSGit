using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace USCS.ViewModels
{
    public class CategoryData
    {
        public int CategoryID { get; set; }

        public string Domain { get; set; }

        public string PrimaryCategory { get; set; }

        public string SecondaryCategory { get; set; }

        public string TertiaryCategory { get; set; }
    }
}