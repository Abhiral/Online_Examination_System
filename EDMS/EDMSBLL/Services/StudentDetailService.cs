using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDMSModel;
using EDMSBLL.Interface;
using EDMSDAL;

namespace EDMSBLL.Services
{
   public class StudentDetailService:IStudentDetailService
    {
        DynamicFormEntities _context;
        ReturnMessageModel rModel;
        public StudentDetailService()
        {
            rModel = new ReturnMessageModel();
            _context = new DynamicFormEntities();

        }
        public List<StudentDetailModel> GetStudentDetailList()
        {
            return _context.StudentDetails.Select(x => new StudentDetailModel
            {
                StudentId = x.StudentId,
                Name = x.Name,
                Address = x.Address,
                Contact = x.Contact,
                //RegNumber=x.RegNumber,
                Class = x.Class,
                QuestionSetNo = x.QuestionSetNo,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
                ModifiedBy = x.ModifiedBy,
                ModifiedDate = x.ModifiedDate

            }).ToList();
        }


        public ReturnMessageModel CreateEditStudentDetail(StudentDetailModel sModel)
        {
            try
            {
                
                var sdRow = _context.StudentDetails.Where(x => x.StudentId == sModel.StudentId).FirstOrDefault();
                if (sdRow == null)
                {
                    sdRow = new StudentDetail();
                }

                //sdRow.StudentId = sModel.StudentId;
                sdRow.Name = sModel.Name;
                sdRow.Address = sModel.Address;
                sdRow.Contact = sModel.Contact;
                sdRow.RegNumber = sdRow.StudentId;
                sdRow.Class = sModel.Class;
                sdRow.QuestionSetNo = sModel.QuestionSetNo;

                if (sdRow.StudentId == 0)
                {
                    sdRow.CreatedDate = System.DateTime.Now;
                    sdRow.CreatedBy = sModel.CreatedBy;
                    _context.StudentDetails.Add(sdRow);
                    _context.SaveChanges();

                }
                else
                {
                    sdRow.ModifiedDate = System.DateTime.Now;
                    sdRow.ModifiedBy = sModel.ModifiedBy;
                    _context.StudentDetails.Attach(sdRow);
                    _context.Entry(sdRow).State = System.Data.Entity.EntityState.Modified;
                    _context.SaveChanges();
                }

                rModel.Msg = "Student Saved Successfully";
                rModel.Success = true;
                rModel.ReturnId = sdRow.StudentId;
                return rModel;
            }
            catch (Exception ex)
            {
                rModel.Msg = "Section Saved Failed";
                rModel.Success = false;
                return rModel;

            }

        }
    }
}
