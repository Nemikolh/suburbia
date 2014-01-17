using System;
using System.Collections.Generic;
using SimpleJSON;

using NUnit.Framework;

[TestFixture]
public class TestTileManager
{
    private TileManager manager;
    private TileInstance suburbs;
    private TileInstance park;
    private TileInstance factory;
    private TileInstance suburbs_other;
    private TileInstance park_other;
    private TileInstance factory_other;
    private string suburbs_description;
    private string park_description;
    private string factory_description;
    private Tile suburbs_;
    private Tile park_;
    private Tile factory_;
    private Player player;
    private Player player_other;

    public Tile GetTileFromString(string json_string)
    {
        return Tile.LoadFromJson (JSON.Parse (json_string) as JSONClass);
    }

    [SetUp]
    public void Init ()
    {
        manager = new TileManager (0);

        player = new Player ();
        player_other = new Player ();

        suburbs_description = "{\"name\": \"Suburbs\", \"triggers\": [], \"color\": \"GREEN\", \"price\": 3, \"number\": \"0\", \"immediate\": {\"resource\": \"POPULATION\", \"value\": 2}, \"letter\": \"BASE\", \"icon\": \"NONE\"}";
        park_description = "{\"name\": \"Community Park\", \"triggers\": [{\"scope\": \"ADJACENT\", \"when\": \"ALWAYS\", \"effect\": {\"resource\": \"REPUTATION\", \"value\": 1}, \"type\": \"YELLOW\"}, {\"scope\": \"ADJACENT\", \"when\": \"ALWAYS\", \"effect\": {\"resource\": \"REPUTATION\", \"value\": 1}, \"type\": \"GREEN\"}, {\"scope\": \"ADJACENT\", \"when\": \"ALWAYS\", \"effect\": {\"resource\": \"REPUTATION\", \"value\": 1}, \"type\": \"BLUE\"}], \"color\": \"GREY\", \"price\": 4, \"number\": \"0\", \"immediate\": {\"resource\": \"INCOME\", \"value\": -1}, \"letter\": \"BASE\", \"icon\": \"NONE\"}";
        factory_description = "{\"name\": \"Heavy Factory\", \"triggers\": [{\"scope\": \"ADJACENT\", \"when\": \"ALWAYS\", \"effect\": {\"resource\": \"REPUTATION\", \"value\": -1}, \"type\": \"GREY\"}, {\"scope\": \"ADJACENT\", \"when\": \"ALWAYS\", \"effect\": {\"resource\": \"REPUTATION\", \"value\": -1}, \"type\": \"GREEN\"}], \"color\": \"YELLOW\", \"price\": 3, \"number\": \"0\", \"immediate\": {\"resource\": \"INCOME\", \"value\": 1}, \"letter\": \"BASE\", \"icon\": \"NONE\"}";
        suburbs_ = GetTileFromString(suburbs_description);
        park_ = GetTileFromString(park_description);
        factory_ = GetTileFromString(factory_description);

        // We create the first three base tiles instances for both players
        suburbs = new TileInstance (suburbs_);
        suburbs.owner = player;
        park = new TileInstance (park_);
        park.owner = player;
        factory = new TileInstance (factory_);
        factory.owner = player;

        suburbs.position = new TilePosition (0, 0);
        park.position = new TilePosition (0, 2);
        factory.position = new TilePosition (0, 4);


        suburbs_other = new TileInstance (suburbs_);
        suburbs_other.owner = player_other;
        park_other = new TileInstance (park_);
        park_other.owner = player_other;
        factory_other = new TileInstance (factory_);
        factory_other.owner = player_other;

        suburbs_other.position = new TilePosition (0, 0);
        park_other.position = new TilePosition (0, 2);
        factory_other.position = new TilePosition (0, 4);
    }

    [TearDown]
    public void CleanUp ()
    {
        manager.RemovePlayer (player);
        manager.RemovePlayer (player_other);
    }

