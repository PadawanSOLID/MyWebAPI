﻿using SqlSugar;

namespace MyModels
{
    public class BaseId
    {
        [SugarColumn(IsIdentity =true,IsPrimaryKey =true)]
        public int Id { get; set; }
    }
}
