namespace Roblox.Platform.Math.Geography;

using System;

/// <summary>
/// Represents a distance calculator.
/// </summary>
public static class DistanceCalculator
{
    /// <summary>
    /// Calculates distance in miles between two locations
    /// </summary>
    /// <param name="lat1">The latitude of the first location.</param>
    /// <param name="lon1">The longitude of the first location.</param>
    /// <param name="lat2">The latitude of the second location.</param>
    /// <param name="lon2">The longitude of the second location.</param>
    /// <returns>The distance in miles.</returns>
    public static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
    {
        var rlat = Math.PI * lat1 / 180;
        var rlat2 = Math.PI * lat2 / 180;
        var theta = lon1 - lon2;
        var rtheta = Math.PI * theta / 180;

        return Math.Acos(Math.Sin(rlat) * Math.Sin(rlat2) + Math.Cos(rlat) * Math.Cos(rlat2) * Math.Cos(rtheta)) * 180 / Math.PI * 60 * 1.1515;
    }
}
