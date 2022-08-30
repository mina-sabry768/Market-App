using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuperStore.Models
{
    public class Item
    {
        public int Id { get; set; }
        
        [Display(Name = "Item Name")]
        public string ItemTitle { get; set; }
       
        [Display(Name = "Item Description")]
        [AllowHtml]
        public string ItemContent { get; set; }
    
        [Display(Name = "Item Image")]
        public string ItemImage { get; set; }
      
        [Display(Name = "Item Price")]
        public int Price { get; set; }
        [Display(Name = "Category Type")]
        public int CategoryId { get; set; }

        public string UserID { get; set; }
        public virtual Category Category { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}