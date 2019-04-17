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
                Data = GetMockData()
            };
        }

        public static double ExampleMockValue => 25.0;

        public static List<List<object>> GetMockData()
        {
            return new List<List<object>>
            {
                new List<object>
                {
                        new DateTime(2012, 1, 1),
                        ExampleMockValue
                },
                new List<object>
                {
                        new DateTime(2012, 1, 2),
                        35.0
                },
                new List<object>
                {
                        new DateTime(2012, 1, 3),
                        45.0
                },
                new List<object>
                {
                        new DateTime(2012, 1, 4),
                        45.0
                },
                new List<object>
                {
                        new DateTime(2012, 1, 5),
                        29.0
                }
            };
        }
    }
}
