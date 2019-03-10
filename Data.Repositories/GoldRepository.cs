using System;
using System.Collections.Generic;
using Data.Model;

namespace Data.Repositories
{
    public class GoldRepository : IGoldRepository
    {
        public GoldDataModel Get()
        {
            return new GoldDataModel
            {
                OldestAvailableDate = DateTime.MinValue.ToLongDateString(),
                NewestAvailaleDate = DateTime.Now.ToLongDateString(),
                DailyGoldDataUnparsed = new List<List<object>>
                {
                    new List<object>
                    {
                         DateTime.Now,
                         5.0
                    }
                }
            };
        }
    }
}
