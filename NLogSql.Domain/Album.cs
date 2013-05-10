using System.Collections.Generic;

namespace NLogSql.Domain
{
    public class Album
    {
        public int AlbumId { get; set; }
        public string Title { get; set; }
        public int ArtistId { get; set; }

        public virtual ICollection<Track> Tracks { get; set; }
    }
}
