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
    
    public partial class ChiTietTongHop
    {
        public long ID { get; set; }
        public long TongHop_ID { get; set; }
        public long GiangVien_ID { get; set; }
        public Nullable<int> DinhMucGioChuan { get; set; }
        public Nullable<int> DinhMucCongTac { get; set; }
        public Nullable<double> GiamDinhMuc { get; set; }
        public string GhiChu { get; set; }
    
        public virtual GiangVien GiangVien { get; set; }
        public virtual TongHop TongHop { get; set; }
    }
}
