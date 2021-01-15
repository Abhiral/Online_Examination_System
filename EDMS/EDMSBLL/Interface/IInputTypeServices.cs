using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDMSDAL;
using EDMSModel;

namespace EDMSBLL.Interface
{
    public interface IInputTypeServices
    {
        List<InputTypeModel> GetInputTypeList();
        ReturnMessageModel CreateEditInputTypeCategory(InputTypeModel sModel);
    }

}
