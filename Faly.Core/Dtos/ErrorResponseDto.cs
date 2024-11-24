using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Faly.Core.Dtos;

public class ErrorResponseDto
{
    [Display(Name = "Tür", Description = "Hata türünü belirtir.")]
    [DefaultValue("https://example.com/probs/out-of-credit")]
    public string Type { get; set; }

    [Display(Name = "Başlık", Description = "Hata başlığı.")]
    [DefaultValue("Bir hata oluştu.")]
    public string Title { get; set; }

    [Required]
    [Display(Name = "Durum Kodu", Description = "HTTP durum kodu.")]
    [DefaultValue(500)]
    public int Status { get; set; }

    [Display(Name = "Detay", Description = "Hata detayları.")]
    [DefaultValue("Hata detay bilgisi.")]
    public string Detail { get; set; }

    [Display(Name = "İstek Yolu", Description = "Hatanın oluştuğu istek yolu.")]
    [DefaultValue("/api/example")]
    public string Instance { get; set; }

    [Display(Name = "Özellik 1", Description = "Ek hata bilgisi 1.")]
    public object Property1 { get; set; }

    [Display(Name = "Özellik 2", Description = "Ek hata bilgisi 2.")]
    public object Property2 { get; set; }
}