    [Test]
    public void TestGetAllTiles ()
    {
        Assert.AreEqual (0, manager.GetAllTiles ().Count);

        manager.AddPlayer (player);
        Assert.AreEqual (3, manager.GetAllTiles ().Count);

        manager.AddPlayer (player_other);
        Assert.AreEqual (6, manager.GetAllTiles ().Count);

        TileInstance tile = new TileInstance ();
        TilePosition pos = new TilePosition (0, 0);
        tile.position = pos;
        tile.owner = player;
        Assert.AreEqual (7, manager.GetAllTiles ().Count);
    }

    [Test]
    public void TestGetTileOfPlayer ()
    {
        List<TileInstance> tiles;

        Assert.AreEqual (null, manager.GetTilesOfPlayer (player));

        manager.AddPlayer (player);
        tiles = manager.GetTilesOfPlayer (player);
        Assert.AreEqual (3, tiles.Count);
        foreach (TileInstance tile in tiles) {
            Assert.AreEqual (player, tile.owner);
        }

        manager.AddPlayer (player_other);
        tiles = manager.GetTilesOfPlayer (player);
        Assert.AreEqual (3, tiles.Count);
        foreach (TileInstance tile in tiles) {
            Assert.AreEqual (player, tile.owner);
        }
    }

    [Test]
    public void TestGetTileOtherPlayers ()
    {
        List<TileInstance> tiles;

        Assert.AreEqual (0, manager.GetTilesOfOtherPlayers (player_other).Count);

        manager.AddPlayer (player);
        tiles = manager.GetTilesOfOtherPlayers (player_other);
        Assert.AreEqual (3, tiles.Count);
        foreach (TileInstance tile in tiles) {
            Assert.AreNotEqual (player_other, tile.owner);
        }

        manager.AddPlayer (player_other);
        tiles = manager.GetTilesOfOtherPlayers (player_other);
        Assert.AreEqual (3, tiles.Count);
        foreach (TileInstance tile in tiles) {
            Assert.AreNotEqual (player_other, tile.owner);
        }
    }

    [Test]
    public void TestGetFreePositionsForPlayer ()
    {
        List<TilePosition> free_pos;

        Assert.AreEqual (null, manager.GetFreePositionsForPlayer (player));

        manager.AddPlayer (player);
        free_pos = manager.GetFreePositionsForPlayer (player);
        Assert.AreEqual (7, free_pos.Count);
        Assert.AreEqual (true, free_pos.Contains (new TilePosition (-1, 1)));
        Assert.AreEqual (true, free_pos.Contains (new TilePosition (-1, 3)));
        Assert.AreEqual (true, free_pos.Contains (new TilePosition (-1, 5)));
        Assert.AreEqual (true, free_pos.Contains (new TilePosition (0, 6)));
        Assert.AreEqual (true, free_pos.Contains (new TilePosition (1, 5)));
        Assert.AreEqual (true, free_pos.Contains (new TilePosition (1, 3)));
        Assert.AreEqual (true, free_pos.Contains (new TilePosition (1, 1)));
    }

    [Test]
    public void TestGetAdjacentToOwnLake ()
    {
        List<TileInstance> adjacent_to_lake;

        Assert.AreEqual (null, manager.GetAdjacentToOwnLake (player));

        manager.AddPlayer (player);
        adjacent_to_lake = manager.GetAdjacentToOwnLake (player);
        Assert.AreEqual (0, adjacent_to_lake.Count);

        // We put a lake on the right of the park and the factory
        string lake_description = "{\"name\": \"Lake\", \"triggers\": [{\"scope\": \"ADJACENT\", \"when\": \"ALWAYS\", \"effect\": {\"resource\": \"MONEY\", \"value\": 2}, \"type\": \"YELLOW\"}, {\"scope\": \"ADJACENT\", \"when\": \"ALWAYS\", \"effect\": {\"resource\": \"MONEY\", \"value\": 2}, \"type\": \"GREY\"}, {\"scope\": \"ADJACENT\", \"when\": \"ALWAYS\", \"effect\": {\"resource\": \"MONEY\", \"value\": 2}, \"type\": \"GREEN\"}, {\"scope\": \"ADJACENT\", \"when\": \"ALWAYS\", \"effect\": {\"resource\": \"MONEY\", \"value\": 2}, \"type\": \"BLUE\"}], \"color\": \"LAKE\", \"price\": 0, \"number\": 0, \"immediate\": \"NONE\", \"letter\": \"BASE\", \"icon\": \"NONE\"}";

        Tile description = GetTileFromString(lake_description);
        TileInstance lake = new TileInstance (description);
        TilePosition pos = new TilePosition (1, 3);
        lake.position = pos;
        lake.owner = player;

        adjacent_to_lake = manager.GetAdjacentToOwnLake (player);
        Assert.AreEqual (2, adjacent_to_lake.Count);
        Assert.AreEqual (true, adjacent_to_lake.Contains (park));
        Assert.AreEqual (true, adjacent_to_lake.Contains (factory));
    }

