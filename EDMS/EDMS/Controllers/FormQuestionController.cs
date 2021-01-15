using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EDMSBLL.Interface;
using EDMSBLL.Services;
using EDMSModel;


namespace EDMS.Controllers
{
    public class FormQuestionController : Controller
    {
        // GET: FormQuestion
        IFormQuestionService _iFormQuestionService;
        ReturnMessageModel rModel;
        IInputTypeServices _iInputTypeService;
        IDropDownListServices _idropdownlistservices;
        public FormQuestionController()
        {
            _iFormQuestionService = new FormQuestionService();
            _iInputTypeService = new InputTypeServices();
            _idropdownlistservices = new DropDownListServices();

            rModel = new ReturnMessageModel();
        }
        #region FormQuestion
        public ActionResult CreateEditFormQuestion(int FormId)
        {
            FormQuestionModel fqModel = new FormQuestionModel();
            fqModel.FormQuestionList = _iFormQuestionService.GetFormQuestionList().Where(x => x.FormCategoryId == FormId).ToList();
            if (fqModel == null)
            {
                fqModel = new FormQuestionModel();
            }
            fqModel.FormCategoryId = FormId;
            ViewBag.InputTypeCollection = new SelectList(_iInputTypeService.GetInputTypeList(), "InputTypeId", "InputName");
            return View(fqModel);
        }

        [HttpPost]
        public ActionResult CreateEditFormQuestion(FormQuestionModel fqModel)
        {
            if (ModelState.IsValid)
            {
                var result = _iFormQuestionService.SaveFormQuestion(fqModel);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(rModel, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult EditFormQuestion(int FormQuestionId)
        {
            FormQuestionModel fqModel = new FormQuestionModel();

            fqModel = _iFormQuestionService.GetFormQuestionList().Where(x => x.FormQuestionId == FormQuestionId).FirstOrDefault();
            ViewBag.InputTypeCollection = new SelectList(_iInputTypeService.GetInputTypeList(), "InputTypeId", "InputName");
            return PartialView("_EditFormQuestion", fqModel);
        }

        [HttpPost]
        public ActionResult EditFormQuestion(FormQuestionModel fqModel)
        {
            if (ModelState.IsValid)
            {
                var result = _iFormQuestionService.EditFormQuestion(fqModel);
                return RedirectToAction("CreateEditFormQuestion", "FormQuestion", new { @FormId = fqModel.FormCategoryId });
            }
            else
            {
                return Json(rModel, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AddQuestionToListView(int displayOrder, string question, bool isNull, int inputTypeId, int counter, int formId)
        {
            FormQuestionModel qdModel = new FormQuestionModel();
            counter++;
            ViewBag.Counter = counter;
            var inputType = _iFormQuestionService.GetInputTypeById(inputTypeId);
            qdModel.DisplayOrder = displayOrder;
            qdModel.Question = question;
            qdModel.IsNull = isNull;
            qdModel.InputName = inputType.InputName;
            qdModel.InputTypeId = inputTypeId;
            qdModel.FormCategoryId = formId;
            return PartialView("_AddQuestionToListView", qdModel);
        }
        #endregion

        #region DropDownList
        public ActionResult AddDropDownOption(int FormQuestionId)
        {
            DropDownListModel ddModel = new DropDownListModel();
            var formId = _iFormQuestionService.GetFormIdByFormQuestionId(FormQuestionId).FormId;
            ddModel.DropDownList = _idropdownlistservices.GetDropDownList(ddModel).Where(x => x.FormQuestionId == FormQuestionId).ToList();
            ddModel.FormQuestionId = FormQuestionId;
            ddModel.FormId = formId;
            return View(ddModel);
        }

        [HttpPost]
        public ActionResult AddDropDownOption(DropDownListModel adModel)
        {
            if (ModelState.IsValid)
            {
                var result = _idropdownlistservices.SaveDropDown(adModel);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(rModel, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult AddDropDownToList(string DropDownName, int counter)
        {
            DropDownListModel qdModel = new DropDownListModel();
            counter++;
            ViewBag.Counter = counter;
            qdModel.DropDownName = DropDownName;
            return PartialView("_AddDropDownList", qdModel);
        }
        public ActionResult EditDropDownList(int DropDownId)
        {
            DropDownListModel feModel = new DropDownListModel();
            var formQuestionId = _idropdownlistservices.GetFormQuestionIdByDropDownId(DropDownId).FormQuestionId;
            feModel = _idropdownlistservices.GetDropDownList(feModel).Where(x => x.DropDownId == DropDownId).FirstOrDefault();
            feModel.FormQuestionId = formQuestionId;
            return PartialView("_EditDropDownList", feModel);
        }

        [HttpPost]
        public ActionResult EditDropDownList(DropDownListModel feModel)
        {
            if (ModelState.IsValid)
            {
                var result = _idropdownlistservices.EditDropDownList(feModel);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(rModel, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

    }
}