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

    public partial class tblpatient
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblpatient()
        {
            this.tblbills = new HashSet<tblbill>();
            this.tblorders = new HashSet<tblorder>();
            this.tblpatienttestresults = new HashSet<tblpatienttestresult>();
            this.tblrecords = new HashSet<tblrecord>();
        }
    
        public int patientid { get; set; }
        public string patientfirstname { get; set; }
        public string patientmiddelname { get; set; }
        public string patientlastname { get; set; }
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> patientdob { get; set; }
        public Nullable<bool> patientmaritalstatus { get; set; }
        public string patientemail { get; set; }
        public string patientmobllienumber { get; set; }
        public string patientemergencynumber { get; set; }
        public Nullable<System.DateTime> created { get; set; }
        public Nullable<System.DateTime> updated { get; set; }
        public string HL7 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblbill> tblbills { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblorder> tblorders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblpatienttestresult> tblpatienttestresults { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblrecord> tblrecords { get; set; }
    }
}
