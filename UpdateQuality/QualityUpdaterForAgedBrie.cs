using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp
{
    internal class QualityUpdaterForAgedBrie : QualityUpdater
    {
        public QualityUpdaterForAgedBrie() : base()
        {
            this.IncreaseByQuality = 1;
        }
    }
}
