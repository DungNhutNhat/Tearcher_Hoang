using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Tearcher_Hoang.Models
{
    public class SanPham
    {
        [Key]
        public string ID { get; set; }

        [Required]
        public string Ten { get; set; }

        [Required]
        public int GiaTien { get; set; }

        [Required]
        public int TrangThai { get; set; }

        [Required]
        public string IDNhaSanXuat { get; set; }
        [ForeignKey("IDNhaSanXuat")]
        public NhaSanXuat nhaSanXuat { get; set; }
    }
}
