using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperStore.Models
{
    public class ItemsViewModel
    {
        public string Itemtitle { get; set; }

        public IEnumerable<BuyItem> Listes { get; set; }
    }
}