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
    private Tile m_tile_description;
    private List<TriggerInstance> m_triggers;
    private TilePosition m_position;
    private Player m_owner;
    private bool m_is_lake;

    public TileInstance (Tile p_tile)
    {
        m_is_lake = false;
        m_tile_description = p_tile;
        m_triggers = new List<TriggerInstance> ();
        m_owner = null;
        this.UpdateTriggers ();
    }

    public void TransformIntoLake ()
    {
        m_is_lake = true;
        this.UpdateTriggers ();
    }

    public void SwitchWithLake ()
    {
        m_is_lake = !m_is_lake;
        this.UpdateTriggers ();
    }

    private void UpdateTriggers ()
    {
        m_triggers.Clear ();
        if (m_is_lake) {
            foreach (Trigger trigger in Tile.GetLake().triggers) {
                m_triggers.Add (new TriggerInstance (trigger, this));
            }
        } else {
            foreach (Trigger trigger in m_tile_description.triggers) {
                m_triggers.Add (new TriggerInstance (trigger, this));
            }
        }
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
        if (m_is_lake)
            return Tile.GetLake ().IsOfType (p_type);
        else
            return this.m_tile_description.IsOfType (p_type);
    }

    public Player owner {
        get {
            return this.m_owner;
        }

        set {
            this.m_owner = value;
            value.AddTileInstance (this);
        }
    }

    public void SetOwner (Player p_player)
    {
        // This method is only called by a Plaier who already "owns" this TileInstance
        this.m_owner = p_player;
    }

    public string name {
        get {
            if (this.m_is_lake)
                return Tile.GetLake ().name;
            else
                return this.m_tile_description.name;
        }
    }

    public ETileColor color {
        get {
            if (this.m_is_lake)
                return Tile.GetLake ().color;
            else
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
            if (m_is_lake)
                return Tile.GetLake ().types;
            else
                return this.m_tile_description.types;
        }
    }

    public Tile description {
        get {
            if (this.m_is_lake)
                return Tile.GetLake ();
            else
                return this.m_tile_description;

        }
    }

    public bool IsAdjacentTo (TileInstance p_other)
    {
        if (this.owner == p_other.owner)
            return this.position.IsAdjacentTo (p_other.position);
        return false;
    }

    public bool IsAdjacentToLake ()
    {
        List<TileInstance> adjacent_instances = GetAdjacentInstances ();

        foreach (TileInstance instance in adjacent_instances) {
            if (instance.description != null && instance.description.IsOfType (new TileType (ETileColor.LAKE)))
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
        if (m_is_lake)
            return;
        if (m_tile_description.immediate_effect == null)
            return;
        m_tile_description.immediate_effect.Apply (m_owner, 1);
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
            hash = hash * 23 + position.GetHashCode ();
            hash = hash * 23 + owner.GetHashCode ();
            return hash;
        }
    }
}


