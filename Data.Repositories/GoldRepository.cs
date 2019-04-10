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
                NewestAvailaleDate = DateTime.Now.Date.ToLongDateString(),
                DailyGoldPricesUnparsed = new List<List<object>>
                {
                    new List<object>
                    {
                         DateTime.Now.Date,
                         5.0
                    }
                }
            };
        }
    }
}
