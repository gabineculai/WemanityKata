# WemanityKata
Execution steps:
1) Download the sources;
2) Open the SLN with VisualStudio 2017;
3) Restore nuGet packages;
4) Execute all unit tests to check the success of the implementation;


Unit tests implemented in order to make sure the changes are not breaking the existin implementation:

a) QualityNoLessThanZero - makes sure quaity doesn't go bellow zero;

b) QualityDecreasesTwiceAsFastAfterAfterSellIn - makes sure that quality decreses twice after the sell in;

c) AgedBrieIncresesInQuality - makes sure AgedBrie quality is increasing in time;

d) QualityIsNeverMoreThan50 - makes sure that except for "Conjured", the items quality is never more than 50;

e) SulfurasNeverSoldAndHasNoQualityChange - makes sure "Sulfuras" quality and sell in is constant;

f) BackStageIncrease1Before10DaysSellIn - makes sure the "Backstage" items quality increses with 1 while SellIn > 10;

g) BackStageIncrease2Before5DaysSellIn - makes sure the "Backstage" items quality increses with 2 while SellIn <= 10 and SellIn > 5;

h) BackStageIncrease3BeforeSellIn - makes sure the "Backstage" items quality increses with 3 while SellIn <= 5 and SellIn > 0;

i) BackStageZeroAfterSellIn - makes sure the "Backstage" items quality is zero after SellIn < 0;

j) ConjuredQualityDecreasesTwiceAsFastAsNormal - makes sure ethe "Conjured" quality decreses twice as fas as default ones;


Implementation solution:

a) Use an extension method to add UpdateQuality method on a Item without changing the class;

b) Create a Factory in order to return the correct QualityUpdater;

c) Execute the returned QualityUpdater;
