namespace Roblox.Platform.Math.Statistics;

using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Text.RegularExpressions;

/// <summary>
/// Represents a t-distribution.
/// </summary>
public static class TDistribution
{
    private static readonly SortedDictionary<int, double> _90th = new();
    private static readonly SortedDictionary<int, double> _95th = new();
    private static readonly SortedDictionary<int, double> _99th = new();
    private static readonly SortedDictionary<int, double> _99p9th = new();

    private static readonly Assembly _assembly = typeof(TDistribution).Assembly;
    private static readonly string _resourceName = $"{_assembly.GetName().Name}.Statistics.TDistribution.csv";

    /// <summary>
    /// Gets the t-critical value for the specified degrees of freedom and confidence level.
    /// </summary>
    /// <param name="dof">The degrees of freedom.</param>
    /// <param name="conf">The confidence level.</param>
    public static double TCritical(int dof, Confidence conf)
    {
        if (dof <= 0) return double.MaxValue;
        
        int mappedDOF = GetMappedDOF(dof);
        return conf switch
        {
            Confidence.C95 => _95th[mappedDOF],
            Confidence.C99 => _99th[mappedDOF],
            Confidence.C99p9 => _99p9th[mappedDOF],
            _ => _90th[mappedDOF],
        };
    }

    static TDistribution()
    {
        try
        {
            using var csvStream = _assembly.GetManifestResourceStream(_resourceName);
            using var csvReader = new StreamReader(csvStream);

            string line;
            while ((line = csvReader.ReadLine()) != null)
            {
                var values = Regex.Split(line, ",");
                if (values != null && values.Length == 5)
                {
                    int dof = int.Parse(values[0]);
                    _90th.Add(dof, double.Parse(values[1]));
                    _95th.Add(dof, double.Parse(values[2]));
                    _99th.Add(dof, double.Parse(values[3]));
                    _99p9th.Add(dof, double.Parse(values[4]));
                }
            }
        }
        catch (Exception ex)
        {
            throw new MathException("TDistribution could not read CSV file", ex);
        }
    }

    private static int GetMappedDOF(int dof)
    {
        if (dof <= 0) return 0;
        if (dof <= 30 || dof == 120) return dof;
        if (dof < 40) return 30;
        if (dof < 50) return 40;
        if (dof < 60) return 50;
        if (dof < 80) return 60;
        if (dof < 100) return 80;
        if (dof < 120) return 100;
        
        return 121;
    }
}
