using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace MauiApp4_odev3.Services
{
    // Firebase kimlik doðrulama ve veritabaný iþlemlerini merkezi olarak yöneten servis sýnýfý
    internal static class FirebaseServices
    {
        // Firebase projesine ait özel kimlik bilgileri 
        static string projectId = "gorsel-prog-c2668";
        static string apiKey = "AIzaSyCu1UXoBC-ECanqzgQ_aATWbt-h0KLsV5w";
        static string authDomain = "gorsel-prog-c2668.firebaseapp.com";
        static string storeBucked = "gorsel-prog-c2668.firebasestorage.app";

        // Firebase Auth iþlemlerini baþlatmak için gerekli olan yapýlandýrma ayarlarý
        static FirebaseAuthConfig config = new FirebaseAuthConfig()
        {
            ApiKey = apiKey,
            AuthDomain = authDomain,
            Providers = new FirebaseAuthProvider[] { new EmailProvider() } // E-posta ve þifre ile giriþ saðlayýcýsý
        };

        // Yeni bir kullanýcý kaydý oluþturan metot
        public static Task<bool> Register(string username, string email, string password, ref string message)
        {
            message = "";
            try
            {
                // Auth istemcisini oluþturur ve yeni kullanýcýyý Firebase'e kaydeder
                var client = new FirebaseAuthClient(config);
                var res = client.CreateUserWithEmailAndPasswordAsync(email, password, username);

                // Kullanýcý baþarýyla oluþturulduysa true, aksi halde false döner
                return Task.FromResult(res.Result.User != null ? true : false);
            }
            catch (Exception ex)
            {
                // Bir hata oluþursa hata mesajýný referans olarak döndürür
                message = ex.Message;
            }
            return Task.FromResult(false);
        }

        // Mevcut bir kullanýcýnýn giriþ yapmasýný saðlayan metot
        public static Task<bool> Login(string email, string password, ref string message)
        {
            message = "";
            try
            {
                // Auth istemcisini oluþturur ve bilgileri Firebase üzerinden kontrol eder
                var client = new FirebaseAuthClient(config);
                var res = client.SignInWithEmailAndPasswordAsync(email, password);

                // Kullanýcý bilgileri doðruysa ve oturum açýldýysa true döner
                return Task.FromResult(res.Result?.User != null ? true : false);
            }
            catch (Exception ex)
            {
                // Hatalý þifre veya e-posta gibi durumlarda hata mesajýný döndürür
                message = ex.Message;
            }
            return Task.FromResult(false);
        }

        // Realtime Database baðlantý adresi
        const string ConnectionString = "https://rehber2025-cdaf0-default-rtdb.firebaseio.com/";

        // Veritabaný üzerinde okuma/yazma iþlemleri yapacak olan ana nesne
        static FirebaseClient firebaseClient = new FirebaseClient(ConnectionString);
    }
}