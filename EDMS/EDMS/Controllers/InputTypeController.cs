using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EDMSBLL;
using EDMSDAL;
using EDMSModel;
using EDMSBLL.Services;
using EDMSBLL.Interface;

namespace EDMS.Controllers
{
   
    public class InputTypeController : Controller
    {
        IInputTypeServices _iinputtypeservices;
        ReturnMessageModel rModel;
      
        public InputTypeController()
        {
            _iinputtypeservices = new InputTypeServices();
            rModel = new ReturnMessageModel();
        }
        public ActionResult InputTypeList()
        {
            InputTypeModel cccModel = new InputTypeModel();
            cccModel.InputTypeList = _iinputtypeservices.GetInputTypeList();
            return View(cccModel);
        }

        public ActionResult CreateEditInputTypeCategory(int? InputTypeId)
        {
            InputTypeModel cccModel = new InputTypeModel();
            if (InputTypeId != null)
            {
                cccModel = _iinputtypeservices.GetInputTypeList().Where(x => x.InputTypeId == InputTypeId).FirstOrDefault();
            }
            return PartialView("_CreateEditInputTypeCategory", cccModel);
        }

        [HttpPost]
        public ActionResult CreateEditInputTypeCategory(InputTypeModel cccModel)
        {
            if (ModelState.IsValid)
            {
                var result = _iinputtypeservices.CreateEditInputTypeCategory(cccModel);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(rModel, JsonRequestBehavior.AllowGet);
            }
        }

    }
}