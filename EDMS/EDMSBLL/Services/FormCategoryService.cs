using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDMSBLL.Interface;
using EDMSModel;
using EDMSDAL;
using System.Data;


namespace EDMSBLL.Services
{
    public class FormCategoryService : IFormCategoryService
    {
        DynamicFormEntities _context;
        ReturnMessageModel rModel;

        public FormCategoryService()
        {
            _context = new DynamicFormEntities();
            rModel = new ReturnMessageModel();
        }
        public FormCategoryModel GetFormNameById(int formId)
        {
            FormCategoryModel fcModel = new FormCategoryModel();
            fcModel = (from g in _context.FormCategories
                              where g.FormId == formId
                              select new FormCategoryModel()
                              {
                                  FormId = g.FormId,
                                  FormName = g.FormName
                              }).FirstOrDefault();
            return fcModel;
        }

        public List<FormCategoryModel> GetFormCategoryList(FormCategoryModel fcModel)
        {
            var FormCategoryList = _context.FormCategories.Select(x => new FormCategoryModel
            {
                FormId = x.FormId,
                FormName = x.FormName,
                SubjectId = x.SubjectId,
                CreatedDate = x.CreatedDate,
                CreatedBy = x.CreatedBy,
                ModifiedDate = x.ModifiedDate,
                ModifiedBy = x.ModifiedBy
            }).ToList();
            return FormCategoryList;
        }

        public ReturnMessageModel SaveFormCategory(FormCategoryModel formCategoryModel)
        {
            var row = _context.FormCategories.Where(x => x.FormId == formCategoryModel.FormId).FirstOrDefault();
            if (row == null)
            {
                row = new FormCategory();
            }

            row.FormName = formCategoryModel.FormName;
            row.SubjectId = formCategoryModel.SubjectId;

            if (formCategoryModel.FormId == 0)
            {

                row.CreatedDate = System.DateTime.Now;
                row.CreatedBy = formCategoryModel.CreatedBy;
                _context.FormCategories.Add(row);
            }

            else
            {

                row.ModifiedDate = System.DateTime.Now;
                row.ModifiedBy = formCategoryModel.ModifiedBy;
                _context.FormCategories.Attach(row);
                _context.Entry(row).State = System.Data.Entity.EntityState.Modified;
            }
            _context.SaveChanges();
            rModel.Msg = "Saved Successfully";
            rModel.Success = true;
            return rModel;
        }
    }
}
