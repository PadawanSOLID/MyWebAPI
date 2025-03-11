using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class ApiResultHelper
    {
        public static ApiResult Success(dynamic data)
        {
            return new ApiResult
            {
                Code = 200,
                Data = data,
                Msg = "Success",
                Total = 0
            };
        }

        public static ApiResult Success(dynamic data,RefAsync<int> total)
        {
            return new ApiResult
            {
                Code = 200,
                Data = data,
                Msg = "Success",
                Total = total
            };
        }

        public static ApiResult Error(string msg)
        {
            return new ApiResult
            {
                Code = 500,
                Data = null,
                Msg = msg,
                Total = 0
            };
        }
    }
}
