using Microsoft.EntityFrameworkCore;
using TCC_API.Models;

namespace TCC_API.Context
{
    public class CrawlerContext : DbContext
    {
        public CrawlerContext(DbContextOptions<CrawlerContext> options)
            : base(options)
        {
        }
    }
}
