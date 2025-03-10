using MyModels;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRepositories
{
    public interface IBlogNewsRepository : IBaseRepository<BlogNews>
    {

    }
    public class BlogNewsRepository : BaseRepository<BlogNews>,IBlogNewsRepository
    {
        public BlogNewsRepository(ISqlSugarClient context) : base(context)
        {
        }
    }
}
