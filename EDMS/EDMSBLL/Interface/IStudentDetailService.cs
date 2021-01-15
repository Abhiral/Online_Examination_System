using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDMSModel;

namespace EDMSBLL.Interface
{
   public interface IStudentDetailService
    {
        //StudentDetailModel GetStudentDetailById(int StudentId);

        List<StudentDetailModel> GetStudentDetailList();
        ReturnMessageModel CreateEditStudentDetail(StudentDetailModel sModel);
    }
}
