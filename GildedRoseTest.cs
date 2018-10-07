using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace csharp
{
    [TestFixture]
    public class GildedRoseTest
    {
        private const string ItemNameAgedBrie = "Aged Brie";
        private const string ItemNameDexterityVest = "+5 Dexterity Vest";
        private const string ItemNameElixir = "Elixir of the Mongoose";
        private const string ItemNameSulfuras = "Sulfuras, Hand of Ragnaros";
        private const string ItemNameBackstage = "Backstage passes to a TAFKAL80ETC concert";
        private const string ItemNameConjured = "Conjured Mana Cake";

        private static List<string> Names
        {
            get 
            {
                return new List<string> {
                    ItemNameDexterityVest,
                    ItemNameAgedBrie,
                    ItemNameElixir,
                    ItemNameSulfuras,
                    ItemNameBackstage,
                    ItemNameConjured };
            }
        }

        private static List<string> NamesDecreasingTwiceAfterSellIn
        {
            get
            {
                return new List<string> {
                    ItemNameDexterityVest,
                    ItemNameAgedBrie,
                    ItemNameElixir,
                    ItemNameConjured };
            }
        }

        private static List<Item> GetItems(List<string> names, int quality, int sellIn)
        {
            List<Item> items = new List<Item>();
            foreach (var name in names)
            {
                items.Add(new Item() { Name = name, Quality = quality, SellIn = sellIn });
            }
            return items;
        }

        private static List<Item> GetItemsThatWillHaveIncreasedQuality(int quality, int sellIn)
        {
            return new List<Item>()
            {
                new Item() { Name = ItemNameAgedBrie, Quality = quality, SellIn = sellIn },
                new Item() { Name = ItemNameBackstage, Quality = quality, SellIn = sellIn }
            };
        }

        private static int GetQualityDiffByName(List<Item> itemsBeforeChange, List<Item> itemsAfterChange, string nameToCheck)
        {
            var sellIn20DaysBeforeQualityChangeItem = GetItemByName(itemsBeforeChange, nameToCheck);
            var sellIn20DaysAfterQualityChangeItem = GetItemByName(itemsAfterChange, nameToCheck);

            return sellIn20DaysBeforeQualityChangeItem.Quality - sellIn20DaysAfterQualityChangeItem.Quality;
        }

        private static Item GetItemByName(List<Item> items, string nameToFind)
        {
            var sellIn20DaysBeforeQualityChangeItem = items.FirstOrDefault(i => i.Name.Equals(nameToFind));
            Assert.IsNotNull(sellIn20DaysBeforeQualityChangeItem);
            return sellIn20DaysBeforeQualityChangeItem;
        }

        [Test]
        public void QualityNoLessThanZero()
        {
            List<Item> items = GetItems(Names, 0, 0);    
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();

            foreach (var item in items)
            {
                Assert.GreaterOrEqual(item.Quality, 0);
            }
        }

        [Test]
        public void QualityDecreasesTwiceAsFastAfterAfterSellIn()
        {
            List<Item> sellIn20DaysBeforeQualityChange = GetItems(NamesDecreasingTwiceAfterSellIn, 10, 20);
            List<Item> sellIn20DaysAfterQualityChange = GetItems(NamesDecreasingTwiceAfterSellIn, 10, 20);

            List<Item> sellInMinus20DaysBeforeQualityChange = GetItems(NamesDecreasingTwiceAfterSellIn, 10, -20);
            List<Item> sellInMinus20DaysAfterQualityChange = GetItems(NamesDecreasingTwiceAfterSellIn, 10, -20);

            (new GildedRose(sellIn20DaysAfterQualityChange)).UpdateQuality();
            (new GildedRose(sellInMinus20DaysAfterQualityChange)).UpdateQuality();

            foreach (var name in NamesDecreasingTwiceAfterSellIn)
            {
                int beforeSellInDiff = GetQualityDiffByName(sellIn20DaysBeforeQualityChange, sellIn20DaysAfterQualityChange, name);
                var afterSellInDiff = GetQualityDiffByName(sellInMinus20DaysBeforeQualityChange, sellInMinus20DaysAfterQualityChange, name);

                Assert.AreEqual(afterSellInDiff, 2 * beforeSellInDiff);
            }
        }

        [Test]
        public void AgedBrieIncresesInQuality()
        {
            for (int sellIn = -30; sellIn < 31; sellIn++)
            {
                List<Item> itemsBeforeChange = new List<Item> { new Item() { Name = ItemNameAgedBrie, Quality = 10, SellIn = sellIn } };
                List<Item> itemsAfterChange = new List<Item> { new Item() { Name = ItemNameAgedBrie, Quality = 10, SellIn = sellIn } };

                (new GildedRose(itemsAfterChange)).UpdateQuality();

                var itemBeforeChange = GetItemByName(itemsBeforeChange, ItemNameAgedBrie);
                var itemAfterChange = GetItemByName(itemsAfterChange, ItemNameAgedBrie);

                Assert.Greater(itemAfterChange.Quality, itemBeforeChange.Quality);
            }
        }

        [Test]
        public void QualityIsNeverMoreThan50()
        {
            List<Item> itemsToUpdateQuality = GetItemsThatWillHaveIncreasedQuality(50, 20);

            (new GildedRose(itemsToUpdateQuality)).UpdateQuality();

            foreach (var item in itemsToUpdateQuality)
            {
                Assert.LessOrEqual(item.Quality, 50);
            }
        }

        [Test]
        public void SulfurasNeverSoldAndHasNoQualityChange()
        {
            for (int sellIn = -30; sellIn < 31; sellIn++)
            {
                List<Item> itemsBeforeChange = new List<Item> { new Item() { Name = ItemNameSulfuras, Quality = 10, SellIn = sellIn } };
                List<Item> itemsAfterChange = new List<Item> { new Item() { Name = ItemNameSulfuras, Quality = 10, SellIn = sellIn } };

                (new GildedRose(itemsAfterChange)).UpdateQuality();

                var itemBeforeChange = GetItemByName(itemsBeforeChange, ItemNameSulfuras);
                var itemAfterChange = GetItemByName(itemsAfterChange, ItemNameSulfuras);

                Assert.AreEqual(itemAfterChange.Quality, itemBeforeChange.Quality);
                Assert.AreEqual(itemAfterChange.SellIn, itemBeforeChange.SellIn);
            }
        }

        [Test]
        public void BackStageIncrease1Before10DaysSellIn()
        {
            for (int sellIn = 20; sellIn > 10; sellIn--)
            {
                List<Item> itemsBeforeChange = new List<Item> { new Item() { Name = ItemNameBackstage, Quality = 10, SellIn = sellIn } };
                List<Item> itemsAfterChange = new List<Item> { new Item() { Name = ItemNameBackstage, Quality = 10, SellIn = sellIn } };

                (new GildedRose(itemsAfterChange)).UpdateQuality();

                var itemBeforeChange = GetItemByName(itemsBeforeChange, ItemNameBackstage);
                var itemAfterChange = GetItemByName(itemsAfterChange, ItemNameBackstage);

                Assert.AreEqual(itemAfterChange.Quality, itemBeforeChange.Quality + 1);
            }
        }

        [Test]
        public void BackStageIncrease2Before5DaysSellIn()
        {
            for (int sellIn = 10; sellIn > 5; sellIn--)
            {
                List<Item> itemsBeforeChange = new List<Item> { new Item() { Name = ItemNameBackstage, Quality = 10, SellIn = sellIn } };
                List<Item> itemsAfterChange = new List<Item> { new Item() { Name = ItemNameBackstage, Quality = 10, SellIn = sellIn } };

                (new GildedRose(itemsAfterChange)).UpdateQuality();

                var itemBeforeChange = GetItemByName(itemsBeforeChange, ItemNameBackstage);
                var itemAfterChange = GetItemByName(itemsAfterChange, ItemNameBackstage);

                Assert.AreEqual(itemAfterChange.Quality, itemBeforeChange.Quality + 2);
            }
        }

        [Test]
        public void BackStageIncrease3BeforeSellIn()
        {
            for (int sellIn = 5; sellIn > 0; sellIn--)
            {
                List<Item> itemsBeforeChange = new List<Item> { new Item() { Name = ItemNameBackstage, Quality = 10, SellIn = sellIn } };
                List<Item> itemsAfterChange = new List<Item> { new Item() { Name = ItemNameBackstage, Quality = 10, SellIn = sellIn } };

                (new GildedRose(itemsAfterChange)).UpdateQuality();

                var itemBeforeChange = GetItemByName(itemsBeforeChange, ItemNameBackstage);
                var itemAfterChange = GetItemByName(itemsAfterChange, ItemNameBackstage);

                Assert.AreEqual(itemAfterChange.Quality, itemBeforeChange.Quality + 3);
            }
        }

        [Test]
        public void BackStageZeroAfterSellIn()
        {
            for (int sellIn = -1; sellIn > -10; sellIn--)
            {
                List<Item> itemsBeforeChange = new List<Item> { new Item() { Name = ItemNameBackstage, Quality = 10, SellIn = sellIn } };
                List<Item> itemsAfterChange = new List<Item> { new Item() { Name = ItemNameBackstage, Quality = 10, SellIn = sellIn } };

                (new GildedRose(itemsAfterChange)).UpdateQuality();

                var itemBeforeChange = GetItemByName(itemsBeforeChange, ItemNameBackstage);
                var itemAfterChange = GetItemByName(itemsAfterChange, ItemNameBackstage);

                Assert.AreEqual(itemAfterChange.Quality, 0);
            }
        }

        [Test]
        public void ConjuredQualityDecreasesTwiceAsFastAsNormal()
        {
            List<Item> itemsConjuredBeforeQualityChange = new List<Item> { new Item() { Name = ItemNameConjured, Quality = 10, SellIn = 20 } };
            List<Item> itemsConjuredAfterQualityChange = new List<Item> { new Item() { Name = ItemNameConjured, Quality = 10, SellIn = 20 } };

            List<Item> itemsNormalBeforeQualityChange = new List<Item> { new Item() { Name = ItemNameDexterityVest, Quality = 10, SellIn = 20 } };
            List<Item> itemsNormalAfterQualityChange = new List<Item> { new Item() { Name = ItemNameDexterityVest, Quality = 10, SellIn = 20 } };

            (new GildedRose(itemsConjuredAfterQualityChange)).UpdateQuality();
            (new GildedRose(itemsNormalAfterQualityChange)).UpdateQuality();

            int itemConjuredSellInDiff = GetQualityDiffByName(itemsConjuredBeforeQualityChange, itemsConjuredAfterQualityChange, ItemNameConjured);
            var itemNormalSellInDiff = GetQualityDiffByName(itemsNormalBeforeQualityChange, itemsNormalAfterQualityChange, ItemNameDexterityVest);

            Assert.AreEqual(itemNormalSellInDiff * 2, itemConjuredSellInDiff);
        }
    }
}
