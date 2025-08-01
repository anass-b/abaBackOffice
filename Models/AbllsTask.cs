using abaBackOffice.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("ablls_tasks", Schema = "core")]
public class AbllsTask : Auditable
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [Required, MaxLength(10)]
    [Column("code")]
    public string Code { get; set; }

    [Column("reference")]
    public string? Reference { get; set; }

    [Required]
    [Column("title")]
    public string Title { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Required]
    [ForeignKey("Domain")]
    [Column("domain_id")]
    public int DomainId { get; set; }

    public Domain Domain { get; set; }
    

    // 🎬 Vidéo explicative
    [Column("use_external_explanation_video")]
    public bool UseExternalExplanationVideo { get; set; } = true;

    [Column("explanation_video_url")]
    public string? ExplanationVideoUrl { get; set; }

    [Column("explanation_video_path")]
    public string? ExplanationVideoPath { get; set; }

    [Column("explanation_thumbnail_url")]
    public string? ExplanationThumbnailUrl { get; set; }

    [Column("evaluation_scoring")]
    public string? EvaluationScoring { get; set; }

    [Column("example_consigne")]
    public string? ExampleConsigne { get; set; }

    [Column("expected_response_type")]
    public string? ExpectedResponseType { get; set; }

    [Column("guidance_type")]
    public string? GuidanceType { get; set; }

    [Column("baseline_text")]
    public string? BaselineText { get; set; }
    [Column("expected_criteria_count")]
    public int? ExpectedCriteriaCount { get; set; }

    [Column("status")]
    [MaxLength(50)]
    public string? Status { get; set; } // exemple : "En cours", "Terminé"


    public ICollection<EvaluationCriteria> EvaluationCriterias { get; set; } = [];
    public ICollection<MaterialPhoto> MaterialPhotos { get; set; } = [];
    public ICollection<BaselineContent> BaselineContents { get; set; } = [];
}
