using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp
{
    public static class ItemExtensions
    {
        public static void UpdateQuality(this Item item)
        {
            QualityUpdaterFactory.GetByItem(item).UpdateItemQuality(item);
        }
    }
}
