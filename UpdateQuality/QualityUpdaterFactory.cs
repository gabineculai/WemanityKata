using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp
{
    public static class QualityUpdaterFactory
    {
        private const string ItemNameAgedBrie = "Aged Brie";
        private const string ItemNameDexterityVest = "+5 Dexterity Vest";
        private const string ItemNameElixir = "Elixir of the Mongoose";
        private const string ItemNameSulfuras = "Sulfuras, Hand of Ragnaros";
        private const string ItemNameBackstage = "Backstage passes to a TAFKAL80ETC concert";
        private const string ItemNameConjured = "Conjured Mana Cake";

        public static IQualityUpdater GetByItem(Item item)
        {
            switch (item.Name)
            {
                case ItemNameAgedBrie:
                    return new QualityUpdaterForAgedBrie();

                case ItemNameBackstage:
                    return new QualityUpdaterForBackstage();

                case ItemNameConjured:
                    return new QualityUpdaterForConjured();

                case ItemNameDexterityVest:
                    return new QualityUpdaterForDexterityVest();

                case ItemNameElixir:
                    return new QualityUpdaterForElixir();

                case ItemNameSulfuras:
                    return new QualityUpdaterForSulfuras();

                default:
                    return new QualityUpdaterDefault();
            }
        }
    }
}
