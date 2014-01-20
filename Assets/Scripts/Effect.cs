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

    public ETileResource resource {
        get {
            return m_resource;
        }
    }

    public int value {
        get {
            return m_value;
        }
    }

    private void Apply (Player p_owner)
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

    public override bool Equals (System.Object p_obj)
    {
        if (p_obj == null) {
            return false;
        }

        Effect effect = p_obj as Effect;
        if ((System.Object)effect == null) {
            return false;
        }

        return (this.resource == effect.resource && this.value == effect.value);
    }

    public override int GetHashCode ()
    {
        unchecked {
            int hash = 17;
            hash = hash * 23 + this.resource.GetHashCode ();
            hash = hash * 23 + this.value.GetHashCode ();
            return hash;
        }
    }
}


