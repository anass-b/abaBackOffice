using abaBackOffice.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("baseline_contents", Schema = "core")]
public class BaselineContent : Auditable
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [ForeignKey("AbllsTask")]
    [Column("ablls_task_id")]
    public int AbllsTaskId { get; set; }
    public AbllsTask AbllsTask { get; set; }

    [ForeignKey("EvaluationCriteria")]
    [Column("criteria_id")]
    public int? CriteriaId { get; set; }
    public EvaluationCriteria? EvaluationCriteria { get; set; }

    [Column("content_html")]
    public string? ContentHtml { get; set; }

    [Column("file_url")]
    public string? FileUrl { get; set; }
}
