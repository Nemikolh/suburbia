// --------------------------------------------------------------- //
//
// Project : Suburbia
// Author  : Nemikolh
// All Wrongs Reserved.
// --------------------------------------------------------------- //
using System;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

public class Trigger
{
    private readonly TileType m_type;
    private readonly ETileScope m_scope;
    private readonly ETileWhen m_when;
    private readonly Effect m_effect;

    public static Trigger LoadFromJson (JSONClass p_json)
    {
        try {
            TileType tile_type = TileType.LoadFromValue (Util.getValue<string> (p_json, "type"));
            ETileScope scope = Util.parseEnum<ETileScope> (Util.getValue<string> (p_json, "scope"));
            ETileWhen when = Util.parseEnum<ETileWhen> (Util.getValue<string> (p_json, "when"));
            Effect effect = new Effect (Util.parseEnum<ETileResource> (p_json ["effect"] ["resource"].Value),
                                        p_json ["effect"] ["value"].AsInt);

            return new Trigger (scope, when, tile_type, effect);
        } catch (ArgumentException) {
            Debug.LogError ("Error while trying to load trigger");
            return null;
        }
    }

    private Trigger (ETileScope p_scope, ETileWhen p_when, TileType p_type, Effect p_effect)
    {
        m_type = p_type;
        m_scope = p_scope;
        m_when = p_when;
        m_effect = p_effect;
    }

    public ETileScope scope {
        get {
            return this.m_scope;
        }
    }

    public TileType type {
        get {
            return this.m_type;
        }
    }

    public ETileWhen when {
        get {
            return this.m_when;
        }
    }

    public Effect effect {
        get {
            return this.m_effect;
        }
    }

    public void Apply (Player p_owner, bool p_adjacent)
    {
        // TODO ADJACENT_TO_OWN_LAKE
        if (m_scope == ETileScope.ADJACENT && !p_adjacent)
            return;

        m_effect.Apply (p_owner);
    }

    public override bool Equals (System.Object p_obj)
    {
        if (p_obj == null) {
            return false;
        }

        Trigger trigger = p_obj as Trigger;
        if ((System.Object)trigger == null) {
            return false;
        }

        return (this.effect.Equals(trigger.effect) && this.scope.Equals(trigger.scope)
                && this.type.Equals (trigger.type) && this.when.Equals (trigger.when));
    }

    public override int GetHashCode ()
    {
        unchecked {
            int hash = 17;
            hash = hash * 23 + this.effect.GetHashCode ();
            hash = hash * 23 + this.scope.GetHashCode ();
            hash = hash * 23 + this.type.GetHashCode ();
            hash = hash * 23 + this.when.GetHashCode ();
            return hash;
        }
    }
}


