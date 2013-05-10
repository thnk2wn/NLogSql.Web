using AutoMapper;
using NLogSql.Domain;
using NLogSql.Web.Models;

namespace NLogSql.Web.App_Start
{
    public static class AutoMapping
    {
        public static void CreateMaps()
        {
            Mapper.CreateMap<Genre, HomeModel.GenreModel>();
        }
    }
}