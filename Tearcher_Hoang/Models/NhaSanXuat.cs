using System.ComponentModel.DataAnnotations;

namespace Tearcher_Hoang.Models
{
    public class NhaSanXuat
    {
        [Key]
        public string ID { get; set; }

        [Required]
        public string Ten { get; set; }
        [Required]
        public string DiaChi { get; set; }

        public ICollection<SanPham> sanPhams { get; set; }
    }
}
