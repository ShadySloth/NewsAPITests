using System.ComponentModel.DataAnnotations;

namespace infrastructure.Models;

public class Article
{
    [MinLength(5)]
    [MaxLength(30)]
    public string Headline { get; set; }
    public string Author { get; set; }
    [MaxLength(1000)]
    public string Body { get; set; }
    public int ArticleId { get; set; }
    public string ArticleImgUrl { get; set; }
}