using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PhuKienAZ.Areas.Admin.Models.DataModel
{
    public class Customer
    {
        [Key]
        [Display(Name = "Mã số")]
        [StringLength(32)]
        public string Id { get; set; }

        [StringLength(64, MinimumLength = 6, ErrorMessage = "Dữ liệu phải có độ dài từ 6 - 64 ký tự")]
        [Display(Name = "Họ tên")]
        [Required(ErrorMessage = "Chưa nhập dữ liệu")]
        public string Name { get; set; }

        [RegularExpression("^0[0-9]{9,10}$", ErrorMessage = "Số điện thoại không đúng định dạng")]
        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Chưa nhập dữ liệu")]
        public string Phone { get; set; }

        [StringLength(10, MinimumLength = 10, ErrorMessage = "Dữ liệu phải có độ dài 10 ký tự")]
        [Display(Name = "Số tài khoản ngân hàng")]
        public string BankAccountCode { get; set; }

        [StringLength(128, MinimumLength = 6, ErrorMessage = "Dữ liệu phải có độ dài từ 6 - 128 ký tự")]
        [Required(ErrorMessage = "Chưa nhập dữ liệu")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [StringLength(128, MinimumLength = 6, ErrorMessage = "Dữ liệu phải có độ dài từ 6 - 128 ký tự")]
        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "Chưa nhập dữ liệu")]
        public string Address { get; set; }

        [StringLength(32, MinimumLength = 6, ErrorMessage = "Dữ liệu phải có độ dài từ 6 - 128 ký tự")]
        [Display(Name = "Tên tài khoản")]
        [Required(ErrorMessage = "Chưa nhập dữ liệu")]
        public string Username { get; set; }

        [StringLength(32, MinimumLength = 6, ErrorMessage = "Dữ liệu phải có độ dài từ 6 - 32 ký tự")]
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Chưa nhập dữ liệu")]
        public string Password { get; set; }

        [Display(Name = "Nam")]
        [Column(TypeName = "bit")]
        public bool Male { get; set; }

        [Display(Name = "Bị vô hiệu hóa")]
        [Column(TypeName = "bit")]
        public bool Disabled { get; set; }

    }
}