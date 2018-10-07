using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp
{
    internal class QualityUpdaterForBackstage : QualityUpdater
    {
        public QualityUpdaterForBackstage() : base()
        {

        }

        internal override int IncreaseByQualityForItem(Item item)
        {
            return (item.SellIn >= 10)
                ? 1
                : (item.SellIn >= 5)
                    ? 2
                    : (item.SellIn >= 0)
                     ? 3
                     : (item.Quality * -1);
        }
    }
}
