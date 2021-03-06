using System;
using SimpleJSON;

using NUnit.Framework;

[TestFixture]
public class TestTrigger
{
    private Trigger trigger;
    private string trigger_description;

    [SetUp]
    public void Init()
    {
        trigger_description = "{\"scope\": \"ADJACENT\", \"when\": \"ALWAYS\", \"effect\": {\"resource\": \"MONEY\", \"value\": 2}, \"type\": \"GREEN\"}";
        trigger = Trigger.LoadFromJson(JSON.Parse(trigger_description) as JSONClass);
    }

    [Test]
    public void TestLoadFromJson ()
    {
        // We don't actually test the loading from the JSON, just that the SetUp worked fine
        Assert.AreEqual(ETileScope.ADJACENT, trigger.scope);
        Assert.AreEqual(ETileWhen.ALWAYS, trigger.when);
        Assert.AreEqual(new TileType(ETileColor.GREEN).color, trigger.type.color);
        Assert.AreEqual(new Effect(ETileResource.MONEY, 2), trigger.effect);

        // We test the casino's trigger because it's kind of en edge case
        string casino_description = "{\"scope\": \"NONE\", \"when\": \"AFTER_RED_LINE\", \"effect\": {\"resource\": \"INCOME\", \"value\": 1}, \"type\": \"NONE\"}";
        Trigger casino_trigger = Trigger.LoadFromJson(JSON.Parse(casino_description) as JSONClass);
        Assert.AreEqual(new TileType(ETileIcon.NULL), casino_trigger.type);

    }

    [Test]
    public void TestEquals ()
    {
        Trigger trigger_idem = Trigger.LoadFromJson(JSON.Parse(trigger_description) as JSONClass);
        string trigger_description_other = "{\"scope\": \"ADJACENT\", \"when\": \"ALWAYS\", \"effect\": {\"resource\": \"MONEY\", \"value\": 2}, \"type\": \"YELLOW\"}";
        Trigger trigger_other = Trigger.LoadFromJson(JSON.Parse(trigger_description_other) as JSONClass);

        Assert.AreEqual(false, trigger.Equals(trigger_other));
        Assert.AreEqual(true, trigger.Equals(trigger_idem));
    }
}

