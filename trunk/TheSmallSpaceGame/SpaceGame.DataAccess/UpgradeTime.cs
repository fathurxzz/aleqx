using System;

namespace SpaceGame.DataAccess
{
    public class UpgradeTime
    {
        public static TimeSpan Caclulate(long metal, long crystal, short roboticsFactoryLevel, short naniteFactoryLevel)
        {
            double result1 = (((double)crystal + metal) / 2500);
            double result2 = (1 / ((double)roboticsFactoryLevel + 1));
            double result3 = Math.Pow(0.5, naniteFactoryLevel);
            double result = result1 * result2 * result3*1000;
            var ts = TimeSpan.FromSeconds(result);
            return ts;
        }
    }
}