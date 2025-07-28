using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using abaBackOffice.Models;

[Table("evaluation_criteria_material", Schema = "core")]
public class EvaluationCriteriaMaterial : Auditable
{
    [Key, Column(Order = 0)]
    public int EvaluationCriteriaId { get; set; }

    [Key, Column(Order = 1)]
    public int MaterialPhotoId { get; set; }

    [ForeignKey(nameof(EvaluationCriteriaId))]
    public EvaluationCriteria EvaluationCriteria { get; set; } = null!;

    [ForeignKey(nameof(MaterialPhotoId))]
    public MaterialPhoto MaterialPhoto { get; set; } = null!;
}
