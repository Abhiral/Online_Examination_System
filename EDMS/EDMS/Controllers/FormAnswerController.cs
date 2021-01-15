using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EDMSModel;
using EDMSBLL.Interface;
using EDMSBLL.Services;
using System.Data;
using System.Text.RegularExpressions;
using System.IO;

namespace EDMS.Controllers
{
    public class FormAnswerController : Controller
    {
        IFormAnswerServices _iformanswerservice;
        ReturnMessageModel rModel;
        IFormCategoryService _iformcategoryservices;
        IFormQuestionService _iformquestion;

        public FormAnswerController()
        {
            _iformanswerservice = new FormAnswerServices();
            rModel = new ReturnMessageModel();
            _iformcategoryservices = new FormCategoryService();
            _iformquestion = new FormQuestionService();
        }
        public ActionResult FormAnswerIndex(int FormId)
        {
            FormAnswerModel faModel = new FormAnswerModel();
            var formName = _iformcategoryservices.GetFormNameById(FormId);
            faModel.FormAnswerList = _iformanswerservice.GetFormAnswer(FormId);
            
            faModel.FormId = FormId;
            faModel.FormName = formName.FormName;
            
            return View(faModel);
        }

        public ActionResult QuestionDetail(int FormId, int? AnswerId,string Name,int? StudentId)
        {
            QuestionAnswerModel faModel = new QuestionAnswerModel();
            var formName = _iformcategoryservices.GetFormNameById(FormId);

            faModel.FormCategoryId = FormId;
            faModel.FormName = formName.FormName;
            if (StudentId != 0)
            {
                faModel.StudentId = StudentId;
            }

            if (AnswerId != null)
            {
                faModel.QuestionAnswerList = _iformquestion.GetQuestionAnswers(FormId, AnswerId);
                faModel.SubmittedBy = faModel.QuestionAnswerList[0].SubmittedBy;
                int? totalMarks = 0;
                foreach (var item in faModel.QuestionAnswerList)
                {
                    totalMarks += item.Marks;
                }
                faModel.grandTotal = Convert.ToDecimal(totalMarks);
            }
            else
            {
                faModel.QuestionAnswerList = _iformanswerservice.GetQuestionDetails(FormId);
                faModel.SubmittedBy = Name;

            }
            // faModel.SubmittedBy = faModel.QuestionAnswerList[0].SubmittedBy;


            return View(faModel);
        }

        [HttpPost]
        public ActionResult SaveQuestionAnswer(QuestionAnswerModel faModel)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in faModel.QuestionAnswerList)
                {
                    if (item.InputName == "Files")
                    {
                        string formName = faModel.FormName;
                        formName = formName.Replace(' ', '_');
                        string question = item.Question;
                        question = question.Replace(' ', '_');

                        IEnumerable<HttpPostedFileBase> files = Request.Files.GetMultiple(question);
                        foreach (HttpPostedFileBase postedFile in files)
                        {
                            string fileNameCV = "";
                            int permittedSizeInBytes = 5 * 1024 * 1024;
                            if (faModel.FileChange && Request.Files[question] != null)
                            {
                                if (postedFile.ContentLength > 0)
                                {
                                    if (postedFile.ContentLength > permittedSizeInBytes)
                                    {
                                        rModel.Msg = "File size is not Valid!!";
                                        rModel.Success = false;
                                        return Json(rModel, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        string nonReplaceStringFile = Guid.NewGuid().ToString() + "_" + Path.GetFileName(postedFile.FileName);
                                        fileNameCV = Regex.Replace(nonReplaceStringFile, @"-", "");

                                        /* Create Folder */
                                        string createFol = Server.MapPath(string.Format("~/Upload/{0}/{1}/", formName, question));
                                        if (!Directory.Exists(createFol))
                                        {
                                            Directory.CreateDirectory(createFol);
                                        }

                                        string FilePath = string.Format("~/Upload/{0}/{1}/", formName, question) + fileNameCV;
                                        try
                                        {
                                            System.IO.File.Delete(Server.MapPath("~" + item.Answer));
                                        }
                                        catch (Exception)
                                        {
                                            //throw;
                                        }
                                        postedFile.SaveAs(Server.MapPath(FilePath));
                                        if (item.Answer == null)
                                        {
                                            item.Answer = string.Format("~/Upload/{0}/{1}/", formName, question) + fileNameCV;
                                        }
                                        else
                                        {
                                            item.Answer += "," + string.Format("~/Upload/{0}/{1}/", formName, question) + fileNameCV;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                var result = _iformanswerservice.SaveQuestionAnswer(faModel);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(rModel, JsonRequestBehavior.AllowGet);
            }
        }

        public FileResult Download(string fileLocation)
        {
            string[] extension = fileLocation.Split('/');
            string filename = extension[4].ToString();
            return new FilePathResult(fileLocation, filename);
        }

    }
}