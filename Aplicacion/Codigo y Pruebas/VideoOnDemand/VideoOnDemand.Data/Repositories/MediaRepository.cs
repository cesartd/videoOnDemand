using ltracker.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoOnDemand.Data;
using VideoOnDemand.Entities;

namespace VideoOnDemand.Repositories
{
    public class MediaRepository : BaseRepository<Media>
    {
        public MediaRepository(VideoOnDemandContext context) : base(context)
        {

        }
    }
}
