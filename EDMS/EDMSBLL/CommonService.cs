using EDMSDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EDMSBLL
{
    public class CommonService
    {
        public static SelectList GetDropDownOptions(int questionId)
        {
            using (DynamicFormEntities _context = new DynamicFormEntities())
            {
              var selectlist=  new SelectList(_context.DropDownLists.Where(x => x.FormQuestionId == questionId).ToList(), "DropDownId", "DropDownName");
                return selectlist;
            }
        }
        public static string ToUpperFirstLetter(string source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            // convert to char array of the string
            char[] letters = source.ToCharArray();
            // upper case the first char
            letters[0] = char.ToUpper(letters[0]);
            // return the array made of the new char array
            return new string(letters);
        }
    }
}
