using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDMSModel
{
    public class FormCategoryModel
    {
        public FormCategoryModel()
        {
            FormCategoryModelList = new List<FormCategoryModel>();
        }
        public int FormId { get; set; }
        public string FormName { get; set; }
        public Nullable<int> SubjectId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }

        public List<FormCategoryModel> FormCategoryModelList { get; set; }
    }
}
