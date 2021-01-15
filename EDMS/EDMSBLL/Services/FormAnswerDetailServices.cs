using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDMSBLL.Interface;
using EDMSModel;
using EDMSDAL;

namespace EDMSBLL.Services
{
  public class FormAnswerDetailServices : IFormAnswerDetailServices
    {
     
            DynamicFormEntities _context;
            ReturnMessageModel rModel;

            public FormAnswerDetailServices()
            {
                _context = new DynamicFormEntities();
                rModel = new ReturnMessageModel();
            }

            public List<FormAnswerDetailModel> GetFormAnswerDetailCategoryList()
            {
                var FormAnswerDetailCategoryList = _context.FormAnswerDetails.Select(x => new FormAnswerDetailModel
                {
                    AnswerDetailID = x.AnswerDetailID,
                    AnswerId = x.AnswerId,
                    FormQuestionId = x.FormQuestionId,
                    Answer = x.Answer,
                    Marks = x.Marks,
                    CreatedDate = x.CreatedDate,
                    CreatedBy = x.CreatedBy,
                    ModifiedDate = x.ModifiedDate,
                    ModifiedBy = x.ModifiedBy
                }).ToList();
                return FormAnswerDetailCategoryList;
            }

            public ReturnMessageModel SaveFormAnswerDetailCategory(FormAnswerDetailModel formanswerdetailCategoryModel)
            {
                var row = _context.FormAnswerDetails.Where(x => x.AnswerDetailID == formanswerdetailCategoryModel.AnswerDetailID).FirstOrDefault();
                if (row == null)
                {
                    row = new FormAnswerDetail();
                }

                row.AnswerId = formanswerdetailCategoryModel.AnswerId;
                row.Answer = formanswerdetailCategoryModel.Answer;
            row.FormQuestionId = formanswerdetailCategoryModel.FormQuestionId;
            row.Marks = formanswerdetailCategoryModel.Marks;

            if (formanswerdetailCategoryModel.AnswerDetailID == 0)
                {

                    row.CreatedDate = System.DateTime.Now;
                    row.CreatedBy = formanswerdetailCategoryModel.CreatedBy;
                    _context.FormAnswerDetails.Add(row);
                }

                else
                {

                    row.ModifiedDate = System.DateTime.Now;
                    row.ModifiedBy = formanswerdetailCategoryModel.ModifiedBy;
                    _context.FormAnswerDetails.Attach(row);
                    _context.Entry(row).State = System.Data.Entity.EntityState.Modified;
                }
                _context.SaveChanges();
                rModel.Msg = "Saved Successfully";
                rModel.Success = true;
                return rModel;
            }
        }
    }

