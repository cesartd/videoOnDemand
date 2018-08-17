﻿using ltracker.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoOnDemand.Data;
using VideoOnDemand.Entities;

namespace VideoOnDemand.Repositories
{
    public class MediaOnPlayRepository : BaseRepository<MediaOnPlay>
    {
        public MediaOnPlayRepository(VideoOnDemandContext context) : base(context)
        {

        }
    }
}
