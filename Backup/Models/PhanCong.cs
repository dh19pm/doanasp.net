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
    using System.ComponentModel.DataAnnotations;
    
    public partial class PhanCong
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhanCong()
        {
            this.NhiemVu = new HashSet<NhiemVu>();
        }
    
        public long ID { get; set; }
        public long TongHop_ID { get; set; }
        public byte HocKi { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NhiemVu> NhiemVu { get; set; }
        public virtual TongHop TongHop { get; set; }
    }
    public class PhanCongModel
    {
        public long ID { get; set; }
        public int NamHoc { get; set; }
        public byte HocKi { get; set; }
    }
    public class ThemNhiemVuModel
    {
        public long PhanCong_ID { get; set; }

        [Display(Name = "Tên giảng viên")]
        [Required(ErrorMessage = "Tên giảng viên không được bỏ trống!")]
        public string TenGV { get; set; }

        [Display(Name = "Tên lớp")]
        [Required(ErrorMessage = "Tên lớp không được bỏ trống!")]
        public string TenLop { get; set; }

        [Display(Name = "Mã học phần")]
        [Required(ErrorMessage = "Mã học phần không được bỏ trống!")]
        public string MaHP { get; set; }

        [Display(Name = "Loại phòng")]
        [Required(ErrorMessage = "Loại phòng không được bỏ trống!")]
        [Range(0, 1, ErrorMessage = "Loại phòng không hợp lệ")]
        public byte LoaiPhong { get; set; }

        [Display(Name = "Nhóm lý thuyết")]
        public Nullable<int> NhomLT { get; set; }

        [Display(Name = "Nhóm thực hành")]
        public Nullable<int> NhomTH { get; set; }

        [Display(Name = "Ghi chú")]
        public string GhiChu { get; set; }
    }
    public class SuaNhiemVuModel
    {
        public long ID { get; set; }
        public long PhanCong_ID { get; set; }

        [Display(Name = "Tên giảng viên")]
        [Required(ErrorMessage = "Tên giảng viên không được bỏ trống!")]
        public string TenGV { get; set; }

        [Display(Name = "Tên lớp")]
        [Required(ErrorMessage = "Tên lớp không được bỏ trống!")]
        public string TenLop { get; set; }

        [Display(Name = "Mã học phần")]
        [Required(ErrorMessage = "Mã học phần không được bỏ trống!")]
        public string MaHP { get; set; }

        [Display(Name = "Loại phòng")]
        [Required(ErrorMessage = "Loại phòng không được bỏ trống!")]
        [Range(0, 1, ErrorMessage = "Loại phòng không hợp lệ")]
        public byte LoaiPhong { get; set; }

        [Display(Name = "Nhóm lý thuyết")]
        public Nullable<int> NhomLT { get; set; }

        [Display(Name = "Nhóm thực hành")]
        public Nullable<int> NhomTH { get; set; }

        [Display(Name = "Ghi chú")]
        public string GhiChu { get; set; }
    }
    public class XoaNhiemVuModel
    {
        public long ID { get; set; }
        public string TenGV { get; set; }
        public long PhanCong_ID { get; set; }
    }
    public class NhiemVuLopModel
    {
        public long ID { get; set; }
        public string TenLop { get; set; }
        public string TenNganh { get; set; }
    }
    public class NhiemVuNhomHocPhanModel
    {
        public long ID { get; set; }
        public long Lop_ID { get; set; }
        public byte HocPhanDieuKien { get; set; }
        public int TongTC { get; set; }
    }
    public class NhiemVuModel
    {
        public long ID { get; set; }
        public long Lop_ID { get; set; }
        public long NhomHocPhan_ID { get; set; }
        public string TenLop { get; set; }
        public int SoSV { get; set; }
        public string MaHP { get; set; }
        public string TenHP { get; set; }
        public byte LoaiHP { get; set; }
        public int SoTC { get; set; }
        public Nullable<int> SoTietLT { get; set; }
        public Nullable<int> SoTietTH { get; set; }
        public string TenGV { get; set; }
        public byte LoaiPhong { get; set; }
        public Nullable<int> NhomLT { get; set; }
        public Nullable<int> NhomTH { get; set; }
        public string GhiChu { get; set; }
        public decimal? HeSo { get; set; }
        public decimal? TongTietLT { get; set; }
        public decimal? TongTietTH { get; set; }
        public decimal? TongTiet { get; set; }
    }
    public class ViewNhiemVuModel
    {
        public PhanCong PhanCong { get; set; }
        public IEnumerable<NhiemVuLopModel> Lop { get; set; }
        public IEnumerable<NhiemVuNhomHocPhanModel> NhomHocPhan { get; set; }
        public IEnumerable<NhiemVuModel> NhiemVu { get; set; }
    }
}
