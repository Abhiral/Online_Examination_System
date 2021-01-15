using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDMSModel
{
    public class FormAnswerModel
    {

        public FormAnswerModel()
        {
            FormAnswerList = new List<FormAnswerModel>();

        }
        public int AnswerId { get; set; }
        public int FormCategoryId { get; set; }
        public string SubmittedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }

        public List<FormAnswerModel> FormAnswerList { get; set; }

        public int? FormId { get; set; }
        [DisplayName("FormName")]
        public string FormName { get; set; }
        public List<FormCategoryModel> FormCatogoryList { get; set; }

        public int? FormQuestionId { get; set; }
        [DisplayName("FormQuestionName")]
        public string FormQuestionName { get; set; }
        public List<QuestionAnswerModel> QuestionAnswerList { get; set; }
        public string InputType { get; set; }
        public int InputTypeId { get; set; }
        public decimal grandTotal { get; set;}

    }
}
