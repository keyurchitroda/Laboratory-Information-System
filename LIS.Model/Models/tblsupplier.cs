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
    
    public partial class tblsupplier
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblsupplier()
        {
            this.tblpurchases = new HashSet<tblpurchase>();
        }
    
        public int supplierid { get; set; }
        public string suppliername { get; set; }
        public string suppliermoblienumber { get; set; }
        public string supplieremail { get; set; }
        public string supplieraddress { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblpurchase> tblpurchases { get; set; }
    }
}
