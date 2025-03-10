using MyModels;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRepositories
{
    public interface ITypeInfoRepository : IBaseRepository<TypeInfo>
    {

    }
    public class TypeInfoRepository : BaseRepository<TypeInfo>, ITypeInfoRepository
    {
        public TypeInfoRepository(ISqlSugarClient context) : base(context)
        {
        }
    }
}
