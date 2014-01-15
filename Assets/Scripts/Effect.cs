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
using UnityEngine;

public class Effect
{
    private readonly ETileResource m_resource;
    private readonly int m_value;

    public Effect (ETileResource p_resource, int p_value)
    {
        m_resource = p_resource;
        m_value = p_value;
    }

    ETileResource resource {
        get {
            return m_resource;
        }
    }

    int value {
        get {
            return m_value;
        }
    }

    public void Apply (Player p_owner)
    {
        switch (m_resource) {
        case ETileResource.INCOME:
            p_owner.income += m_value;
            break;
        case ETileResource.MONEY:
            p_owner.money += m_value;
            break;
        case ETileResource.POPULATION:
            p_owner.population += m_value;
            break;
        case ETileResource.REPUTATION:
            p_owner.reputation += m_value;
            break;
        case ETileResource.NONE:
            Debug.LogError ("Effect with None resource !");
            break;
        }
    }

    public void Apply (Player p_owner, int n)
    {
        if (n < 0) {
            Debug.LogError ("Negative integer for apply effect !");
            return;
        }
        for (int i = 0; i < n; ++i) {
            this.Apply (p_owner);
        }
    }
}


