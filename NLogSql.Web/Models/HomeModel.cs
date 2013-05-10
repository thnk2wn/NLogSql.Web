using System.Collections.Generic;

namespace NLogSql.Web.Models
{
    public class HomeModel
    {
        public IEnumerable<GenreModel> Genres { get; set; }

        public class GenreModel
        {
            public string Name { get; set; }
        }
    }
}