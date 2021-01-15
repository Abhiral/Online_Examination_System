using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EDMSBLL.Interface;
using EDMSBLL.Services;
using EDMSModel;
using EDMSDAL;

namespace EDMS.Controllers
{
    public class StudentLoginController : Controller
    {
        IFormCategoryService _iFormCategoryService;
        IStudentDetailService _istudentdetailservice;
        ReturnMessageModel rModel;

        public StudentLoginController()
        {
            _istudentdetailservice = new StudentDetailService();
            _iFormCategoryService = new FormCategoryService();
            rModel = new ReturnMessageModel();
        }
        [AllowAnonymous]
        public ActionResult StudentLogin(string returnUrl,int? StudentId)
        {
            var sdModel = _istudentdetailservice.GetStudentDetailList().ToList();

            if (StudentId != null)
            {
                ViewBag.Thankyou = "ThankYou! Your answers are submitted";
                return View();
            }
            else
            {
                return View();
            }     
        }


        [HttpPost]
        public ActionResult StudentLogin(StudentDetailModel uModel)
        {
            using (DynamicFormEntities context = new DynamicFormEntities())
            {
                var sdModel = _istudentdetailservice.GetStudentDetailList().ToList();


                var users = sdModel.Where(x => x.Name == uModel.Name && x.StudentId == uModel.StudentId).FirstOrDefault();
                
                if (users != null)
                {
                    if (users.QuestionSetNo == null)
                    {
                        FormCategoryModel fcModel = new FormCategoryModel();
                        var formList = _iFormCategoryService.GetFormCategoryList(fcModel);
                        Random ran = new Random();
                        int index = ran.Next(formList.Count);
                        var formId = formList[index].FormId;
                        var StudentName = uModel.Name;
                        var StudentId = uModel.StudentId;
                        return RedirectToAction("QuestionDetail", "FormAnswer", new { FormId = formId, Name = StudentName, StudentId = StudentId });
                    }
                    else
                    {
                        ViewBag.QuestionSetMessage = "Already Attend Question";
                        return View(uModel);
                    }
                }
                else
                {
                    ViewBag.LoginMessage = "Invalid User Name or Password !!";
                    return View(uModel);
                }

            }

        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("StudentLogin");
        }

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public ActionResult StudentLogin(StudentDetailModel model, string returnUrl)
        //{
        //    if (ModelState.IsValid && WebSecurity.Login(model.Name, model.StudentId))
        //    {

        //        string username = model.Name;

        //        int StudentId = WebSecurity.GetUserId(username);
        //        StudentDetailModel scdModel = new StudentDetailModel();
        //        UserProfile up = scdModel.Database.SqlQuery<UserProfile>("Select *,0 as 'RoleId','' as'RoleName',''as 'Password',''as'ConfirmPassword' from UserProfile where UserId=" + userId + " and CanLogin=1").FirstOrDefault();
        //        if (up != null)
        //        {
        //            StudentDetailModel userSession = new StudentDetailModel() { UserId = StudentId, UserName = model.Name };
        //            Session["UserSession"] = userSession;
        //            Session.Timeout = 10;

        //            if (returnUrl != null)
        //            {
        //                return RedirectToLocal(returnUrl);

        //            }
        //            else
        //            {
        //                return RedirectToAction("Index", "FormCategory");
        //            }
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Sorry !! Your Are Not Registered");
        //            return View(model);
        //        }

        //    }

        //    ModelState.AddModelError("", "The user name or password provided is incorrect.");
        //    return View(model);
        //}
        //[HttpPost]
        //public ActionResult StudentLogin(int? StudentId,string Name)
        //{
        //    StudentDetailModel scdModel = new StudentDetailModel();
        //    var sccModel = _istudentdetailservice.GetStudentDetailList().ToList();

        //    if (StudentId != null)
        //    {
        //        scdModel = _istudentdetailservice.GetStudentDetailList().Where(x => x.StudentId == StudentId).FirstOrDefault();
        //        FormCategoryModel fcModel = new FormCategoryModel();
        //        fcModel.FormCategoryModelList = _iFormCategoryService.GetFormCategoryList(fcModel);
        //        return View(fcModel);
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}

    }
}