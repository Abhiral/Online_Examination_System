using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDMSModel
{
    public class InputTypeModel
    {
        public InputTypeModel()
        {
            InputTypeList = new List<InputTypeModel>();
        }
        public int InputTypeId { get; set; }
        public string InputName { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public List<InputTypeModel> InputTypeList { get; set; }
    }
}
