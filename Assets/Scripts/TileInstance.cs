// --------------------------------------------------------------- //
//
// Project : Suburbia
// Author  : Nemikolh
// All Wrongs Reserved.
// --------------------------------------------------------------- //
using System;
using System.Linq;
using System.Collections.Generic;

public class TileInstance : System.Object
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

    // public for test purposes
    public TileInstance ()
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
            // We add the tile instance to the player's tile instances
            value.AddTileInstance (this);
        }
    }

    public string name {
        get {
            return this.m_tile_description.name;
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
            value.parent = this;
        }
    }

    public List<TileType> types {
        get {
            return this.m_tile_description.types;
        }
    }

    public Tile description {
        get {
            return this.m_tile_description;
        }
    }

    public bool IsAdjacentTo (TileInstance p_other)
    {
        if (this.owner == p_other.owner)
            return this.position.IsAdjacentTo (p_other.position);
        return false;
    }

    public bool IsAdjacentToLake()
    {
        List<TileInstance> adjacent_instances = GetAdjacentInstances();

        foreach (TileInstance instance in adjacent_instances) {
            if (instance.description != null && instance.description.IsOfType(new TileType(ETileColor.LAKE)))
                return true;
        }
        return false;
    }

    public List<TileInstance> GetAdjacentInstances ()
    {
        // We get the adjacent positions
        List<TilePosition> adjacent_pos = this.m_position.GetAdjacentPositions ();

        // We get the tile instances from the player
        List<TileInstance> adjacent_instances = this.owner.tiles;

        // And only keep those corresponding to the positions
        adjacent_instances = (from instance in adjacent_instances where adjacent_pos.Contains (instance.position) select instance).ToList ();

        return adjacent_instances;
    }

    public List<TilePosition> GetAdjacentFreePositions ()
    {
        // We get the adjacent positions
        List<TilePosition> adjacent_pos = this.m_position.GetAdjacentPositions ();

        // We get the occupied adjacent positions
        List<TilePosition> occupied_pos = (from instance in GetAdjacentInstances () select instance.position).ToList ();

        // And do the difference
        List<TilePosition> free_pos = (from pos in adjacent_pos where !occupied_pos.Contains (pos) select pos).ToList ();

        return free_pos;
    }

    public void ApplyImmediateEffect ()
    {
        m_tile_description.effect.Apply (m_owner);
    }

    public override bool Equals (System.Object p_obj)
    {
        if (p_obj == null) {
            return false;
        }

        TileInstance p_instance = p_obj as TileInstance;
        if ((System.Object)p_instance == null) {
            return false;
        }

        return (this.position.Equals (p_instance.position)) && (this.owner == p_instance.owner);
    }

    public override int GetHashCode ()
    {
        //https://stackoverflow.com/questions/5221396/what-is-an-appropriate-gethashcode-algorithm-for-a-2d-point-struct-avoiding
        unchecked {
            int hash = 17;
            hash = hash * 23 + position.GetHashCode();
            hash = hash * 23 + owner.GetHashCode ();
            return hash;
        }
    }
}


