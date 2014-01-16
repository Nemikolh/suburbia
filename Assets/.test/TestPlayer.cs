using System;

using NUnit.Framework;

[TestFixture]
public class TestPlayer
{
    private Player player;


    [SetUp]
    public void Init()
    {
        player = new Player();
    }

    [Test]
    public void TestNumberRedLines()
    {
        int result;

        result = Player.GetNumberRedLinesBetween(10, 10);
        Assert.AreEqual(0, result);

        result = Player.GetNumberRedLinesBetween(10, 14);
        Assert.AreEqual(0, result);

        result = Player.GetNumberRedLinesBetween(10, 15);
        Assert.AreEqual(1, result);

        result = Player.GetNumberRedLinesBetween(10, 22);
        Assert.AreEqual(2, result);

        result = Player.GetNumberRedLinesBetween(15, 10);
        Assert.AreEqual(-1, result);

        result = Player.GetNumberRedLinesBetween(22, 10);
        Assert.AreEqual(-2, result);
    }

    [Test]
    public void TestClampMoney ()
    {
        Assert.AreEqual(15, player.money);

        player.money = 200;
        Assert.AreEqual(200, player.money);

        Assert.AreEqual(2, player.population);
        player.money = -1;
        Assert.AreEqual(0, player.money);
        Assert.AreEqual(1, player.population);
    }

    [Test]
    public void TestClampPopulation ()
    {
        Assert.AreEqual(2, player.population);
        Assert.AreEqual(1, player.reputation);
        Assert.AreEqual(0, player.income);

        // No red lines
        player.population = 10;
        Assert.AreEqual(10, player.population);
        Assert.AreEqual(1, player.reputation);
        Assert.AreEqual(0, player.income);

        // One forward red line
        player.population = 15;
        Assert.AreEqual(15, player.population);
        Assert.AreEqual(0, player.reputation);
        Assert.AreEqual(-1, player.income);

        // One backward red line
        player.population = 10;
        Assert.AreEqual(10, player.population);
        Assert.AreEqual(1, player.reputation);
        Assert.AreEqual(0, player.income);

        // Two forward red lines
        player.population = 22;
        Assert.AreEqual(22, player.population);
        Assert.AreEqual(-1, player.reputation);
        Assert.AreEqual(-2, player.income);

        // Two backward red lines
        player.population = 10;
        Assert.AreEqual(10, player.population);
        Assert.AreEqual(1, player.reputation);
        Assert.AreEqual(0, player.income);
    }

    [Test]
    public void TestClampIncomeReputation()
    {
        Assert.AreEqual(1, player.reputation);
        Assert.AreEqual(0, player.income);

        // Normal set
        player.reputation = 5;
        Assert.AreEqual(5, player.reputation);
        player.income = 5;
        Assert.AreEqual(5, player.income);

        // We hit the lower bound
        player.reputation = -10;
        Assert.AreEqual(-5, player.reputation);
        player.income = -10;
        Assert.AreEqual(-5, player.income);

        // And the upper bound
        player.reputation = 20;
        Assert.AreEqual(15, player.reputation);
        player.income = 20;
        Assert.AreEqual(15, player.income);
    }

    [Test]
    public void CleanUp()
    {
        Assert.AreEqual(15, player.money);
        Assert.AreEqual(2, player.population);
        Assert.AreEqual(1, player.reputation);
        Assert.AreEqual(0, player.income);

        player.CleanUp();
        Assert.AreEqual(15, player.money);
        Assert.AreEqual(3, player.population);
        Assert.AreEqual(1, player.reputation);
        Assert.AreEqual(0, player.income);
    }


}