    [Test]
    public void TestManages ()
    {
        Assert.AreEqual (false, manager.Manages (player));
        Assert.AreEqual (false, manager.Manages (player_other));

        manager.AddPlayer (player);
        Assert.AreEqual (true, manager.Manages (player));
        Assert.AreEqual (false, manager.Manages (player_other));

        manager.AddPlayer (player_other);
        Assert.AreEqual (true, manager.Manages (player));
        Assert.AreEqual (true, manager.Manages (player_other));

        manager.RemovePlayer (player);
        Assert.AreEqual (false, manager.Manages (player));
        Assert.AreEqual (true, manager.Manages (player_other));

        manager.RemovePlayer (player_other);
        Assert.AreEqual (false, manager.Manages (player));
        Assert.AreEqual (false, manager.Manages (player_other));
    }

    [Test]
    public void TestHandleNewTileImmediateEffect()
    {
        player.reputation = 0;
        player.income = 0;

        // We add a park in player's burrough
        TileInstance park_new = new TileInstance(park_);
        park_new.position = new TilePosition(1, 1);
        park_new.owner = player;
        manager.HandleNewTileImmediateEffect(park_new);
        // And check that his income is reduced by 1 (immediate effect only)
        Assert.AreEqual(-1, player.income);
    }

    [Test]
    public void TestHandleNewTileConditionalEffectAdjacent ()
    {
        player.income = 0;
        manager.AddPlayer (player);

        // We add a park on the right between the park and the suburbs
        TileInstance park_new = new TileInstance(park_);
        park_new.position = new TilePosition(1, 1);
        park_new.owner = player;
        manager.HandleNewTileConditionalEffect(park_new);
        // And check that his reputation increased by 1 (conditional effect only)
        Assert.AreEqual(1, player.reputation);

        // We add a park on the right of the new park
        TileInstance park_new_ = new TileInstance(park_);
        park_new_.position = new TilePosition(2, 0);
        park_new_.owner = player;
        manager.HandleNewTileConditionalEffect(park_new_);
        // And check that player's reputation hasn't moved (park doesn't trigger on adjacent grey)
        Assert.AreEqual(1, player.reputation);
    }

    [Test]
    public void TestHandleNewTileConditionalEffectOwn ()
    {
        player.money = 0;
        manager.AddPlayer (player);

        // We add a Mint to player's burrough
        string mint_description = "{\"name\": \"Mint\", \"triggers\": [{\"scope\": \"OWN\", \"when\": \"ALWAYS\", \"effect\": {\"resource\": \"MONEY\", \"value\": 2}, \"type\": \"GREY\"}], \"color\": \"GREY\", \"price\": 15, \"number\": 2, \"immediate\": {\"resource\": \"INCOME\", \"value\": 3}, \"letter\": \"A\", \"icon\": \"NONE\"}";
        Tile mint_ = GetTileFromString(mint_description);
        TileInstance mint = new TileInstance(mint_);
        mint.position = new TilePosition(0, 6);
        mint.owner = player;
        manager.HandleNewTileConditionalEffect(mint);
        // And check that the player's money has increased by 4 (conditional effect only)
        Assert.AreEqual(4, player.money);
    }
}

