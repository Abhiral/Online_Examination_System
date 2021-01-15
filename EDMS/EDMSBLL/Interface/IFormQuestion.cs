using EDMSModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDMSBLL.Interface
{
 public  interface IFormQuestion
    {
        List<FormQuestionModel> GetFormQuestionList(FormQuestionModel fcModel);
        ReturnMessageModel SaveFormQuestion(FormQuestionModel formquestionModel);
        List<QuestionAnswerModel> GetQuestionAnswers(int formId);
    }
}
