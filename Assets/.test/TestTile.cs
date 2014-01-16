using System;
using SimpleJSON;

using NUnit.Framework;

[TestFixture]
public class TestTile
{
    private Tile lake;

    [SetUp]
    public void Init ()
    {
        string lake_description = "{\"name\": \"Lake\", \"triggers\": [{\"scope\": \"ADJACENT\", \"when\": \"ALWAYS\", \"effect\": {\"resource\": \"MONEY\", \"value\": 2}, \"type\": \"YELLOW\"}, {\"scope\": \"ADJACENT\", \"when\": \"ALWAYS\", \"effect\": {\"resource\": \"MONEY\", \"value\": 2}, \"type\": \"GREY\"}, {\"scope\": \"ADJACENT\", \"when\": \"ALWAYS\", \"effect\": {\"resource\": \"MONEY\", \"value\": 2}, \"type\": \"GREEN\"}, {\"scope\": \"ADJACENT\", \"when\": \"ALWAYS\", \"effect\": {\"resource\": \"MONEY\", \"value\": 2}, \"type\": \"BLUE\"}], \"color\": \"LAKE\", \"price\": 0, \"number\": 0, \"immediate\": \"NONE\", \"letter\": \"BASE\", \"icon\": \"NONE\"}";
        lake = Tile.LoadFromJson(JSON.Parse(lake_description) as JSONClass);
    }

    [Test]
    public void TestLoadFromJson ()
    {
        // We don't actually test the loading from the JSON, just that the SetUp worked fine
        Assert.AreEqual("Lake", lake.name);
        Assert.AreEqual(ETileIcon.NONE, lake.icon);
        Assert.AreEqual(ETileLetter.BASE, lake.letter);
        Assert.AreEqual(ETileColor.LAKE, lake.color);
        Assert.AreEqual(0, lake.number);
        Assert.AreEqual(0, lake.price);
        Assert.AreEqual(null, lake.immediate_effect);

        // And the triggers
        Assert.AreEqual(4, lake.triggers.Count);

        string trigger_description;
        trigger_description = "{\"scope\": \"ADJACENT\", \"when\": \"ALWAYS\", \"effect\": {\"resource\": \"MONEY\", \"value\": 2}, \"type\": \"BLUE\"}";
        Trigger trigger_blue = Trigger.LoadFromJson(JSON.Parse(trigger_description) as JSONClass);
        trigger_description = "{\"scope\": \"ADJACENT\", \"when\": \"ALWAYS\", \"effect\": {\"resource\": \"MONEY\", \"value\": 2}, \"type\": \"GREY\"}";
        Trigger trigger_grey = Trigger.LoadFromJson(JSON.Parse(trigger_description) as JSONClass);
        trigger_description = "{\"scope\": \"ADJACENT\", \"when\": \"ALWAYS\", \"effect\": {\"resource\": \"MONEY\", \"value\": 2}, \"type\": \"GREEN\"}";
        Trigger trigger_green = Trigger.LoadFromJson(JSON.Parse(trigger_description) as JSONClass);
        trigger_description = "{\"scope\": \"ADJACENT\", \"when\": \"ALWAYS\", \"effect\": {\"resource\": \"MONEY\", \"value\": 2}, \"type\": \"YELLOW\"}";
        Trigger trigger_yellow = Trigger.LoadFromJson(JSON.Parse(trigger_description) as JSONClass);

        Assert.AreEqual(true, lake.triggers.Contains(trigger_blue));
        Assert.AreEqual(true, lake.triggers.Contains(trigger_green));
        Assert.AreEqual(true, lake.triggers.Contains(trigger_grey));
        Assert.AreEqual(true, lake.triggers.Contains(trigger_yellow));

    }

    [Test]
    public void TestIsOfType ()
    {
        Assert.AreEqual(true, lake.IsOfType(new TileType(ETileColor.LAKE)));
        Assert.AreEqual(true, lake.IsOfType(new TileType(ETileIcon.NONE)));  // Icon is NONE
    }
}

