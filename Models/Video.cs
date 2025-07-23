using abaBackOffice.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("videos", Schema = "core")]
public class Video : Auditable
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [Required, MaxLength(200)]
    [Column("title")]
    public string Title { get; set; }

    [Column("description")]
    public string Description { get; set; }

    [MaxLength(100)]
    [Column("category")]
    public string Category { get; set; }

    [Column("duration")]
    public int? Duration { get; set; }

    [Column("url")]
    public string? Url { get; set; }

    [Column("thumbnailurl")]
    public string? ThumbnailUrl { get; set; }

    [Column("ispremium")]
    public bool IsPremium { get; set; } = false;
    [Column("useexternal")]
    public bool UseExternal { get; set; } = false;


}
