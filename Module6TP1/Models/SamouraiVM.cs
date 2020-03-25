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
        public IEnumerable<SelectListItem> allArme { get; set; }
        public int? IdArme { get; set; }
        public List<ArtMartial> ArtMartials { get; set; } = new List<ArtMartial>();
        public List<int> IdsArtMartiaux { get; set; } = new List<int>();
    }
}