using Dapper;
using infrastructure.Models;
using Npgsql;

namespace infrastructure;

public class Repository
{
    private readonly NpgsqlDataSource _dataSource;
    private readonly string[] validAuthors = { "Bob", "Rob", "Dob", "Lob" };
    
    public Repository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public IEnumerable<Article> GetAllArticles()
    {
        var sql = $@"SELECT articleid, headline, SUBSTRING(body, 1, 50) AS body, articleimgurl FROM news.articles;";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<Article>(sql);
        }
    }

    public Article CreateArticle(Article article)
    {
        var sql = $@"INSERT INTO 
                    news.articles (headline, author, body, articleimgurl)
                    VALUES (@headline, @author, @body, @articleImgUrl)
                    RETURNING *;";
        if (validAuthors.Contains(article.Author))
        {
            using (var conn = _dataSource.OpenConnection())
            {
                return conn.QueryFirst<Article>(sql, article);
            }
        }

        throw new Exception("Authors name is not valid.");
    }

    public Article UpdateArticle(int articleId, Article article)
    {
        var sql = $@"UPDATE news.articles SET
                    headline = @headline,
                    body = @body,
                    author = @author,
                    articleimgurl = @articleImgUrl
                    WHERE articleid = @articleId
                    RETURNING *;";
        
        if (validAuthors.Contains(article.Author))
        {
            using (var conn = _dataSource.OpenConnection())
            {
                return conn.QueryFirst<Article>(sql, new { articleId, article.Headline, article.ArticleImgUrl, article.Author, article.Body });
            }
        }

        throw new Exception("Authors name is not valid.");
    }

    public object DeleteArticle(int articleId)
    {
        var sql = $@"DELETE FROM news.articles WHERE articleid = @articleId;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Execute(sql, new { articleId }) == 1;
        }
    }

    public Article GetArticle(int articleId)
    {
        var sql = $@"SELECT * FROM news.articles WHERE articleid = @articleId;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<Article>(sql, new { articleId });
        }
    }

    public IEnumerable<Article> SearchArticles(string searchterm, int pageSize)
    {
        var sql = $@"SELECT * FROM news.articles 
                    WHERE body LIKE @searchterm OR
                    WHERE headline LIKE @searchterm
                    LIMIT @pagesize;";
        if (searchterm.Length >= 3)
        {
            using (var conn = _dataSource.OpenConnection())
            {
                return conn.Query<Article>(sql, new {searchterm, pageSize});
            }
        }

        throw new Exception("Search term not long enough.");
    }
}