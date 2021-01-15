using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDMSModel;
namespace EDMSBLL.Interface
{
  public  interface IDropDownListServices
    {
        ReturnMessageModel EditDropDownList(DropDownListModel feModel);
        List<DropDownListModel> GetDropDownList(DropDownListModel fdModel);
        ReturnMessageModel SaveDropDown(DropDownListModel fdModel);
        FormQuestionModel GetFormQuestionIdByDropDownId(int dropDownId);
    }
}
