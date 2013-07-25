using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using rovnicaMVC.Domain;

namespace rovnicaMVC.Models
{
    public class categoryInfo
    {
        public categoryInfo()
        {
            pager = new pagerModel();
            items = new List<cItem>();

        }

        public int categoryId { get; set; }
        public pagerModel pager { get; set; }
        public List<cItem> items { get; set; }
    }
}
