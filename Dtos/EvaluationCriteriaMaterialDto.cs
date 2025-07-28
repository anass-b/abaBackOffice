using abaBackOffice.DTOs;

namespace abaBackOffice.DTOs
{
    public class EvaluationCriteriaMaterialDto : AuditableDto
    {
        public int? Id { get; set; }

        public int EvaluationCriteriaId { get; set; }
        public int MaterialPhotoId { get; set; }

        // Optionnel : infos enrichies
        public MaterialPhotoDto? MaterialPhoto { get; set; }
    }
}
