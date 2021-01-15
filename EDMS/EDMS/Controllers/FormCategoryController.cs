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
    public class FormCategoryController : Controller
    {
        IFormCategoryService _iFormCategoryService;
        ReturnMessageModel rModel;

        public FormCategoryController()
        {
            _iFormCategoryService = new FormCategoryService();
            rModel = new ReturnMessageModel();
        }

        public ActionResult Index()
        {
            FormCategoryModel fcModel = new FormCategoryModel();
            fcModel.FormCategoryModelList = _iFormCategoryService.GetFormCategoryList(fcModel);
            return View(fcModel);
        }

        public ActionResult CreateEditFormCategory(int? FormId)
        {
            FormCategoryModel fcModel = new FormCategoryModel();
            if(FormId != null)
            {
                fcModel = _iFormCategoryService.GetFormCategoryList(fcModel).Where(x=>x.FormId==FormId).FirstOrDefault();
            }
            return PartialView("_CreateEditFormCategory",fcModel);
        }

        [HttpPost]
        public ActionResult CreateEditFormCategory(FormCategoryModel fcModel)
        {
            if (ModelState.IsValid)
            {
                var result = _iFormCategoryService.SaveFormCategory(fcModel);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(rModel, JsonRequestBehavior.AllowGet);
            }
        }
    }
}