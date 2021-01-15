using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EDMSDAL;
using EDMSBLL.Interface;
using EDMSBLL.Services;
using EDMSModel;

namespace EDMS.Controllers
{
    public class StudentDetailController : Controller
    {
        IStudentDetailService _istudentdetailservice;
        ReturnMessageModel rModel;

        public StudentDetailController()
        {
            _istudentdetailservice = new StudentDetailService();
            rModel = new ReturnMessageModel();
        }
        public ActionResult StudentDetailList()
        {
            StudentDetailModel sdmModel = new StudentDetailModel();
            sdmModel.StudentDetailList = _istudentdetailservice.GetStudentDetailList();

            return View(sdmModel);
        }

        public ActionResult CreateEditStudentDetail(int? StudentId)
        {
            StudentDetailModel scdModel = new StudentDetailModel();
            if (StudentId != null)
            {
                scdModel = _istudentdetailservice.GetStudentDetailList().Where(x => x.StudentId == StudentId).FirstOrDefault();
            }
            return PartialView("_CreateEditStudentDetail", scdModel);
        }

        [HttpPost]

        public ActionResult CreateEditStudentDetail(StudentDetailModel scdModel)
        {
            if (ModelState.IsValid)
            {
                var result = _istudentdetailservice.CreateEditStudentDetail(scdModel);
                StudentDetailModel sdModel = new StudentDetailModel();
                sdModel = _istudentdetailservice.GetStudentDetailList().Where(x => x.StudentId == result.ReturnId).FirstOrDefault();
                return PartialView("_PrintAdmitCard", sdModel);
            }
            else
            {
                return Json(rModel, JsonRequestBehavior.AllowGet);
            }
        }
      

    }
}