using MeSoftCase.Infrastructure.Attributes;

namespace MeSoftCase.Infrastructure.Enums
{
    public enum ExceptionType
    {
        [ExceptionTypeDescription("Beklenmedik bir hata oluştu, daha sonra tekrar deneyiniz.")]
        InternalServerError = 1,
        [ExceptionTypeDescription("IP adresiniz engellenmiştir.")]
        IpBlocked,
        [ExceptionTypeDescription("Bilgilerinizi kontrol ederek tekrar deneyiniz.")]
        Validation,
        [ExceptionTypeDescription("E-posta adresiniz veya şifreniz hatalı.")]
        AppUserServiceSignInNotFound,
        [ExceptionTypeDescription("E-posta adresiniz veya şifreniz hatalı.")]
        AppUserServiceSignInInvalidPassword,
        [ExceptionTypeDescription("Hesabınız üst üste hatalı girişler nedeniyle geçici süre kilitlenmiştir. Daha sonra tekrar deneyiniz.")]
        AppUserServiceSignInLocked,
        [ExceptionTypeDescription("E-posta adresi halihazırda kullanılmaktadır.")]
        AppUserServiceSignUpEmailAlreadyExists,
        [ExceptionTypeDescription("Geçersiz davet kodu.")]
        AppUserInvalidReferralCode,
        [ExceptionTypeDescription("Girmiş olduğunuz E-posta adresi davet kodu ile eşleşmemektedir.")]
        AppUserInvalidReferralEmail,
        [ExceptionTypeDescription("Kötü niyetli, zararlı içerik tespit edilmiştir.")]
        MaliciousInputDetected
    }
}
