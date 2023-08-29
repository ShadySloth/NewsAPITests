using infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using service;

namespace Api.Controllers;

[ApiController]
public class ArticleController : ControllerBase
{
    private readonly Service _service;

    public ArticleController(Service service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("/api/feed")]
    public IEnumerable<Article> GetArticles()
    {
        return _service.GetAllArticles();
    }

    [HttpPost]
    [Route("/api/articles")]
    public Article PostArticle([FromBody] Article article)
    {
        return _service.CreateArticle(article);
    }

    [HttpGet]
    [Route("/api/articles/{articleId}")]
    public Article GetArticle([FromRoute] int articleId)
    {
        return _service.GetArticles(articleId);
    }

    [HttpPut]
    [Route("/api/articles/{articleId}")]
    public Article UpdateArticle([FromBody] Article article, [FromRoute] int articleId)
    {
        return _service.UpdateArticle(articleId, article);
    }

    [HttpDelete]
    [Route("/api/articles/{articleId}")]
    public object DeleteArticle([FromRoute] int articleId)
    {
        return _service.DeleteArticle(articleId);
    }

    [HttpGet]
    [Route("/api/articles")]
    public IEnumerable<Article> SearchArticles([FromQuery] String searchterm, [FromQuery] int pageSize)
    {
        return _service.SearchArticles(searchterm, pageSize);
    }
}