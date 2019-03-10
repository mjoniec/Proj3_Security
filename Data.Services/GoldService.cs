using Data.Repositories;
using System;
using System.Collections.Generic;

namespace Data.Services
{
    public class GoldService : IGoldService
    {
        //TODO
        //initialize GOLD SERVICE MQTT client here

        IGoldRepository _goldRepository;

        public GoldService(IGoldRepository goldRepository)
        {
            _goldRepository = goldRepository;
        }

        IDictionary<DateTime, double> IGoldService.GetAllPriceData()
        {
            var goldData = _goldRepository.Get();

            return goldData.DailyGoldData;
        }

        public double GetNewestPrice()
        {
            var goldData = _goldRepository.Get();
            DateTime.TryParse(goldData.OldestAvailableDate, out DateTime date);

            goldData.DailyGoldData.TryGetValue(date, out double value);

            return value;
        }

        public DateTime GetOldestDay()
        {
            var goldData = _goldRepository.Get();

            DateTime.TryParse(goldData.OldestAvailableDate, out DateTime date);

            return date;
        }
    }
}
