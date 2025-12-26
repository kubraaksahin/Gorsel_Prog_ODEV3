using Firebase.Database;
using Firebase.Database.Query;
using System.Diagnostics;
using MauiApp4_odev3.Model;

namespace MauiApp4_odev3.Services;

// Firebase Realtime Database üzerinde yapýlacaklar listesi verilerini yöneten servis sýnýfý
public class TodoFirebase
{
    // Veritabaný baðlantýsý için kullanýlan ana istemci (URL projeme özel)
    private readonly FirebaseClient client = new FirebaseClient("https://gorsel-prog-c2668-default-rtdb.europe-west1.firebasedatabase.app/");

    // Veritabanýna yeni bir görev ekleyen metot 
    public async Task AddTask(TodoTask task) =>
        await client.Child("Tasks").PostAsync(task);

    // Veritabanýndaki tüm görevleri çeken ve bir liste olarak döndüren metot
    public async Task<List<TodoTask>> GetAllTasks()
    {
        try
        {
            // "Tasks" düðümü altýndaki tüm verileri asenkron olarak getirir
            var collection = await client.Child("Tasks").OnceAsync<TodoTask>();

            // Eðer veritabaný boþsa veya veri gelmediyse boþ bir liste döndürür
            if (collection == null) return new List<TodoTask>();

            // Firebase'den gelen ham veriyi , kendi TodoTask modelimize dönüþtürür
            return collection.Select(item => new TodoTask
            {
                // Firebase'in otomatik oluþturduðu benzersiz anahtarý Id alanýna atar
                Id = item.Key,
                Baslik = item.Object.Baslik,
                Detay = item.Object.Detay,
                Tarih = item.Object.Tarih,
                Saat = item.Object.Saat,
                IsDone = item.Object.IsDone
            }).ToList();
        }
        catch (Exception ex)
        {
            // Baðlantý veya veri çekme hatasý oluþursa hatayý hata ayýklama konsoluna yazdýrýr
            Debug.WriteLine($"Sorgu Hatasý: {ex.Message}");
            // Uygulamanýn çökmemesi için hata durumunda boþ bir liste döndürür
            return new List<TodoTask>();
        }
    }

    // Belirtilen benzersiz Id'ye sahip görevi veritabanýndan silen metot
    public async Task DeleteTask(string id)
    {
        // "Tasks" düðümü altýnda ilgili Id'yi bulur ve kalýcý olarak siler
        await client
            .Child("Tasks")
            .Child(id)
            .DeleteAsync();
    }

    // Mevcut bir görevi Id üzerinden güncelleyen metot
    public async Task UpdateTask(TodoTask task)
    {
        // PutAsync metodu, ilgili Id altýndaki tüm veriyi
    }
}