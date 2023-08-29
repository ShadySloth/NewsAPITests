using infrastructure;
using infrastructure.Models;

namespace service;

public class Service
{
    private readonly Repository _repository;

    public Service(Repository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Article> GetAllArticles()
    {
        try
        {
            return _repository.GetAllArticles();
        }
        catch (Exception)
        {
            throw new Exception("Could not get articles.");
        }
    }

    public Article CreateArticle(Article article)
    {
        try
        {
            return _repository.CreateArticle(article);
        }
        catch (Exception)
        {
            throw new Exception("Could not create article.");
        }
    }

    public Article UpdateArticle(int articleId, Article article)
    {
        try
        {
            return _repository.UpdateArticle(articleId, article);
        }
        catch (Exception)
        {
            throw new Exception("Could not update article.");
        }
    }

    public object DeleteArticle(int articleId)
    {
        try
        {
            return _repository.DeleteArticle(articleId);
        }
        catch (Exception)
        {
            throw new Exception("Could not delete article.");
        }
    }

    public Article GetArticles(int articleId)
    {
        try
        {
            return _repository.GetArticle(articleId);
        }
        catch (Exception)
        {
            throw new Exception("Could not get article.");
        }
    }

    public IEnumerable<Article> SearchArticles(string searchterm, int pageSize)
    {
        try
        {
            return _repository.SearchArticles(searchterm, pageSize);
        }
        catch (Exception)
        {
            throw new Exception("Could not search for articles.");
        }
    }
}