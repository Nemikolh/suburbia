using System;
using System.Collections.Generic;

using NUnit.Framework;

[TestFixture]
public class TestTileManager
{
    private TileInstance suburbs;
    private TileInstance park;
    private TileInstance factory;
    private TileInstance suburbs_other;
    private TileInstance park_other;
    private TileInstance factory_other;
    private Player player;
    private Player player_other;

    [SetUp]
    public void Init()
    {
        player = new Player();
        player_other = new Player();

        // We create the first three base tiles instances for both players
        suburbs = new TileInstance();
        suburbs.owner = player;
        park = new TileInstance();
        park.owner = player;
        factory = new TileInstance();
        factory.owner = player;

        suburbs.position = new TilePosition(0, 0);
        park.position = new TilePosition(0, 2);
        factory.position = new TilePosition(0, 4);


        suburbs_other = new TileInstance();
        suburbs_other.owner = player_other;
        park_other = new TileInstance();
        park_other.owner = player_other;
        factory_other = new TileInstance();
        factory_other.owner = player_other;

        suburbs_other.position = new TilePosition(0, 0);
        park_other.position = new TilePosition(0, 2);
        factory_other.position = new TilePosition(0, 4);
    }

    [TearDown]
    public void CleanUp()
    {
        TileManager.RemovePlayer(player);
        TileManager.RemovePlayer(player_other);
    }

    [Test]
    public void TestGetAllTiles ()
    {
        Assert.AreEqual(0, TileManager.GetAllTiles().Count);

        TileManager.AddPlayer(player);
        Assert.AreEqual(3, TileManager.GetAllTiles().Count);

        TileManager.AddPlayer(player_other);
        Assert.AreEqual(6, TileManager.GetAllTiles().Count);

        TileInstance tile = new TileInstance();
        TilePosition pos = new TilePosition(0, 0);
        tile.position = pos;
        tile.owner = player;
        Assert.AreEqual(7, TileManager.GetAllTiles().Count);
    }

    [Test]
    public void TestGetTileOfPlayer()
    {
        List<TileInstance> tiles;

        Assert.AreEqual(null, TileManager.GetTilesOfPlayer(player));

        TileManager.AddPlayer(player);
        tiles = TileManager.GetTilesOfPlayer(player);
        Assert.AreEqual(3, tiles.Count);
        foreach (TileInstance tile in tiles) {
            Assert.AreEqual(player, tile.owner);
        }

        TileManager.AddPlayer(player_other);
        tiles = TileManager.GetTilesOfPlayer(player);
        Assert.AreEqual(3, tiles.Count);
        foreach (TileInstance tile in tiles) {
            Assert.AreEqual(player, tile.owner);
        }
    }

    [Test]
    public void TestGetTileOtherPlayers()
    {
        List<TileInstance> tiles;

        Assert.AreEqual(0, TileManager.GetTilesOfOtherPlayers(player_other).Count);

        TileManager.AddPlayer(player);
        tiles = TileManager.GetTilesOfOtherPlayers(player_other);
        Assert.AreEqual(3, tiles.Count);
        foreach (TileInstance tile in tiles) {
            Assert.AreNotEqual(player_other, tile.owner);
        }

        TileManager.AddPlayer(player_other);
        tiles = TileManager.GetTilesOfOtherPlayers(player_other);
        Assert.AreEqual(3, tiles.Count);
        foreach (TileInstance tile in tiles) {
            Assert.AreNotEqual(player_other, tile.owner);
        }
    }

    [Test]
    public void Manages()
    {
        Assert.AreEqual(false, TileManager.Manages(player));
        Assert.AreEqual(false, TileManager.Manages(player_other));

        TileManager.AddPlayer(player);
        Assert.AreEqual(true, TileManager.Manages(player));
        Assert.AreEqual(false, TileManager.Manages(player_other));

        TileManager.AddPlayer(player_other);
        Assert.AreEqual(true, TileManager.Manages(player));
        Assert.AreEqual(true, TileManager.Manages(player_other));

        TileManager.RemovePlayer(player);
        Assert.AreEqual(false, TileManager.Manages(player));
        Assert.AreEqual(true, TileManager.Manages(player_other));

        TileManager.RemovePlayer(player_other);
        Assert.AreEqual(false, TileManager.Manages(player));
        Assert.AreEqual(false, TileManager.Manages(player_other));


    }
}

