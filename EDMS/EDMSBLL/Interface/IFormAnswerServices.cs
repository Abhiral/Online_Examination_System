using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDMSModel;
namespace EDMSBLL.Interface
{
   public interface IFormAnswerServices
    {
       
        List<FormAnswerModel> GetFormAnswer(int FormId);
        ReturnMessageModel SaveQuestionAnswer(QuestionAnswerModel faModel);
        List<QuestionAnswerModel> GetQuestionDetails(int formId);
    }
}
