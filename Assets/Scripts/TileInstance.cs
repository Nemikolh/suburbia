// --------------------------------------------------------------- //
// 
// Project : Suburbia
// Author  : Nemikolh
// All Wrongs Reserved.
// --------------------------------------------------------------- //
using System;
using System.Collections.Generic;

public class TileInstance
{
    private readonly Tile m_tile_description;
    private List<TriggerInstance> m_triggers;
    private TilePosition m_position;
    private Player m_owner;

    public TileInstance (Tile p_tile)
    {
        m_tile_description = p_tile;
        m_triggers = new List<TriggerInstance> ();

        foreach (Trigger trigger in m_tile_description.triggers) {
            m_triggers.Add (new TriggerInstance (trigger, this));
        }

        m_owner = null;
    }

    protected TileInstance ()
    {
        m_tile_description = null;
        m_triggers = null;
        m_owner = null;
    }

    public List<TriggerInstance> triggers {
        get {
            return this.m_triggers;
        }
    }

    public bool IsOfType (TileType p_type)
    {
        return this.m_tile_description.IsOfType (p_type);
    }

    public Player owner {
        get {
            return this.m_owner;
        }

        set {
            this.m_owner = value;
        }
    }

    public ETileColor color {
        get {
            return this.m_tile_description.color;
        }
    }

    public TilePosition position {
        get {
            return this.m_position;
        }

        set {
            this.m_position = value;
        }
    }

    public List<TileType> types {
        get {
            return this.m_tile_description.types;
        }
    }

    public bool IsAdjacentTo (TileInstance p_other)
    {
        if (this.owner == p_other.owner)
            return this.position.IsAdjacentTo (p_other.position);
        return false;
    }

    public void ApplyImmediateEffect ()
    {
        m_tile_description.effect.Apply (m_owner);
    }
}


