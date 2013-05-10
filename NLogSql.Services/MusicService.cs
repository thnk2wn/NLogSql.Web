using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using NLogSql.Data;
using NLogSql.Domain;

namespace NLogSql.Services
{
    public class MusicService : IMusicService
    {
        public async Task<IList<Artist>> GetArtistsAsync(string name)
        {
            using (var context = new ChinookContext())
            {
                var q = context.Artists.AsQueryable();

                if (!string.IsNullOrWhiteSpace(name))
                    q = q.Where(x => x.Name.Contains(name));

                var artists = q.OrderBy(x => x.Name).ToList();
                return artists;
            }
        }

        public async Task<Artist> GetArtistAsync(int artistId)
        {
            using (var context = new ChinookContext())
            {
                var artist = context.Set<Artist>()
                    .Include(x => x.Albums)
                    .FirstOrDefault(x => x.ArtistId == artistId);
                return artist;
            }
        }

        public async Task<IList<Track>> GetTracksAsync(int albumId)
        {
            using (var context = new ChinookContext())
            {
                var tracks = context.Tracks.Where(x => x.AlbumId == albumId)
                                    .ToList();
                return tracks;
            }
        }

        public async Task<IList<Genre>> GetGenresAsync()
        {
            using (var context = new ChinookContext())
            {
                var genres = context.Genres.OrderBy(x => x.Name).ToList();
                return genres;
            }
        }
    }
}
