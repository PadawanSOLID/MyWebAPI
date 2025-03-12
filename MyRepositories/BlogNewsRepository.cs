using Azure;
using MyModels;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyRepositories
{
    public interface IBlogNewsRepository : IBaseRepository<BlogNews>
    {

    }
    public class BlogNewsRepository : BaseRepository<BlogNews>, IBlogNewsRepository
    {
        public BlogNewsRepository(ISqlSugarClient context) : base(context)
        {
        }
        public override async Task<List<BlogNews>> QueryAllAsync()
        {
            return await base.Context.Queryable<BlogNews>()
                .Mapper(c => c.TypeInfo, c => c.TypeId, c => c.TypeInfo.Id)
                .Mapper(c => c.WriterInfo, c => c.WriterId, c => c.WriterInfo.Id)
                .ToListAsync();
        }

        public async override Task<List<BlogNews>> QueryAsync(Expression<Func<BlogNews, bool>> func)
        {
            return await base.Context.Queryable<BlogNews>()
                .Where(func)
                .Mapper(c => c.TypeInfo, c => c.TypeId, c => c.TypeInfo.Id)
                .Mapper(c => c.WriterInfo, c => c.WriterId, c => c.WriterInfo.Id)
                .ToListAsync();
        }


    }
}
