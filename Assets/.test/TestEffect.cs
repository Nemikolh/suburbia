using System;

using NUnit.Framework;

[TestFixture]
public class TestEffect
{
    private Effect effect_money;
    private Effect effect_reputation;

    [SetUp]
    public void Init()
    {
        effect_reputation = new Effect(ETileResource.REPUTATION, 2);
        effect_money = new Effect(ETileResource.MONEY, 10);
    }

    [Test]
    public void TestApply ()
    {
        Player player = new Player();
        player.reputation = 1;

        Assert.AreEqual(1, player.reputation);
        Assert.AreEqual(15, player.money);

        effect_money.Apply (player, 1);
        Assert.AreEqual(25, player.money);

        effect_reputation.Apply (player, 1);
        Assert.AreEqual(3, player.reputation);
    }

    [Test]
    public void TestEquals ()
    {
        Effect effect_money_idem = new Effect(ETileResource.MONEY, 10);

        Assert.AreEqual(false, effect_money.Equals(effect_reputation));
        Assert.AreEqual(true, effect_money.Equals(effect_money_idem));
    }
}

