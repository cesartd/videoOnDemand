﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoOnDemand.Entities
{
    public class Serie : Media
    {
        public ICollection<Episodio> Episodios { get; set; }

    }
}
