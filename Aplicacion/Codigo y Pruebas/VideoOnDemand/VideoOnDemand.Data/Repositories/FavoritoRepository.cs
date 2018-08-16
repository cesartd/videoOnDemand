using ltracker.Data.Repositories;
using VideoOnDemand.Data;
using VideoOnDemand.Entities;

namespace VideoOnDemand.Repositories
{
    public class FavoritoRepository : BaseRepository<Favorito>
    {

        public FavoritoRepository(VideoOnDemandContext context) : base(context)
        {

        }
    }
}
