using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class DiscardItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public double AverageRating { get; set; }
        public string Sentiments { get; set; }
    }
}
