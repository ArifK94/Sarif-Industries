using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using Sarif_Industries.Models;

namespace Sarif_Industries.PartialModels
{
    public class Country_PM
    {
        public static IEnumerable<Country> GetCountries()
        {
            return CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                 .Select(x => new Country
                 {
                     CountryID = new RegionInfo(x.LCID).Name,
                     CountryName = new RegionInfo(x.LCID).EnglishName
                 })
                 .GroupBy(c => c.CountryID)
                 .Select(c => c.First())
                 .OrderBy(x => x.CountryName);
        }
    }
}