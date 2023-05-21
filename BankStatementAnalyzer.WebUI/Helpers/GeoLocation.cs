using System;

namespace BankStatementAnalyzer.WebUI.Helpers
{
    public static class GeoLocation
    {
        public static double GetDistanceBetweenPoints(double sourceLat, double sourceLong, double destinationLat, double long2)
        {
            double distance = 0;

            double dLat = (destinationLat - sourceLat) / 180 * Math.PI;
            double dLong = (long2 - sourceLong) / 180 * Math.PI;

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2)
                        + Math.Cos(destinationLat) * Math.Sin(dLong / 2) * Math.Sin(dLong / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            //Calculate radius of earth
            // For this you can assume any of the two points.
            double radiusE = 6378135; // Equatorial radius, in metres
            double radiusP = 6356750; // Polar Radius

            //Numerator part of function
            double nr = Math.Pow(radiusE * radiusP * Math.Cos(sourceLat / 180 * Math.PI), 2);
            //Denominator part of the function
            double dr = Math.Pow(radiusE * Math.Cos(sourceLat / 180 * Math.PI), 2)
                            + Math.Pow(radiusP * Math.Sin(sourceLat / 180 * Math.PI), 2);
            double radius = Math.Sqrt(nr / dr);

            //Calaculate distance in metres.
            distance = radius * c;
            return distance;
        }
    }
}