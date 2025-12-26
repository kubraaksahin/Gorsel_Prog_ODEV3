namespace MauiApp4_odev3.Model;

public class TodoTask
{
    // Firebase veritabanýndaki benzersiz kayýt anahtarýný  tutar
    public string Id { get; set; }

    // Görevin ana baþlýðý 
    public string Baslik { get; set; }

    // Göreve dair ek açýklamalar 
    public string Detay { get; set; }

    // Görevin yapýlacaðý tarih bilgisi
    public DateTime Tarih { get; set; }

    // Görevin yapýlacaðý saat bilgisi
    public TimeSpan Saat { get; set; }

    // Görevin tamamlanýp tamamlanmadýðýný belirten durum 
    public bool IsDone { get; set; }

    // XAML arayüzünde tarih ve saati yan yana, düzgün bir formatta göstermek için kullanýlan salt okunur özellik
    public string FullDateTime => $"{Tarih:dd/MM/yyyy} {Saat:hh\\:mm}";
}