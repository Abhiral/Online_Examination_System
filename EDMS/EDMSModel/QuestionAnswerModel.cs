using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDMSModel
{
    public class QuestionAnswerModel
    {

        public QuestionAnswerModel()
        {
            QuestionAnswerList = new List<QuestionAnswerModel>();
        }

        public int AnswerDetailID { get; set; }
        public Nullable<int> AnswerId { get; set; }
        public int FormQuestionId { get; set; }
        public string Answer { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public int FormCategoryId { get; set; }
        public string InputName { get; set; }
        public List<QuestionAnswerModel> QuestionAnswerList { get; set; }

        public string Question { get; set; }

        public bool IsMarried { get; set; }
        public bool IsActive { get; set; }

        public string SubmittedBy { get; set; }
        public int InputTypeId { get; set; }

        public string FormName { get; set; }

        public bool FileChange { get; set; }
        public Nullable<int> Marks { get; set; }
        public Nullable<int> StudentId { get;set; }
        public decimal grandTotal { get; set; }





    }
}
