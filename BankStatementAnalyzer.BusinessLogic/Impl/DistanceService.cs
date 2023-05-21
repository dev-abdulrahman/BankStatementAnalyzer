using BankStatementAnalyzer.BusinessLogic.Common;
using BankStatementAnalyzer.BusinessLogic.Interface;
using BankStatementAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace BankStatementAnalyzer.BusinessLogic.Impl
{
    public class DistanceService : IDistanceService
    {
        private readonly ISystemSettingService systemSettingService;

        public DistanceService(ISystemSettingService systemSettingService)
        {
            this.systemSettingService = systemSettingService;
        }

        public bool GetService(double lat, double longitude)
        {
            var sysVal = systemSettingService.FindBy(x => x.SettingType == SettingType.MaxDistance).FirstOrDefault();

            if (sysVal == null)
            {
                return false;
            }

            var center = systemSettingService.FindBy(x => x.SettingType == SettingType.AvailibilityCenterPoint).FirstOrDefault();

            if (center == null)
            {
                return false;
            }

            var ctr = center.Value.Split(',');

            var centerLat = Convert.ToDouble(ctr[0]);
            var centerLong = Convert.ToDouble(ctr[1]);

            double configValue = sysVal == null ? 50000 : Convert.ToDouble(sysVal.Value);
            var val = GeoLocation.GetDistanceBetweenPoints(lat, longitude, centerLat, centerLong);
            if (val <= configValue)
            {
                return true;
            }

            return false;
        }

        public bool GetServiceAvailability(IQueryable<Store> stores, double lat, double longitude)
        {
            Dictionary<int, double> dist = new Dictionary<int, double>();

            foreach (var store in stores)
            {
                var val = GeoLocation.GetDistanceBetweenPoints(lat, longitude, store.Latitude, store.Longitude);
                dist.Add(store.ID, val);
            }

            var sysVal = systemSettingService.FindBy(x => x.SettingType == SettingType.MaxDistance).FirstOrDefault();

            double configValue = sysVal == null ? 50 : Convert.ToDouble(sysVal.Value);

            if (dist.Any(x => x.Value <= configValue))
            {
                return true;
            }

            return false;
        }
    }
}
