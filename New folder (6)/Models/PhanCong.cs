//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PCGD.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PhanCong
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhanCong()
        {
            this.NhiemVu = new HashSet<NhiemVu>();
        }
    
        public long ID { get; set; }
        public int NguoiDung_ID { get; set; }
        public byte HocKi { get; set; }
        public System.DateTime NgayTao { get; set; }
    
        public virtual NguoiDung NguoiDung { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NhiemVu> NhiemVu { get; set; }
    }
}
