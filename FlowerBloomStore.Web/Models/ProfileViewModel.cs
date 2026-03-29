#nullable disable
namespace FlowerBloomStore.Web.Models
{
    public class ProfileViewModel
    {
        // بيانات المستخدم الأساسية
        [Display(Name = "الاسم الأول")]
        public string FirstName { get; set; }

        [Display(Name = "الاسم الأخير")]
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        [Display(Name = "الصورة الشخصية")]
        public string ProfileImage { get; set; }

        [Display(Name = "تاريخ الانضمام")]
        public DateTime MemberSince { get; set; }

        [Display(Name = "عضو مميز")]
        public bool IsPremiumMember { get; set; }

        // الإحصائيات
        [Display(Name = "عدد الطلبات")]
        public int TotalOrders { get; set; }

        [Display(Name = "عدد الفضليات")]
        public int FavoriteCount { get; set; }

        // البيانات الأساسية
        [Display(Name = "البريد الإلكتروني")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "رقم الهاتف")]
        [Phone]
        public string PhoneNumber { get; set; }

        [Display(Name = "تاريخ الميلاد")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }


        // العناوين المحفوظة
        public List<Address> SavedAddresses { get; set; }

        // التفضيلات
        [Display(Name = "اللغة المفضلة")]
        public string PreferredLanguage { get; set; } = "العربية"; // العربية، English

        [Display(Name = "استقبال إشعارات البريد الإلكتروني")]
        public bool ReceiveEmailNotifications { get; set; }

        [Display(Name = "استقبال رسائل SMS")]
        public bool ReceiveSmsNotifications { get; set; }

        [Display(Name = "الاشتراك في النشرة الإخبارية")]
        public bool SubscribeToNewsletter { get; set; }

        // الأمان
        [Display(Name = "تاريخ آخر تغيير كلمة مرور")]
        public DateTime LastPasswordChange { get; set; }

        [Display(Name = "تفعيل المصادقة الثنائية")]
        public bool TwoFactorEnabled { get; set; }
    }

    public class SavedAddress
    {
        public int Id { get; set; }

        [Display(Name = "نوع العنوان")]
        public string AddressType { get; set; } // منزل، عمل، إلخ

        [Display(Name = "التفاصيل")]
        public string Details { get; set; }

        [Display(Name = "العنوان")]
        [Required]
        public string Address { get; set; }

        [Display(Name = "المدينة")]
        public string City { get; set; }

        [Display(Name = "الرمز البريدي")]
        public string PostalCode { get; set; }

        [Display(Name = "الدولة")]
        public string Country { get; set; }

        public bool IsDefault { get; set; }
    }
}
