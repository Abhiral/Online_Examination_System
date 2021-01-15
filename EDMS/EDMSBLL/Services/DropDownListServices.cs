using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDMSBLL.Interface;
using EDMSModel;
using EDMSDAL;
using System.Data.Entity;

namespace EDMSBLL.Services
{
   public class DropDownListServices : IDropDownListServices
    {

        DynamicFormEntities _context;
        ReturnMessageModel rModel;

        public DropDownListServices()
        {
            _context = new DynamicFormEntities();
            rModel = new ReturnMessageModel();
        }

        //public InputTypeModel GetInputTypeById(int p)
        //{
        //    InputTypeModel inputTypeModel = new InputTypeModel();
        //    inputTypeModel = (from g in _context.InputTypes
        //                      where g.InputTypeId == p
        //                      select new InputTypeModel()
        //                      {
        //                          InputTypeId = g.InputTypeId,
        //                          InputName = g.InputName
        //                      }).FirstOrDefault();
        //    return inputTypeModel;

        //}
        public FormQuestionModel GetFormQuestionIdByDropDownId(int dropDownId)
        {
            FormQuestionModel formQuestionModel = new FormQuestionModel();
            formQuestionModel = (from dd in _context.DropDownLists
                                 join fq in _context.FormQuestions on dd.FormQuestionId equals fq.FormQuestionId
                                 
                                 where dd.DropDownId == dropDownId
                                 select new FormQuestionModel()
                                 {
                                  FormQuestionId = fq.FormQuestionId   
                                 }).FirstOrDefault();
            return formQuestionModel;
        }




        public List<DropDownListModel> GetDropDownList(DropDownListModel fdModel)
        {
            var DropDownList = _context.DropDownLists.Select(x => new DropDownListModel
            {
                DropDownId = x.DropDownId,
                FormQuestionId = x.FormQuestionId,
               
                DropDownName = x.DropDownName,
                IsActive = x.IsActive,

                CreatedDate = x.CreatedDate,
                CreatedBy = x.CreatedBy,
                ModifiedDate = x.ModifiedDate,
                ModifiedBy = x.ModifiedBy
            }).ToList();
            return DropDownList;
        }

        public ReturnMessageModel SaveDropDown(DropDownListModel fdModel)
        {
           
                foreach (var dropdownItem in fdModel.DropDownList)
                {
                    var row = _context.DropDownLists.Where(x => x.DropDownId == dropdownItem.DropDownId).FirstOrDefault();
                    if (row == null)
                    {
                        row = new DropDownList();
                    }
                row.FormQuestionId = fdModel.FormQuestionId;
                row.DropDownName = dropdownItem.DropDownName;
                row.IsActive = dropdownItem.IsActive;
                row.DropDownId = dropdownItem.DropDownId;

                if (dropdownItem.DropDownId == 0)
                    {
                        row.CreatedDate = System.DateTime.Now;
                        row.CreatedBy = dropdownItem.CreatedBy;
                        _context.DropDownLists.Add(row);
                    }

                    else
                    {
                        row.ModifiedDate = System.DateTime.Now;
                        row.ModifiedBy = dropdownItem.ModifiedBy;
                        _context.DropDownLists.Attach(row);
                        _context.Entry(row).State = EntityState.Modified;
                    }

                }
            
            _context.SaveChanges();
            rModel.Msg = "Saved Successfully";
            rModel.Success = true;
            return rModel;
        }

        public ReturnMessageModel EditDropDownList(DropDownListModel feModel)
        {

            var row = _context.DropDownLists.Where(x => x.DropDownId == feModel.DropDownId).FirstOrDefault();
            row.DropDownName = feModel.DropDownName;
            row.FormQuestionId = feModel.FormQuestionId;
            row.IsActive = feModel.IsActive;
            row.ModifiedDate = System.DateTime.Now;
            row.ModifiedBy = feModel.ModifiedBy;
            _context.DropDownLists.Attach(row);
            _context.Entry(row).State = EntityState.Modified;
            _context.SaveChanges();
            rModel.Msg = "Saved Successfully";
            rModel.Success = true;
            return rModel;
        }

  
    }
}
