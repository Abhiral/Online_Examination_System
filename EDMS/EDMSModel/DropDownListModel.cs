using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDMSModel
{
   public class DropDownListModel
    {
        public DropDownListModel()
        {
            DropDownList = new List<DropDownListModel>();
        }
        public int DropDownId { get; set; }
        public int FormQuestionId { get; set; }
        public string DropDownName { get; set; }
        public bool IsActive { get; set; }
        public int FormId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public List<DropDownListModel> DropDownList { get; set; }
        
    }
}
