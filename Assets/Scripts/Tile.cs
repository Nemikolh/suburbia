using System;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

public class Tile
{
    private readonly string m_name;
    private readonly ETileColor m_color;
    private readonly ETileIcon m_icon;
    private readonly int m_price;
    private readonly int m_number;
    private readonly ETileLetter m_letter;
    private readonly Effect m_immediate_effect;
    private readonly List<Trigger> m_triggers;

    public static Tile GetLake ()
    {
        string lake_description = "{\"name\": \"Lake\", \"triggers\": [{\"scope\": \"ADJACENT\", \"when\": \"ALWAYS\", \"effect\": {\"resource\": \"MONEY\", \"value\": 2}, \"type\": \"YELLOW\"}, {\"scope\": \"ADJACENT\", \"when\": \"ALWAYS\", \"effect\": {\"resource\": \"MONEY\", \"value\": 2}, \"type\": \"GREY\"}, {\"scope\": \"ADJACENT\", \"when\": \"ALWAYS\", \"effect\": {\"resource\": \"MONEY\", \"value\": 2}, \"type\": \"GREEN\"}, {\"scope\": \"ADJACENT\", \"when\": \"ALWAYS\", \"effect\": {\"resource\": \"MONEY\", \"value\": 2}, \"type\": \"BLUE\"}], \"color\": \"LAKE\", \"price\": 0, \"number\": 0, \"immediate\": \"NONE\", \"letter\": \"BASE\", \"icon\": \"NONE\"}";
        return Tile.LoadFromJson(JSON.Parse(lake_description) as JSONClass);
    }

    public static Tile LoadFromJson (JSONClass p_json)
    {
        try {
            string name = Util.getValue<string> (p_json, "name");
            ETileColor color = Util.parseEnum<ETileColor> (Util.getValue<string> (p_json, "color"));
            ETileIcon icon = Util.parseEnum<ETileIcon> (Util.getValue<string> (p_json, "icon"));
            int price = Util.getValue<int> (p_json, "price");
            ETileLetter letter = Util.parseEnum<ETileLetter> (Util.getValue<string> (p_json, "letter"));
            int number = Util.getValue<int> (p_json, "number");

            List<Trigger> triggers = new List<Trigger> ();

            if (p_json ["triggers"] != null) {
                foreach (JSONNode trigger in p_json["triggers"].AsArray) {
                    triggers.Add (Trigger.LoadFromJson (trigger.AsObject));
                }
            }

            Effect immediate_effect = null;
            if(p_json["immediate"] != null && p_json["immediate"].Value != "NONE")
            {
                immediate_effect = new Effect(Util.parseEnum<ETileResource>(p_json["immediate"]["resource"].Value),
                                              p_json["immediate"]["value"].AsInt);
            }

            return new Tile (name, color, icon, price, letter, number, triggers, immediate_effect);

        } catch (ArgumentException) {
            Debug.LogError ("Error while loading Tile from Json !");
            return null;
        }
    }

    private Tile (string p_name, ETileColor p_color, ETileIcon p_icon, int p_price, ETileLetter p_letter,
                  int p_number, List<Trigger> p_triggers, Effect p_immediate_effect)
    {
        m_name = p_name;
        m_color = p_color;
        m_icon = p_icon;
        m_price = p_price;
        m_letter = p_letter;
        m_number = p_number;
        m_triggers = p_triggers;
        m_immediate_effect = p_immediate_effect;
    }

    public bool IsOfType (TileType p_type)
    {
        if (p_type.IsColor ()) {
            return m_color == p_type.color;
        }

        if (p_type.IsIcon ()) {
            return m_icon == p_type.icon;
        }

        Debug.LogError ("TileType is not an icon or a color !");
        return false;
    }

    public List<Trigger> triggers {
        get {
            return this.m_triggers;
        }
    }

    public string name {
        get {
            return m_name;
        }
    }

    public ETileColor color {
        get {
            return m_color;
        }
    }

    public ETileIcon icon {
        get {
            return m_icon;
        }
    }

    public int price {
        get {
            return m_price;
        }
    }

    public int number {
        get {
            return this.m_number;
        }
    }

    public ETileLetter letter {
        get {
            return m_letter;
        }
    }

    public Effect immediate_effect {
        get {
            return this.m_immediate_effect;
        }
    }

    public List<TileType> types {
        get {
            List<TileType> list = new List<TileType> ();
            list.Add (new TileType (this.m_color));
            if (m_icon != ETileIcon.NONE)
                list.Add (new TileType (this.m_icon));
            return list;
        }
    }
}
