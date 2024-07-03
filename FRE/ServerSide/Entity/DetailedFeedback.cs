using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Entity
{
    public class DetailedFeedback
    {
        public int Id { get; set; }
        public int MenuItemId { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
        public int QuestionTypeId { get; set; }
        public DateTime CreatedDate { get; set; }
        public MenuItem MenuItem { get; set; }
        public User User { get; set; }
        public QuestionType QuestionType { get; set; }
    }
}
