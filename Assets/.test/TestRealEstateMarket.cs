using System;
using System.Collections.Generic;

using NUnit.Framework;

[TestFixture]
public class TestRealEstateMarket
{
    private RealEstateMarket market;

    [SetUp]
    public void Init()
    {
        market = new RealEstateMarket(2);
    }

    [Test]
    public void TestRemoveTile ()
    {
        string first_tile_name, third_tile_name;
        first_tile_name = market[RealEstateMarket.MAX_TILES - 1].description.name;
        third_tile_name = market[RealEstateMarket.MAX_TILES - 3].description.name;

        market.RemoveTile(RealEstateMarket.MAX_TILES - 2);

        Assert.AreEqual(first_tile_name, market[RealEstateMarket.MAX_TILES - 1].description.name);  // The first tile hasn't moved
        Assert.AreEqual(third_tile_name, market[RealEstateMarket.MAX_TILES - 2].description.name);  // The former third one is in second place
    }

    [Test]
    public void TestConstructor ()
    {
        Assert.AreEqual(RealEstateMarket.MAX_TILES, market.tiles.Count);
    }
}
