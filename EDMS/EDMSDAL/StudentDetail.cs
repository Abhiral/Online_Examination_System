//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EDMSDAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class StudentDetail
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public long Contact { get; set; }
        public Nullable<int> RegNumber { get; set; }
        public int Class { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<int> QuestionSetNo { get; set; }
    
        public virtual FormCategory FormCategory { get; set; }
    }
}