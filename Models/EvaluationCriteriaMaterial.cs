using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using abaBackOffice.Models;

[Table("evaluation_criteria_material", Schema = "core")]
public class EvaluationCriteriaMaterial : Auditable
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [ForeignKey("EvaluationCriteria")]
    [Column("evaluation_criteria_id")]
    public int EvaluationCriteriaId { get; set; }
    public EvaluationCriteria EvaluationCriteria { get; set; }

    [ForeignKey("MaterialPhoto")]
    [Column("material_photo_id")]
    public int MaterialPhotoId { get; set; }
    public MaterialPhoto MaterialPhoto { get; set; }
}
