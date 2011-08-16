namespace Exchange.Models
{
    /// <summary>
    /// Класс для представления сводной таблицы итоговых сумм в разрезе валюты
    /// </summary>
    public class TenderTotalsSummary
    {
        /// <summary>
        /// Валюта
        /// </summary>
        public int CurId { get; set; }

        /// <summary>
        /// Суммма покупки
        /// </summary>
        public decimal AmountBuy { get; set; }

        /// <summary>
        /// Сумма продажи
        /// </summary>
        public decimal AmountSell { get; set; }
    }
}