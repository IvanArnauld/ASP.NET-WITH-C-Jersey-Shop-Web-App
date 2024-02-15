using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExercisesAPI.DAL.DomainClasses
{
    public class MenuItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; } // generates FK
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int Calories { get; set; }
        [Required]
        public int Carbs { get; set; }
        [Required]
        public int Cholesterol { get; set; }
        [Required]
        public float Fat { get; set; }
        [Required]
        public int Fibre { get; set; }
        [Required]
        [StringLength(200)]
        public string? Description { get; set; }
        [Required]
        public int Protein { get; set; }
        [Required]
        public int Salt { get; set; }
        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[]? Timer { get; set; }
    }
}
