using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperStore.Models
{
    public class BuyItem
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public DateTime BuyDate { get; set; }
        public int ItemId { get; set; }
        public string UserId { get; set; }

        public virtual Item item { get; set; }
        public virtual ApplicationUser user { get; set; }
    }
}   