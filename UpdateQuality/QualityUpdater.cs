using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp
{
    internal abstract class QualityUpdater : IQualityUpdater
    {
        internal QualityUpdater()
        {
            this.MaxQuality = 50;
            this.MinQuality = 0;
            this.IncreaseByQuality = -1;
            this.IncreaseBySell = -1;
        }

        protected int IncreaseBySell { get; set; }

        protected int MinQuality { get; set; }

        protected int MaxQuality { get; set; }

        protected virtual int IncreaseByQuality { get; set; }

        internal virtual int IncreaseByQualityForItem(Item item)
        {
            return IncreaseByQuality;
        }

        private int IncreaseQualityMultiplicator(Item item)
        {
            return item.SellIn >= 0
                ? 1
                : 2;
        }

        protected int NewQualityWithoutRangeValidation(Item item)
        {
            return item.Quality + (IncreaseQualityMultiplicator(item) * IncreaseByQualityForItem(item));
        }

        protected int NewQualityWithRangeValidation(Item item)
        {
            return Math.Min(MaxQuality, Math.Max(MinQuality, NewQualityWithoutRangeValidation(item)));
        }

        public virtual void UpdateItemQuality(Item item)
        {
            item.SellIn += IncreaseBySell;
            item.Quality = NewQualityWithRangeValidation(item);
        }
    }
}
