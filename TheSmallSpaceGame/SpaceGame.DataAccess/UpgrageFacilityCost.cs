using System;

namespace SpaceGame.DataAccess
{
    public class UpgrageFacilityCost
    {
        public static long Cost(short level, long level1Cost)
        {
            return level1Cost * (long)(Math.Pow(2, level-1));
            //return level1Cost * (long)(Math.Pow(2, level)-1);
            //return (long)(level1Cost*(1 - Math.Pow(2, level)))/(1 - 2);
        }
    }
}