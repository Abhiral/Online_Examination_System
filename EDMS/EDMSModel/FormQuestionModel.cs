using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDMSModel
{
    public class FormQuestionModel
    {
        public FormQuestionModel()
        {
            FormQuestionList = new List<FormQuestionModel>();
        }
        public int FormQuestionId { get; set; }
        public int FormCategoryId { get; set; }
        public int InputTypeId { get; set; }
        public string Question { get; set; }
        public bool IsNull { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }

        public string FormName { get; set; }
        public string InputName { get; set; }
        public List<FormQuestionModel> FormQuestionList { get; set; }
    }


    public class QuestionDetailModel
    {
        public int FormQuestionId { get; set; }
        public int FormCategoryId { get; set; }
        public int InputTypeId { get; set; }
        public string Question { get; set; }
        public bool IsNull { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public bool IsActive { get; set; }

        public string FormName { get; set; }
        public string InputName { get; set; }
    }
}
