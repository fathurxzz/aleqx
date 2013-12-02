using System;

namespace SpaceGame.DataAccess
{
    public class UpgrageFacilityCost
    {
        public static long Cost(short level, long level1Cost)
        {
            return (long) Math.Pow(level1Cost*2, level);
        }
    }
}