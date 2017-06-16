﻿using System;
using System.Collections.Generic;

namespace Nexosis.Api.Client.Model
{
    public class ChampionInfo
    {
        public DateTime Date { get; set; }

        public Guid SessionId { get; set; }

        public Contestant Champion { get; set; }

        public List<Contestant> Contestants { get; set; }
    }
}