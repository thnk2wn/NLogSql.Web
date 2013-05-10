using System.Collections.Generic;

namespace NLogSql.Domain
{
    public class Artist
    {
        public int ArtistId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
    }
}
