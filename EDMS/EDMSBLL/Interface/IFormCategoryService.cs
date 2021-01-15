using EDMSModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDMSBLL.Interface
{
    public interface IFormCategoryService
    {
        List<FormCategoryModel> GetFormCategoryList(FormCategoryModel fcModel);
        ReturnMessageModel SaveFormCategory(FormCategoryModel formCategoryModel);
        FormCategoryModel GetFormNameById(int formId);
    }
}
