using System;
using System.Collections.Generic;

using NUnit.Framework;

[TestFixture]
public class TestTileInstance
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


    [Test]
    public void TestGetAdjacent ()
    {
        List<TileInstance> neighbors;

        // Adjacent instances of the suburbs tile
        neighbors = suburbs.GetAdjacentInstances();
        Assert.AreEqual(1, neighbors.Count);
        Assert.AreEqual(true, neighbors.Contains(park));
        Assert.AreEqual(false, neighbors.Contains(factory));
        Assert.AreEqual(false, neighbors.Contains(park_other));

        // Adjacent instances of the park tile
        neighbors = park.GetAdjacentInstances();
        Assert.AreEqual(2, neighbors.Count);
        Assert.AreEqual(true, neighbors.Contains(suburbs));
        Assert.AreEqual(true, neighbors.Contains(factory));
        Assert.AreEqual(false, neighbors.Contains(suburbs_other));
        Assert.AreEqual(false, neighbors.Contains(factory_other));
    }

    [Test]
    public void TestGetAdjacentFreePositions ()
    {
        List<TilePosition> free_pos;

        // Free positions adjacent to the suburbs tile
        free_pos = suburbs.GetAdjacentFreePositions();
        Assert.AreEqual(2, free_pos.Count);
        Assert.AreEqual(true, free_pos.Contains(new TilePosition(-1, 1)));
        Assert.AreEqual(true, free_pos.Contains(new TilePosition(1, 1)));
        Assert.AreEqual(false, free_pos.Contains(park.position));

        // Free positions adjacent to the factory tile
        free_pos = factory.GetAdjacentFreePositions();
        Assert.AreEqual(5, free_pos.Count);
        Assert.AreEqual(true, free_pos.Contains(new TilePosition(1, 3)));
        Assert.AreEqual(true, free_pos.Contains(new TilePosition(1, 5)));
        Assert.AreEqual(true, free_pos.Contains(new TilePosition(0, 6)));
        Assert.AreEqual(true, free_pos.Contains(new TilePosition(-1, 5)));
        Assert.AreEqual(true, free_pos.Contains(new TilePosition(-1, 3)));
        Assert.AreEqual(false, free_pos.Contains(park.position));
    }

    [Test]
    public void TestAssignPosition()
    {
        TileInstance instance = new TileInstance();
        TilePosition position = new TilePosition(12, 12);
        instance.position = position;

        Assert.AreEqual(position, instance.position);
        Assert.AreEqual(instance, position.parent);
    }

    [Test]
    public void TestAssignOwner()
    {
        TileInstance instance = new TileInstance();
        TilePosition position = new TilePosition(0, 0);
        Player player = new Player();

        Assert.AreEqual(0, player.tiles.Count);

        instance.owner = player;
        instance.position = position;
        Assert.AreEqual(player, instance.owner);
        Assert.AreEqual(1, player.tiles.Count);
        Assert.AreEqual(true, player.tiles.Contains(instance));

        // And now the other way around
        instance = new TileInstance();
        player = new Player();
        instance.position = position;

        Assert.AreEqual(0, player.tiles.Count);

        player.AddTileInstance(instance);
        Assert.AreEqual(player, instance.owner);
        Assert.AreEqual(1, player.tiles.Count);
        Assert.AreEqual(true, player.tiles.Contains(instance));
    }

    [Test]
    public void TestEquality()
    {
        // Equality works with position and owner
        TileInstance suburbs_idem = new TileInstance();
        suburbs_idem.owner = player;
        suburbs_idem.position = new TilePosition(0, 0);
        Assert.AreEqual(suburbs, suburbs_idem);

        // Different position
        Assert.AreNotEqual(suburbs, park);

        // Different owner
        Assert.AreNotEqual(suburbs, suburbs_other);
    }
}

