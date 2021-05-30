//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TMS.WebApi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ExpenseDetail
    {
        public int ExpenseReportId { get; set; }
        public string ExpenseType { get; set; }
        public System.DateTime ExpenseDate { get; set; }
        public int AmountSpent { get; set; }
        public string PaymentType { get; set; }
        public int MRNumber { get; set; }
        public int ReimbursementAccountNo { get; set; }
        public string ExpenseStatus { get; set; }
    
        public virtual ExpenseAccepted ExpenseAccepted { get; set; }
        public virtual TravelDetail TravelDetail { get; set; }
        public virtual ExpenseRejected ExpenseRejected { get; set; }
    }
}
