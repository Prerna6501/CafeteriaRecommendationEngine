using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Entity
{
    public class QuestionType
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public List<DetailedFeedback> DetailedFeedbacks { get; set; }
    }
}
