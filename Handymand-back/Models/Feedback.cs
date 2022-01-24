using Handymand.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Handymand.Models
{
    public class Feedback:BaseEntity
    {
        public int IdCreationUser { get; set; }
        //public User CreationUser { get; set; }

        public int? IdRefferedUser { get; set; }

        public string Text { get; set; }
        
        public double Grade { get; set; }

        public bool AsClient { get; set; }

    }
}
