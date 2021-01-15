using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDMSModel
{
    public class FormAnswerDetailModel
    {

        public int AnswerDetailID { get; set; }
        public int AnswerId { get; set; }
        public int FormQuestionId { get; set; }
        public string Answer { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> Marks { get; set; }



    }
}


