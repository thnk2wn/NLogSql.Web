using System.Collections.Generic;
using System.Threading.Tasks;
using NLogSql.Domain;

namespace NLogSql.Services
{
    public interface IMusicService
    {
        Task<IList<Artist>> GetArtistsAsync(string name);

        Task<Artist> GetArtistAsync(int artistId);

        Task<IList<Track>> GetTracksAsync(int albumId);

        Task<IList<Genre>> GetGenresAsync();
    }
}
