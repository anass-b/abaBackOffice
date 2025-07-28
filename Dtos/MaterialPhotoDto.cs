namespace abaBackOffice.DTOs
{
    public class MaterialPhotoDto : AuditableDto
    {
        public int? Id { get; set; }
        public int? AbllsTaskId { get; set; }

        public required string Name { get; set; }
        public string? Description { get; set; }

        public IFormFile? File { get; set; }         // Image / PDF / Vidéo
        public string? FileUrl { get; set; }         // URL enregistrée

        public string? VideoUrl { get; set; }        // Lien complémentaire externe
    }

}