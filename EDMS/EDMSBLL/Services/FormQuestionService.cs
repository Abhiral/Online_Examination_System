using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDMSModel;
using EDMSBLL.Interface;
using EDMSDAL;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace EDMSBLL.Services
{
    public class FormQuestionService : IFormQuestionService
    {
        DynamicFormEntities _context;
        ReturnMessageModel rModel;

        public FormQuestionService()
        {
            _context = new DynamicFormEntities();
            rModel = new ReturnMessageModel();
        }

        public InputTypeModel GetInputTypeById(int p)
        {
            InputTypeModel inputTypeModel = new InputTypeModel();
            inputTypeModel = (from g in _context.InputTypes
                              where g.InputTypeId == p
                              select new InputTypeModel()
                              {
                                  InputTypeId = g.InputTypeId,
                                  InputName = g.InputName
                              }).FirstOrDefault();
            return inputTypeModel;

        }

        public FormCategoryModel GetFormIdByFormQuestionId(int formQuestionId)
        {
            FormCategoryModel formCategoryModel = new FormCategoryModel();
            formCategoryModel = (from fq in _context.FormQuestions
                                 join fc in _context.FormCategories on fq.FormCategoryId equals fc.FormId
                                 where fq.FormQuestionId == formQuestionId
                                 select new FormCategoryModel
                                 {
                                     FormId = fc.FormId
                                 }).FirstOrDefault();
            return formCategoryModel;
        }

        public List<FormQuestionModel> GetFormQuestionList()
        {
            var FormQuestionList = _context.FormQuestions.Select(x => new FormQuestionModel
            {
                FormQuestionId = x.FormQuestionId,
                FormCategoryId = x.FormCategoryId,
                FormName = x.FormCategory.FormName,
                InputName = x.InputType.InputName,
                Question = x.Question,
                DisplayOrder = x.DisplayOrder,
                IsNull = x.IsNull,
                InputTypeId = x.InputTypeId,
                IsActive = x.IsActive,
                CreatedDate = x.CreatedDate,
                CreatedBy = x.CreatedBy,
                ModifiedDate = x.ModifiedDate,
                ModifiedBy = x.ModifiedBy
            }).OrderBy(x => x.DisplayOrder).ToList();
            return FormQuestionList;
        }

        public ReturnMessageModel SaveFormQuestion(FormQuestionModel fqModel)
        {
            foreach (var questionItem in fqModel.FormQuestionList)
            {
                var row = _context.FormQuestions.Where(x => x.FormQuestionId == questionItem.FormQuestionId).FirstOrDefault();
                if (row == null)
                {
                    row = new FormQuestion();
                }
                row.FormCategoryId = fqModel.FormCategoryId;
                row.InputTypeId = questionItem.InputTypeId;
                row.Question = questionItem.Question;
                row.IsNull = questionItem.IsNull;
                row.IsActive = questionItem.IsActive;
                row.DisplayOrder = questionItem.DisplayOrder;
                if (questionItem.FormQuestionId == 0)
                {
                    row.CreatedDate = System.DateTime.Now;
                    row.CreatedBy = questionItem.CreatedBy;
                    _context.FormQuestions.Add(row);
                }
                else
                {
                    row.ModifiedDate = System.DateTime.Now;
                    row.ModifiedBy = questionItem.ModifiedBy;
                    _context.FormQuestions.Attach(row);
                    _context.Entry(row).State = EntityState.Modified;
                }
            }

            _context.SaveChanges();
            rModel.Msg = "Saved Successfully";
            rModel.Success = true;
            return rModel;
        }

        public ReturnMessageModel EditFormQuestion(FormQuestionModel fqModel)
        {
            var row = _context.FormQuestions.Where(x => x.FormQuestionId == fqModel.FormQuestionId).FirstOrDefault();
            if (row == null)
            {
                row = new FormQuestion();
            }
            row.FormCategoryId = fqModel.FormCategoryId;
            row.InputTypeId = fqModel.InputTypeId;
            row.Question = fqModel.Question;
            row.IsNull = fqModel.IsNull;
            row.IsActive = fqModel.IsActive;
            row.DisplayOrder = fqModel.DisplayOrder;
            row.ModifiedDate = System.DateTime.Now;
            row.ModifiedBy = fqModel.ModifiedBy;
            _context.FormQuestions.Attach(row);
            _context.Entry(row).State = EntityState.Modified;
            _context.SaveChanges();
            rModel.Msg = "Saved Successfully";
            rModel.Success = true;
            return rModel;
        }

        public List<QuestionAnswerModel> GetQuestionAnswers(int formCategoryId, int? AnswerId)
        {
            using (DynamicFormEntities context = new DynamicFormEntities())
            {
                var result = context.Database.SqlQuery<QuestionAnswerModel>("SPGetQuestionAnswerByFormCategoryId @formCategoryId, @answerId",
                    new SqlParameter("formCategoryId", formCategoryId),
                    new SqlParameter("answerId", AnswerId ?? SqlInt32.Null)
                    ).ToList();
                return result;
            }
        }
    }
}
