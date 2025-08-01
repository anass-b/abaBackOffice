using abaBackOffice.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("evaluation_criteria", Schema = "core")]
public class EvaluationCriteria : Auditable
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [ForeignKey("AbllsTask")]
    [Column("ablls_task_id")]
    public int AbllsTaskId { get; set; }
    public AbllsTask AbllsTask { get; set; }

    [Required]
    [Column("label")]
    public string Label { get; set; }

    [Column("consigne")]
    public string? Consigne { get; set; }

    [Column("expected_response")]
    public string? ExpectedResponse { get; set; }

    [Column("guidance_type")]
    public string? GuidanceType { get; set; }

    [Column("use_external_demonstration_video")]
    public bool UseExternalDemonstrationVideo { get; set; } = true;

    [Column("demonstration_video_url")]
    public string? DemonstrationVideoUrl { get; set; }

    [Column("demonstration_video_path")]
    public string? DemonstrationVideoPath { get; set; }

    [Column("demonstration_thumbnail_url")]
    public string? DemonstrationThumbnailUrl { get; set; }


    
    public ICollection<EvaluationCriteriaMaterial> EvaluationCriteriaMaterials { get; set; } = new List<EvaluationCriteriaMaterial>();
}
