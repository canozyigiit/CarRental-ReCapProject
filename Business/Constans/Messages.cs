using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constans
{
   public class Messages
    {
        public static string AuthorizationDenied = "Yetkili değilsin.";
        public static string UserNotFound = "Kullanıcı bulunamadı.";
        public static string PasswordError = "Password yanlış";
        public static string SuccessfulLogin = "Sisteme giriş başarılı.";
        public static string UserAlreadyExists = "Bu kullanıcı zaten mevcut.";
        public static string UserRegistered = "Kayıt işlemi tamamlandı.";
        public static string AccessTokenCreated = "Token oluşturuldu.";

        public static string BrandLimitExceded = "Marka araç limiti dolu";
        public static string DescriptionAlreadyExists = "Aynı açıklamada zaten bir araç var";

        public static string UserCreditCardAddSuccess = "Kredi kartı başarıyla kaydedildi.";
        public static string PaymentSuccess = "Ödeme işlemi tamamlandı.";

        public static string FindexScoreError = "Findeks puanı yetersiz.";
        public static string FindexScoreSuccess = "Findeks puanı yeterli araç kiralanabilir.";

        public static string RentalNotDelivered = "Araç başkası tarafından kiralanmış.";
        public static string RentalAdded = "Kiralama tamamlandı";
        public static string RentalListed = "Kiralamalar listelendi.";
        public static string RentalUpdated = "Kiralama güncellendi";
        public static string RentalDeleted = "Kiralama silindi";
        public static string RentalSuccess = "Araç kiralanabilir";
        public static string PaymentError { get; set; }

        public static string UserOperationClaimAdded = "Kullanıcı Claim eklendi";
        public static string UserOperationClaimUpdated = "Kullanıcı Cliam güncellendi";
        public static string UserOperationClaimDeleted = "Kullanıcı Cliam silindi";

        public static string OperationClaimAdded = " Claim eklendi";
        public static string OperationClaimDeleted = "Cliam silindi";
        public static string OperationClaimUpdated = "Cliam güncellendi";

        public static string CustomerCreditCardsListed = "Müşteri kredi kartları listelendi";
        public static string CustomerListed = "Müşteriler listelendi";

      
    }
}
