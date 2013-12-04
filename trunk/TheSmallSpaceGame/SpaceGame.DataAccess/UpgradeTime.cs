using System;

namespace SpaceGame.DataAccess
{
    public class UpgradeTime
    {
        public static TimeSpan Caclulate(long metal, long crystal, short robotsLevel, short nanitesLevel, short techLevelTo)
        {
            // Время постройки всех зданий, кроме Фабрики нанитов, Лунной базы, Фаланги и Ворот, снижается (вплоть до 8го уровня)
            var reduction = 1.0;
            //if (techID != 15 && techID != 41 && techID != 42 && techID != 43)
            reduction = Math.Max(4 - techLevelTo / 2.0, 1);
            // Формула ОГейма даёт время в часах - переведём в секунды
            double result = 3600 * (metal + crystal) / (2500.0 * reduction * (robotsLevel + 1.0) * Math.Pow(2.0, nanitesLevel));
            return TimeSpan.FromSeconds(result);
        }
    }
}