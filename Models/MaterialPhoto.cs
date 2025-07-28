using abaBackOffice.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("material_photos", Schema = "core")]
public class MaterialPhoto : Auditable
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [Column("ablls_task_id")]
    public int AbllsTaskId { get; set; }
    public AbllsTask AbllsTask { get; set; }

    [Required]
    [Column("name")]
    public string Name { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Column("file_url")]
    public string? FileUrl { get; set; }

    [Column("video_url")]
    public string? VideoUrl { get; set; }
}
