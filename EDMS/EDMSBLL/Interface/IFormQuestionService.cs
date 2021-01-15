using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDMSDAL;
using EDMSModel;

namespace EDMSBLL.Interface
{
    public interface IFormQuestionService
    {
        ReturnMessageModel EditFormQuestion(FormQuestionModel fqModel);
        List<FormQuestionModel> GetFormQuestionList();
        ReturnMessageModel SaveFormQuestion(FormQuestionModel fqModel);
        InputTypeModel GetInputTypeById(int p);
        List<QuestionAnswerModel> GetQuestionAnswers(int formCategoryId, int? AnswerId);
        FormCategoryModel GetFormIdByFormQuestionId(int formQuestionId);
    }
}
