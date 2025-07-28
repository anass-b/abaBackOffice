using abaBackOffice.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("evaluation_criteria_material", Schema = "core")]
public class EvaluationCriteriaMaterial : Auditable
{
    [Key, Column("evaluation_criteria_id", Order = 0)]
    public int EvaluationCriteriaId { get; set; }

    [Key, Column("material_photo_id", Order = 1)]
    public int MaterialPhotoId { get; set; }

    [ForeignKey(nameof(EvaluationCriteriaId))]
    public EvaluationCriteria EvaluationCriteria { get; set; } = null!;

    [ForeignKey(nameof(MaterialPhotoId))]
    public MaterialPhoto MaterialPhoto { get; set; } = null!;
}
