using MyModels;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRepositories
{
    public interface IWriterInfoRepository : IBaseRepository<WriterInfo>
    {

    }
    public class WriterInfoRepository : BaseRepository<WriterInfo>, IWriterInfoRepository
    {
        public WriterInfoRepository(ISqlSugarClient context) : base(context)
        {
        }
    }
}
