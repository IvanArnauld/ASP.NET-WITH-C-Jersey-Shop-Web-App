using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExercisesAPI.DAL.DomainClasses
{
    public class TrayItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int TrayId { get; set; }
        [Required]
        public int MenuItemId { get; set; }
        [Required]
        public int Qty { get; set; }
        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[]? Timer { get; set; }
        [ForeignKey("TrayId")]
        public Tray? Tray { get; set; }
        [ForeignKey("MenuItemId")]
        public MenuItem? MenuItem { get; set; }
    }
}