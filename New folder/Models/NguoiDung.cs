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

    public partial class NguoiDung
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NguoiDung()
        {
            this.PhanCong = new HashSet<PhanCong>();
        }
    
        public int ID { get; set; }

        [Display(Name = "Quyền hạn")]
        [Required(ErrorMessage = "Quyền hạn không được bỏ trống!")]
        [Range(0, 1, ErrorMessage = "Quyền hạn không hợp lệ")]
        public byte QuyenHan { get; set; }

        [Display(Name = "Tài khoản")]
        [Required(ErrorMessage = "Tài khoản không được bỏ trống!")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Tài khoản buộc phải có ít nhất từ 5 - 50 kí tự!")]
        public string TaiKhoan { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu không được bỏ trống!")]
        public string MatKhau { get; set; }

        public System.DateTime NgayTao { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhanCong> PhanCong { get; set; }
    }
}
