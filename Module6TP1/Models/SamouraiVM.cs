using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Module6TP1.Models
{
    public class SamouraiVM
    {
        public Samourai Samourai { get; set; }
        public List<SelectListItem> allArme { get; set; }
        public int IdArme { get; set; }
    }
}