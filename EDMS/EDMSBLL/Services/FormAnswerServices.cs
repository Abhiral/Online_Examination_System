using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDMSDAL;
using EDMSModel;
using EDMSBLL.Interface;
using System.Web;
using System.Text.RegularExpressions;

namespace EDMSBLL.Services
{
    public class FormAnswerServices : IFormAnswerServices
    {
        DynamicFormEntities _context;
        ReturnMessageModel rModel;

        public FormAnswerServices()
        {
            _context = new DynamicFormEntities();
            rModel = new ReturnMessageModel();

        }
        public List<FormAnswerModel> GetFormAnswer(int FormId)
        {
            var questionlist = _context.FormAnswers.Where(x=>x.FormCategoryId == FormId).Select(x => new FormAnswerModel
            {
                AnswerId = x.AnswerId,
                FormCategoryId = x.FormCategoryId,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
                ModifiedBy = x.ModifiedBy,
                ModifiedDate = x.ModifiedDate,
                IsActive = x.IsActive,
                SubmittedBy = x.SubmittedBy,
                FormName = x.FormCategory.FormName
            }).ToList();
            foreach(var item in questionlist)
            {
                var totalMarks = _context.FormAnswerDetails.Where(x => x.AnswerId == item.AnswerId).ToList();
                item.grandTotal = Convert.ToDecimal(totalMarks.Sum(x => x.Marks));
            }
            return questionlist;
        }

        public List<QuestionAnswerModel> GetQuestionDetails(int formId)
        {

            var questionList = (from fc in _context.FormCategories
                                join fq in _context.FormQuestions on fc.FormId equals fq.FormCategoryId
                                join it in _context.InputTypes on fq.InputTypeId equals it.InputTypeId
                                where fc.FormId == formId
                                select new QuestionAnswerModel
                                {
                                    FormQuestionId = fq.FormQuestionId,
                                    Question = fq.Question,
                                    InputName = it.InputName,
                                    InputTypeId = it.InputTypeId,
                                    FormCategoryId = fc.FormId
                                }).ToList();
            return questionList;
        }
        public ReturnMessageModel SaveQuestionAnswer(QuestionAnswerModel faModel)
        {
            try
            {

                if (faModel.AnswerId == null)
                {
                    var formAnswer = new FormAnswer();
                    foreach (var item in faModel.QuestionAnswerList)
                    {
                        var ccRow = new FormAnswerDetail();
                        ccRow.Answer = item.Answer;
                        ccRow.FormQuestionId = item.FormQuestionId;
                        ccRow.Marks = item.Marks;
                        ccRow.CreatedDate = System.DateTime.Now;
                        ccRow.CreatedBy = faModel.CreatedBy;
                        formAnswer.FormAnswerDetails.Add(ccRow);
                    }

                    var studentRow = _context.StudentDetails.Where(x => x.StudentId == faModel.StudentId).FirstOrDefault();
                    studentRow.QuestionSetNo = faModel.FormCategoryId;
                    _context.StudentDetails.Attach(studentRow);
                    _context.Entry(studentRow).State = System.Data.Entity.EntityState.Modified;


                    formAnswer.FormCategoryId = faModel.FormCategoryId;
                    formAnswer.CreatedBy = faModel.CreatedBy;
                    formAnswer.SubmittedBy = faModel.SubmittedBy;
                    formAnswer.IsActive = true;
                    formAnswer.CreatedDate = System.DateTime.Now;
                    _context.FormAnswers.Add(formAnswer);
                    _context.SaveChanges();

                }
                else
                {
                    var answerRow = _context.FormAnswers.Where(x => x.AnswerId == faModel.AnswerId).FirstOrDefault();
                    answerRow.ModifiedBy = faModel.ModifiedBy;
                    answerRow.ModifiedDate = faModel.ModifiedDate;
                    answerRow.SubmittedBy = faModel.SubmittedBy;
                    _context.FormAnswers.Attach(answerRow);

                    foreach (var items in faModel.QuestionAnswerList)
                    {
                        var ansDetalRow = _context.FormAnswerDetails.Where(x => x.AnswerDetailID == items.AnswerDetailID).FirstOrDefault();
                        ansDetalRow.Answer = items.Answer;
                        ansDetalRow.Marks = items.Marks;
                        ansDetalRow.ModifiedBy = items.ModifiedBy;
                        ansDetalRow.ModifiedDate = DateTime.Now;
                        _context.FormAnswerDetails.Attach(ansDetalRow);
                        _context.Entry(ansDetalRow).State = System.Data.Entity.EntityState.Modified;
                    }
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