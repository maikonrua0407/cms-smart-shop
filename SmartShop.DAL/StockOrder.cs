//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmartShop.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class StockOrder
    {
        public StockOrder()
        {
            this.StockOrderDetails = new HashSet<StockOrderDetail>();
        }
    
        public int StockOrderID { get; set; }
        public string StockOrderCode { get; set; }
        public Nullable<int> StockOrderType { get; set; }
        public bool EffectType { get; set; }
        public Nullable<int> ShoppingOrderID { get; set; }
        public int PeriodID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string Deliverer { get; set; }
        public string Receiver { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ApprovedBy { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public Nullable<int> Department { get; set; }
    
        public virtual ICollection<StockOrderDetail> StockOrderDetails { get; set; }
    }
}
