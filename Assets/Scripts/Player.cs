//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.18331
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

public class Player
{
    private int m_money;
    private int m_income;
    private int m_reputation;
    private int m_population;
    private List<Tile> m_tiles;

    public Player ()
    {
    }

    private int clampIncomeReputation (int p_value)
    {
        if (p_value < -5)
            return -5;
        if (p_value > 15)
            return 15;
        return p_value;
    }

    private int clampPopulation (int p_value)
    {
        if (p_value < 0) {
            if (m_money >= p_value)
                m_money -= p_value;
            else
                m_money = 0;
            return 0;
        }
        return p_value;
    }

    private int clampMoney (int p_value)
    {
        if (p_value < 0) {
            if (m_population >= p_value)
                m_population -= p_value;
            else
                m_population = 0;
            return 0;
        }
        return p_value;
    }

    public int money {
        get {
            return this.m_money;
        }
        set {
            this.m_money = clampMoney (value);
        }
    }

    public int income {
        get {
            return  this.m_income;
        }
        set {
            this.m_income = clampIncomeReputation (value);
        }
    }

    public int reputation {
        get {
            return this.m_reputation;
        }
        set {
            this.m_reputation = clampIncomeReputation (value);
        }
    }

    public int population {
        get {
            return this.m_population;
        }
        set {
            this.m_population = clampPopulation (value);
        }
    }
}

