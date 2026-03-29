using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace FlowerBloomStore.Web.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "البريد الإلكتروني مطلوب")]
        [EmailAddress(ErrorMessage = "البريد الإلكتروني غير صحيح")]
        [Display(Name = "البريد الإلكتروني")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "كلمة المرور مطلوبة")]
        [StringLength(100, ErrorMessage = "كلمة المرور يجب أن تكون بين {2} و {1} أحرف", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة المرور")]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "تأكيد كلمة المرور")]
        [Compare("Password", ErrorMessage = "كلمة المرور وتأكيدها غير متطابقة")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "الاسم الكامل مطلوب")]
        [Display(Name = "الاسم الكامل")]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "رقم الجوال مطلوب")]
        [Phone(ErrorMessage = "رقم الجوال غير صحيح")]
        [Display(Name = "رقم الجوال")]
        public string PhoneNumber { get; set; } = string.Empty;


        public string Role { get; set; } = "Customer";
    }
}
