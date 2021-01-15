using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDMSModel;
namespace EDMSBLL.Interface
{
    public interface IFormAnswerDetailServices
    {
        List<FormAnswerDetailModel> GetFormAnswerDetailCategoryList();
        ReturnMessageModel SaveFormAnswerDetailCategory(FormAnswerDetailModel formanswerdetailCategoryModel);
    }
}
