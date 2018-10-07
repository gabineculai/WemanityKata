using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp
{
    internal class QualityUpdaterForConjured : QualityUpdater
    {
        public QualityUpdaterForConjured() : base()
        {
            
        }

        internal override int IncreaseByQualityForItem(Item item)
        {
            return 2 * IncreaseByQuality;
        }
    }
}
