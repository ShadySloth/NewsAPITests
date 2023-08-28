using Dapper;
using infrastructure.Models;
using Npgsql;

namespace infrastructure;

public class Repository
{
    private readonly NpgsqlDataSource _dataSource;

    public Repository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public IEnumerable<Article> GetAllArticles()
    {
        throw new NotImplementedException();
    }

    public Article CreateArticle(string headline, string author, string body, string articleImgUrl)
    {
        var sql = $@"INSERT INTO 
                    news.articles (headline, author, body, articleImgUrl)
                    VALUES (@headline, @author, @body, @articleImgUrl)
                    RETURNING *";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<Article>(sql, new {headline, author, body, articleImgUrl});
        }
    }

    public Article UpdateArticle(int articleId, string headline, string author, string body, string articleImgUrl)
    {
        throw new NotImplementedException();
    }

    public object DeleteArticle(int articleId)
    {
        throw new NotImplementedException();
    }
}