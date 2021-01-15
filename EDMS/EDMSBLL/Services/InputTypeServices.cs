using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDMSModel;
using EDMSDAL;
using EDMSBLL.Interface;

using System.Data.Entity;
namespace EDMSBLL.Services
{
   public class InputTypeServices: IInputTypeServices
       
    {
        DynamicFormEntities _context;
        ReturnMessageModel rModel;
      public InputTypeServices()
        {
            rModel = new ReturnMessageModel();
            _context = new DynamicFormEntities();
            
        }
        public List<InputTypeModel> GetInputTypeList()
        {
            return _context.InputTypes.Select(x => new InputTypeModel
            {
                InputTypeId = x.InputTypeId,
                InputName = x.InputName,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
                ModifiedBy = x.ModifiedBy,
                ModifiedDate = x.ModifiedDate

            }).ToList();
        }


        public ReturnMessageModel CreateEditInputTypeCategory(InputTypeModel sModel)
        {
            try
            {
                //using (DynamicFormEntities _context = new DynamicFormEntities())
                //{

                //}
                    var ccRow = _context.InputTypes.Where(x => x.InputTypeId == sModel.InputTypeId).FirstOrDefault();
                if (ccRow == null)
                {
                    ccRow = new InputType();
                }

                ccRow.InputTypeId = sModel.InputTypeId;
                ccRow.InputName = sModel.InputName;

                if (ccRow.InputTypeId == 0)
                {
                    ccRow.CreatedDate = System.DateTime.Now;
                    ccRow.CreatedBy = sModel.CreatedBy;
                    _context.InputTypes.Add(ccRow);
                    _context.SaveChanges();

                }
                else
                {
                    ccRow.ModifiedDate = System.DateTime.Now;
                    ccRow.ModifiedBy = sModel.ModifiedBy;
                    _context.InputTypes.Attach(ccRow);
                    _context.Entry(ccRow).State =  System.Data.Entity.EntityState.Modified;
                    _context.SaveChanges();
                }

                rModel.Msg = "Section Saved Successfully";
                rModel.Success = true;
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
