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
    
    public partial class NhiemVu
    {
        public long ID { get; set; }
        public long Lop_ID { get; set; }
        public long PhanCong_ID { get; set; }
        public long HocPhan_ID { get; set; }
        public long GiangVien_ID { get; set; }
        public byte LoaiPhong { get; set; }
        public Nullable<int> NhomLT { get; set; }
        public Nullable<int> NhomHT { get; set; }
        public string GhiChu { get; set; }
    
        public virtual GiangVien GiangVien { get; set; }
        public virtual HocPhan HocPhan { get; set; }
        public virtual Lop Lop { get; set; }
        public virtual PhanCong PhanCong { get; set; }
    }
}
