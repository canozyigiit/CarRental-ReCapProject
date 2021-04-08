namespace Core.Constants
{
    public static class AspectMessages
    {
        
        public static string WrongValidationType = "Yanlış Doğrulama Türü";
        public static string CanNotBeBlank = "Boş bırakılamaz.";
        public static string InvalidEmailAddress = "Geçersiz Biçimde E-posta Adresi.";



       
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
        public static string RentalListed { get; set; }
        public static string RentalNotDelivered = "Araç başkası tarafından kiralanmış.";
        public static string RentalAdded = "Kiralama tamamlandı";
        public static string RentalUpdated = "Kiralama güncellendi";
        public static string RentalDeleted = "Kiralama silindi";
        public static string PaymentError { get; set; }
        public static string CustomerCreditCardsListed = "Müşteri kredi kartları listelendi";

        public static string CustomerListed = "Müşteriler listelendi";

        public static string RentalSuccess = "Araç kiralanabilir";
    }
}