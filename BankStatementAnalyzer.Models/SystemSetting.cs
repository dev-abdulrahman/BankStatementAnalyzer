using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BankStatementAnalyzer.Models
{
    public class SystemSetting
    {
        public SystemSetting()
        {
            IsActive = true;
            CreatedDate = DateTime.Now;
        }

        [Key]
        public int ID { get; set; }

        public bool IsVisibleToAdmin { get; set; }

        [Required]
        public string SettingTypeValue { get; set; }

        [Display(Name = "Setting Type")]
        public SettingType SettingType
        {
            get
            {
                if (SettingTypeValue != null)
                {
                    return (SettingType)Enum.Parse(typeof(SettingType), SettingTypeValue);
                }
                else
                {
                    return Enum.GetValues(typeof(SettingType)).Cast<SettingType>().First();
                }
            }
            set
            {
                SettingTypeValue = value.ToString();
            }
        }

        [Required]
        [MaxLength(128, ErrorMessage = "Length cannot exceed 128 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9 \)\(\&]+[a-zA-Z0-9 ,.\-\)\(\&' ]+[a-zA-Z0-9.\)\(\&' ]+", ErrorMessage = "Invalid name.")]
        public string Name { get; set; }

        [Required]
        public string Value { get; set; }

        [MaxLength(1024, ErrorMessage = "Length cannot exceed 2048 characters.")]
        //[RegularExpression(@"^[a-zA-Z0-9 \)\(\&]+[a-zA-Z0-9 ,.\-\)\(\&' ]+[a-zA-Z0-9.\)\(\&' ]+", ErrorMessage = "Invalid comment.")]
        public string Comments { get; set; }

        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        public DateTime CreatedDate { get; set; }

        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        public DateTime? UpdatedDate { get; set; }

        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        public DateTime? DeletedDate { get; set; }

        [StringLength(100)]
        [ScaffoldColumn(false)]
        public string CreatedBy { get; set; }

        [StringLength(1000)]
        [ScaffoldColumn(false)]
        public string UpdatedBy { get; set; }

        [StringLength(100)]
        [ScaffoldColumn(false)]
        public string DeletedBy { get; set; }

        [ScaffoldColumn(false)]
        public bool IsActive { get; set; }
    }

    public enum SettingType
    {
        EmailTemplate,
        EmailBodyResetPassword,
        EmailCancelOrderCustomer,
        EmailCancelOrderAdmin,
        EmailAdminContact,
        EmailContactCustomer,
        EmailNewUserAdded,
        PushNotifcationURL,
        PushNotificationServerKey,
        PushNotificationServerId,
        ReferralPoints,
        WalletPointValue,
        MaxDistance,
        DeliveryCharges,
        NewCanBasePrice,
        NewCanReturnRequestMaxDays,
        PushNotificationGroupCreateURL,
        RazorPayKey,
        RazorPaySecret,
        PrivacyPolicy,
        TermsAndCondition,
        RefundPolicy,
        ReferalPointHelpText,
        SalesPointHelpText,
        DefaultCreditValue,
        EmailNewOrder,
        AdminEmails,
        EmailForPickUp,
        AvailibilityCenterPoint,
        NewOrderCustomer,
        OrderCompletedCustomer,
        GridColumns,
        OutOfRating,
        AllowedImageExtensions,
        TesterNumberList,
        MinimumSupportedVersion
    }
}