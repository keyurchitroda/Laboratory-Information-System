//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LIS.Model.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class tblpurchase
    {
        public int purchaseid { get; set; }
        public Nullable<int> equipementid { get; set; }
        public Nullable<int> supplierid { get; set; }
        public Nullable<long> quantity { get; set; }
        public Nullable<long> purchaseprice { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]

        public Nullable<System.DateTime> purchasedate { get; set; }
    
        public virtual tblequipement tblequipement { get; set; }
        public virtual tblsupplier tblsupplier { get; set; }
    }
}
