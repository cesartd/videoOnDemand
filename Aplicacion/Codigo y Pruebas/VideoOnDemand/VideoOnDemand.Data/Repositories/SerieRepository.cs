using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ltracker.Data.Repositories;
using VideoOnDemand.Data;
using VideoOnDemand.Entities;

namespace VideoOnDemand.Repositories
{
    public class SerieRepository : BaseRepository<Serie>
    {
        public SerieRepository(VideoOnDemandContext context) : base(context)
        {

        }
    }
}
