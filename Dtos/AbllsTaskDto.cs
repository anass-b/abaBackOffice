
namespace abaBackOffice.DTOs
{
    public class AbllsTaskDto : AuditableDto
    {
        public int? Id { get; set; }

        public required string Code { get; set; }
        public string? Reference { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public string? Domain { get; set; }

        // 🎬 Vidéo explicative
        public bool UseExternalExplanationVideo { get; set; } = true;
        public string? ExplanationVideoUrl { get; set; }                // Lien YouTube/Vimeo
        public IFormFile? ExplanationVideoFile { get; set; }            // Fichier mp4/mov/...
        public string? ExplanationVideoPath { get; set; }               // Enregistré localement
        public IFormFile? ExplanationThumbnail { get; set; }            // Miniature image
        public string? ExplanationThumbnailUrl { get; set; }

        public string? EvaluationScoring { get; set; }
        public string? ExampleConsigne { get; set; }
        public string? ExpectedResponseType { get; set; }
        public string? GuidanceType { get; set; }
        public string? BaselineText { get; set; }

        public List<EvaluationCriteriaDto>? EvaluationCriterias { get; set; }
        public List<MaterialPhotoDto>? MaterialPhotos { get; set; }
        public List<BaselineContentDto>? BaselineContents { get; set; }
    }

}
