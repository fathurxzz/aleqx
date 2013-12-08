using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using SpaceGame.Api;
using SpaceGame.Api.Helpers;
using SpaceGame.DataAccess;
using SpaceGame.DataAccess.Entities;

namespace SpaceGame.UI.Helpers
{
    public static class Extensions
    {
        public static string FormatResourcesAmount(this long value)
        {
            return FormatResourcesAmount(value.ToString(CultureInfo.InvariantCulture));
        }

        public static string FormatResourcesAmount(this string value)
        {
            for (int i = value.Length; i > 0; i--)
            {
                if ((value.Length - i + 1) % 4 == 0)
                {
                    value = value.Insert(i, ".");
                }
            }
            return value;
        }

        public static string FormatUpgradeTime(this TimeSpan value)
        {
            var cnt = 0;
            var sb = new StringBuilder();

            if (value.Days / 7 != 0)
            {
                sb.AppendFormat(" {0} w", value.Days / 7);
                cnt++;
            }
            if (cnt == 2)
                return sb.ToString();

            if (value.Days != 0)
            {
                sb.AppendFormat(" {0} d", value.Days);
                cnt++;
            }
            if (cnt == 2)
                return sb.ToString();

            if (value.Hours != 0)
            {
                sb.AppendFormat(" {0} h", value.Hours);
                cnt++;
            }
            if (cnt == 2)
                return sb.ToString();

            if (value.Minutes != 0)
            {
                sb.AppendFormat(" {0} m", value.Minutes);
                cnt++;
            }
            if (cnt == 2)
                return sb.ToString();

            if (value.Seconds != 0)
            {
                sb.AppendFormat(" {0} s", value.Seconds);
            }

            return sb.ToString();
        }



        public static ResourceSet ResourceSet(this IEnumerable<PlanetResource> source)
        {
            return ResourceHelper.GetResourceSet(source);
        }

        public static ResourceAmountSet ResourceAmountSet(this IEnumerable<PlanetResource> source)
        {
            var resourceSet = ResourceSet(source);
            return new ResourceAmountSet
            {
                Metal = (long)resourceSet.Metal.Amount,
                Crystal = (long)resourceSet.Crystal.Amount,
                Deiterium = (long)resourceSet.Deiterium.Amount
            };
        }

        public static ResourceLevelSet ResourceLevelSet(this IEnumerable<PlanetResource> source)
        {

            var resourceSet = ResourceSet(source);
            return new ResourceLevelSet
            {
                MetalMine = resourceSet.Metal.MineLevel,
                CrystalMine = resourceSet.Crystal.MineLevel,
                DeiteriumGenerator = resourceSet.Deiterium.MineLevel
            };
            //return ResourceHelper.GetResourceLevelSet(source);
        }

    }
}