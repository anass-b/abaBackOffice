using Microsoft.AspNetCore.Http;

namespace abaBackOffice.DTOs
{
    public class AbllsTaskDto : AuditableDto
    {
        public int? Id { get; set; }

        public required string Code { get; set; }
        public string? Reference { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }

        public int DomainId { get; set; }               // FK vers domaine
        public DomainDto? Domain { get; set; }          // Pour chargement lié

        // 🎬 Vidéo explicative
        public bool UseExternalExplanationVideo { get; set; } = true;
        public string? ExplanationVideoUrl { get; set; }
        public IFormFile? ExplanationVideoFile { get; set; }
        public string? ExplanationVideoPath { get; set; }
        public IFormFile? ExplanationThumbnail { get; set; }
        public string? ExplanationThumbnailUrl { get; set; }

        public string? EvaluationScoring { get; set; }
        public string? ExampleConsigne { get; set; }
        public string? ExpectedResponseType { get; set; }
        public string? GuidanceType { get; set; }
        public string? BaselineText { get; set; }

        public int? ExpectedCriteriaCount { get; set; }
        public string? Status { get; set; }

        public List<EvaluationCriteriaDto>? EvaluationCriterias { get; set; }
        public List<MaterialPhotoDto>? MaterialPhotos { get; set; }
        public List<BaselineContentDto>? BaselineContents { get; set; }
    }
}
