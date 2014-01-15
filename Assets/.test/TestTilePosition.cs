using System;
using System.Collections.Generic;

using NUnit.Framework;

[TestFixture]
public class TestTilePosition
{
    private TilePosition suburbs_pos;
    private TilePosition park_pos;
    private TilePosition heavy_factory_pos;

    [SetUp]
    public void Init()
    {
        // We create the first three base tiles positions
        suburbs_pos = new TilePosition(0, 0);
        park_pos = new TilePosition(0, 2);
        heavy_factory_pos = new TilePosition(0, 4);
    }

    [Test]
    public void TestIsValidPosition ()
    {
        bool result;

        // (0, 0) is a valid position
        result = TilePosition.IsValidPosition(0, 0);
        Assert.AreEqual(result, true);

        // (2, 2) is a valid position
        result = TilePosition.IsValidPosition(0, 0);
        Assert.AreEqual(result, true);

        // y has to be positive
        result = TilePosition.IsValidPosition(0, -4);
        Assert.AreEqual(result, false);

        // x and y must have the same parity
        result = TilePosition.IsValidPosition(0, 1);
        Assert.AreEqual(result, false);
    }

    [Test]
    public void TestIsAdjacentTo()
    {
        bool result;

        // suburbs and parks are adjacent
        result = suburbs_pos.IsAdjacentTo(park_pos);
        Assert.AreEqual(result, true);

        // suburbs and heavy factory are not adjacent
        result = suburbs_pos.IsAdjacentTo(heavy_factory_pos);
        Assert.AreEqual(result, false);

        // One more try with odds y
        TilePosition tile_pos = new TilePosition(1, 3);
        result = suburbs_pos.IsAdjacentTo(tile_pos);
        Assert.AreEqual(result, false);

        result = park_pos.IsAdjacentTo(tile_pos);
        Assert.AreEqual(result, true);

        result = heavy_factory_pos.IsAdjacentTo(tile_pos);
        Assert.AreEqual(result, true);
    }

    [Test]
    public void TestEquality()
    {
        TilePosition suburbs_pos_idem = new TilePosition(0, 0);
        Assert.AreEqual(suburbs_pos, suburbs_pos_idem);

        Assert.AreNotEqual(suburbs_pos, park_pos);
    }

    [Test]
    public void TestGetAdjacents()
    {
        List<TilePosition> neighbors;

        // Adjacent positions of the suburbs tile
        neighbors = suburbs_pos.GetAdjacentPositions();
        Assert.AreEqual(3, neighbors.Count);
        Assert.AreEqual(true, neighbors.Contains(park_pos));
        Assert.AreEqual(true, neighbors.Contains(new TilePosition(1, 1)));
        Assert.AreEqual(true, neighbors.Contains(new TilePosition(-1, 1)));
        Assert.AreEqual(false, neighbors.Contains(heavy_factory_pos));

        // Adjacent positions of the park tile
        neighbors = park_pos.GetAdjacentPositions();
        Assert.AreEqual(6, neighbors.Count);
        Assert.AreEqual(true, neighbors.Contains(suburbs_pos));
        Assert.AreEqual(true, neighbors.Contains(heavy_factory_pos));
        Assert.AreEqual(true, neighbors.Contains(new TilePosition(1, 1)));
        Assert.AreEqual(true, neighbors.Contains(new TilePosition(-1, 1)));
        Assert.AreEqual(true, neighbors.Contains(new TilePosition(1, 3)));
        Assert.AreEqual(true, neighbors.Contains(new TilePosition(-1, 3)));
        Assert.AreEqual(false, neighbors.Contains(new TilePosition(0, 8)));
    }
}

