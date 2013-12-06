using System;

namespace SpaceGame.DataAccess
{
    public class UpgrageFacilityCost
    {
        public static long Cost(short level, long level1Cost)
        {
            return level1Cost * (long)(Math.Pow(2, level-1));
        }
    }
}