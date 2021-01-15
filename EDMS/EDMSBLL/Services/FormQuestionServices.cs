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
  public  class FormQuestionServices : IFormQuestion
    {
        DynamicFormEntities _context;
        ReturnMessageModel rModel;

        public FormQuestionServices()
        {
            _context = new DynamicFormEntities();
            rModel = new ReturnMessageModel();
        }

        public List<FormQuestionModel> GetFormQuestionList(FormQuestionModel fcModel)
        {
            var FormQuestionList = _context.FormQuestions.Select(x => new FormQuestionModel
            {
                FormQuestionId = x.FormQuestionId,
                Question = x.Question,
                FormCategoryId=x.FormCategoryId,
                CreatedDate = x.CreatedDate,
                CreatedBy = x.CreatedBy,
                ModifiedDate = x.ModifiedDate,
                ModifiedBy = x.ModifiedBy
            }).ToList();
            return FormQuestionList;
        }

        public ReturnMessageModel SaveFormQuestion(FormQuestionModel formquestionModel)
        {
            var row = _context.FormQuestions.Where(x => x.FormQuestionId == formquestionModel.FormQuestionId).FirstOrDefault();
            if (row == null)
            {
                row = new FormQuestion();
            }

            row.Question = formquestionModel.Question;

            if (formquestionModel.FormQuestionId == 0)
            {

                row.CreatedDate = System.DateTime.Now;
                row.CreatedBy = formquestionModel.CreatedBy;
                _context.FormQuestions.Add(row);
            }

            else
            {

                row.ModifiedDate = System.DateTime.Now;
                row.ModifiedBy = formquestionModel.ModifiedBy;
                _context.FormQuestions.Attach(row);
                _context.Entry(row).State = System.Data.Entity.EntityState.Modified;
            }
            _context.SaveChanges();
            rModel.Msg = "Saved Successfully";
            rModel.Success = true;
            return rModel;
        }

        public List<QuestionAnswerModel> GetQuestionAnswers(int formCategoryId)
        {
            using (DynamicFormEntities context = new DynamicFormEntities())
            {
                var result = context.Database.SqlQuery<QuestionAnswerModel>("SPGetQuestionAnswerByFormCategoryId "+formCategoryId).ToList();
                return result;
            }
        }
    }
}
