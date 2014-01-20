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
    public void TearDown()
    {
        manager.RemoveAllSubscribers();
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

    [Test]
    public void TestSortSubscribedTriggers()
    {
        // We subscribe all player's tiles
        manager.AddSubscriber(suburbs);
        manager.AddSubscriber(park);
        manager.AddSubscriber(factory);

        // We add a park beneath the player's factory
        TileInstance park_new = new TileInstance(park_);
        park_new.position = new TilePosition(0, 6);
        park_new.owner = player;

        List<TriggerInstance> green_subscribers = new List<TriggerInstance>();
        manager.subscribers.TryGetValue(new TileType(ETileColor.GREEN), out green_subscribers);
        Assert.AreEqual(2, green_subscribers.Count);  // factory and park
        green_subscribers = TileManager.SortSubscribedTriggers(green_subscribers, park_new);
        // The first green subscriber is the factory (adjacent)
        Assert.AreEqual(factory, green_subscribers[0].owner);
        // The second one is the park (not adjacent)
        Assert.AreEqual(park, green_subscribers[1].owner);
    }

    [Test]
    public void TestEmitNewTileEvent()
    {
        player.income = 0;
        player.reputation = 0;
        manager.AddPlayer (player);
        manager.AddSubscriber(suburbs);
        manager.AddSubscriber(park);
        manager.AddSubscriber(factory);

        // We add a park beneath the player's factory
        TileInstance park_new = new TileInstance(park_);
        park_new.position = new TilePosition(0, 6);
        park_new.owner = player;
        manager.EmitNewTileEvent(park_new);
        // And check that his reputation decreased by 1 (condtional effect of adjacent factory)
        Assert.AreEqual(-1, player.reputation);
    }

    [Test]
    public void TestAddSubscriber()
    {
        List<TriggerInstance> listeners = new List<TriggerInstance>();

        Assert.AreEqual(0, manager.subscribers.Count);

        // We subscribe a trigger
        string trigger_description = "{\"scope\": \"ADJACENT\", \"when\": \"ALWAYS\", \"effect\": {\"resource\": \"MONEY\", \"value\": 2}, \"type\": \"GREEN\"}";
        Trigger trigger_ = Trigger.LoadFromJson(JSON.Parse(trigger_description) as JSONClass);
        TriggerInstance trigger = new TriggerInstance(trigger_, suburbs);  // Bogus TileInstance
        manager.AddSubscriber(trigger);

        Assert.AreEqual(1, manager.subscribers.Count);
        TileType type_green = new TileType(ETileColor.GREEN);
        Assert.AreEqual(true, manager.subscribers.ContainsKey(type_green));
        manager.subscribers.TryGetValue(type_green, out listeners);
        Assert.AreEqual(1, listeners.Count);
        Assert.AreEqual(true, listeners.Contains(trigger));

        // We subscribe a tile with a trigger
        manager = new TileManager(0);
        Assert.AreEqual(0, manager.subscribers.Count);
        string mint_description = "{\"name\": \"Mint\", \"triggers\": [{\"scope\": \"OWN\", \"when\": \"ALWAYS\", \"effect\": {\"resource\": \"MONEY\", \"value\": 2}, \"type\": \"GREY\"}], \"color\": \"GREY\", \"price\": 15, \"number\": 2, \"immediate\": {\"resource\": \"INCOME\", \"value\": 3}, \"letter\": \"A\", \"icon\": \"NONE\"}";
        Tile mint_ = GetTileFromString(mint_description);
        TileInstance mint = new TileInstance(mint_);
        mint.position = new TilePosition(0, 6);
        mint.owner = player;
        manager.AddSubscriber(mint);

        Assert.AreEqual(1, manager.subscribers.Count);
        TileType type_grey = new TileType(ETileColor.GREY);
        Assert.AreEqual(true, manager.subscribers.ContainsKey(type_grey));
        manager.subscribers.TryGetValue(type_grey, out listeners);
        Assert.AreEqual(true, listeners.Contains(mint.triggers[0]));

        // We subscribe a tile with no trigger
        manager = new TileManager(0);
        Assert.AreEqual(0, manager.subscribers.Count);
        manager.AddSubscriber(suburbs);

        Assert.AreEqual(0, manager.subscribers.Count);
    }

    [Test]
    public void TestHandleTilePlayed()
    {
        player.income = 0;
        player.reputation = 0;
        manager.AddPlayer(player);
        manager.AddSubscriber(suburbs);
        manager.AddSubscriber(park);
        manager.AddSubscriber(factory);

        player_other.income = 0;
        player_other.reputation = 0;
        manager.AddPlayer(player_other);
        manager.AddSubscriber(suburbs_other);
        manager.AddSubscriber(park_other);
        manager.AddSubscriber(factory_other);

        // We add a Park on the right between the park and the factory
        TileInstance park_new = new TileInstance(park_);
        park_new.position = new TilePosition(1, 3);
        park_new.owner = player;

        // We fire the event linked to the new tile being played
        Suburbia.Bus.FireEvent(new EventTilePlayed(park_new, 0));
        Assert.AreEqual(-1, player.income);  // Immediate effect of the new park
        Assert.AreEqual(0, player.reputation);  // The reputation effects of the new park and the factory negate each other
        Assert.AreEqual(0, player_other.income);
        Assert.AreEqual(0, player_other.reputation);  // Other player was not affected
    }

    [Test]
    public void TestWaterfrontRealty()
    {
        // We add a lake on the right of the park and the suburbs
        string lake_description = "{\"name\": \"Lake\", \"triggers\": [{\"scope\": \"ADJACENT\", \"when\": \"ALWAYS\", \"effect\": {\"resource\": \"MONEY\", \"value\": 2}, \"type\": \"YELLOW\"}, {\"scope\": \"ADJACENT\", \"when\": \"ALWAYS\", \"effect\": {\"resource\": \"MONEY\", \"value\": 2}, \"type\": \"GREY\"}, {\"scope\": \"ADJACENT\", \"when\": \"ALWAYS\", \"effect\": {\"resource\": \"MONEY\", \"value\": 2}, \"type\": \"GREEN\"}, {\"scope\": \"ADJACENT\", \"when\": \"ALWAYS\", \"effect\": {\"resource\": \"MONEY\", \"value\": 2}, \"type\": \"BLUE\"}], \"color\": \"LAKE\", \"price\": 0, \"number\": 0, \"immediate\": \"NONE\", \"letter\": \"BASE\", \"icon\": \"NONE\"}";
        Tile description = GetTileFromString(lake_description);
        TileInstance lake = new TileInstance (description);
        lake.position = new TilePosition (1, 1);
        lake.owner = player;

        player.money = 0;
        manager.AddPlayer(player);
        manager.AddSubscriber(suburbs);
        manager.AddSubscriber(park);
        manager.AddSubscriber(factory);
        Suburbia.Bus.FireEvent(new EventTilePlayed(lake, 0));
        Assert.AreEqual(4, player.money);  // 2 adjacent tiles

        // We add a waterfront realty
        string waterfront_description = "{\"name\": \"Waterfront Realty\", \"triggers\": [{\"scope\": \"ADJACENT_TO_OWN_LAKE\", \"when\": \"ALWAYS\", \"effect\": {\"resource\": \"MONEY\", \"value\": 2}, \"type\": \"YELLOW\"}, {\"scope\": \"ADJACENT_TO_OWN_LAKE\", \"when\": \"ALWAYS\", \"effect\": {\"resource\": \"MONEY\", \"value\": 2}, \"type\": \"GREY\"}, {\"scope\": \"ADJACENT_TO_OWN_LAKE\", \"when\": \"ALWAYS\", \"effect\": {\"resource\": \"MONEY\", \"value\": 2}, \"type\": \"GREEN\"}, {\"scope\": \"ADJACENT_TO_OWN_LAKE\", \"when\": \"ALWAYS\", \"effect\": {\"resource\": \"MONEY\", \"value\": 2}, \"type\": \"BLUE\"}], \"color\": \"BLUE\", \"price\": 6, \"number\": 2, \"immediate\": \"NONE\", \"letter\": \"A\", \"icon\": \"NONE\"}";
        Tile description_ = GetTileFromString(waterfront_description);
        TileInstance waterfront_realty = new TileInstance(description_);
        waterfront_realty.position = new TilePosition(0, 6);
        waterfront_realty.owner = player;

        Suburbia.Bus.FireEvent(new EventTilePlayed(waterfront_realty, 0));
        Assert.AreEqual(8, player.money);  // 2 adjacent tiles + waterfront realty
    }

    [Test]
    public void TestHandleRedLinePassed()
    {
        player.income = 0;
        player.reputation = 0;
        manager.AddPlayer(player);

        // We add a casino to player's burrough
        string casino_description = "{\"name\": \"Casino\", \"triggers\": [{\"scope\": \"NONE\", \"when\": \"AFTER_RED_LINE\", \"effect\": {\"resource\": \"INCOME\", \"value\": 1}, \"type\": \"NONE\"}], \"color\": \"BLUE\", \"price\": 22, \"number\": 2, \"immediate\": {\"resource\": \"REPUTATION\", \"value\": -3}, \"letter\": \"B\", \"icon\": \"NONE\"}";
        Tile description = GetTileFromString(casino_description);
        TileInstance casino = new TileInstance(description);
        casino.position = new TilePosition(0, 6);
        casino.owner = player;
        manager.AddSubscriber(casino);

        // player passes 3 red lines backward
        Suburbia.Bus.FireEvent(new EventRedLine(player, -3));
        // And nothing happens
        Assert.AreEqual(0, player.income);
        Assert.AreEqual(0, player.reputation);

        // player passes 3 red lines forward
        Suburbia.Bus.FireEvent(new EventRedLine(player, 3));
        // And it triggers the casino's effect
        Assert.AreEqual(3, player.income);
        Assert.AreEqual(0, player.reputation);

        player.income = 0;

        //And now we don't directly fire the event, we adjust the player's population
        player.population = 22;
        Assert.AreEqual(0, player.income);  // the casino's effect negated the red line
        Assert.AreEqual(-2, player.reputation);  // but the reputation did take a hit
    }
}

