namespace abaBackOffice.DTOs;
public class EvaluationCriteriaDto : AuditableDto
{
    public int? Id { get; set; }
    public int? AbllsTaskId { get; set; }

    public required string Label { get; set; }
    public string? Consigne { get; set; }
    public string? ExpectedResponse { get; set; }
    public string? GuidanceType { get; set; }

    // 🎥 Vidéo démonstration
    public bool UseExternalDemonstrationVideo { get; set; } = true;
    public string? DemonstrationVideoUrl { get; set; }              // URL externe
    public IFormFile? DemonstrationVideoFile { get; set; }          // Fichier uploadé
    public string? DemonstrationVideoPath { get; set; }             // Enregistré localement
    public IFormFile? DemonstrationThumbnail { get; set; }          // Miniature
    public string? DemonstrationThumbnailUrl { get; set; }

    public List<int>? MaterialPhotoIds { get; set; }

}

