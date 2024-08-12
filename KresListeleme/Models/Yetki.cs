namespace KresListeleme.Models
{
    public class Yetki
    {
        public int YetkiId { get; set; }

        public string? YetkiTuru { get; set; }

        public string? YetkiliAd { get; set; }

        public string? YetkiliIletisim { get; set; }

        public string? YetkiliAdres { get; set; }

        public string YetkiliEMail { get; set; } = null!;

        public string YetkiliSifre { get; set; } = null!;

        public bool? Aktif { get; set; }

        public int? KresId { get; set; }
    }
}
