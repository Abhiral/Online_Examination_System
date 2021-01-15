using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EDMSModel
{
   public class StudentDetailModel
    {
        public StudentDetailModel()
        {
            StudentDetailList = new List<StudentDetailModel>();
        }
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public long Contact { get; set; }
        public int RegNumber { get; set; }
        public int Class { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public List<StudentDetailModel> StudentDetailList { get; set; }
        public Nullable<int> QuestionSetNo { get; set; }


        public class LoginModel
        {

         
            public string Name { get; set; }
            public string StudentId { get; set; }

        }

    }
}
