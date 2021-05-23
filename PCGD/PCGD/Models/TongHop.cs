﻿//------------------------------------------------------------------------------
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
    using System.ComponentModel.DataAnnotations;

    public partial class TongHop
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TongHop()
        {
            this.ChiTietTongHop = new HashSet<ChiTietTongHop>();
            this.PhanCong = new HashSet<PhanCong>();
        }
    
        public long ID { get; set; }

        [Display(Name = "Năm học")]
        public int NamHoc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietTongHop> ChiTietTongHop { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhanCong> PhanCong { get; set; }
    }
    public class TongHopModel
    {
        public long PhanCong_ID { get; set; }
        public long GiangVien_ID { get; set; }
        public string TenGV { get; set; }
        public Nullable<int> DinhMucGioChuan { get; set; }
        public Nullable<int> DinhMucCongTac { get; set; }
        public Nullable<double> GiamDinhMuc { get; set; }
        public double? HocKi1 { get; set; }
        public double? HocKi2 { get; set; }
        public string GhiChu { get; set; }
    }
}
