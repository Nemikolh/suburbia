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
using System.IO;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SimpleJSON;


public sealed class TileManager : HandlerTilePlayed, HandlerRedLine
{
    public TileManager (int p_nb_players)
    {
        m_subscribers = new Dictionary<TileType, List<TriggerInstance>> ();
        InitPlayers (p_nb_players);

        // Listen on TilePlayed
        Suburbia.Bus.AddHandler(EventTilePlayed.TYPE, this);

        // Listen on RedLine
        Suburbia.Bus.AddHandler(EventRedLine.TYPE, this);
    }

    private List<Pair<Player, PlayerBurrough>> m_players;
    private Dictionary<TileType, List<TriggerInstance>> m_subscribers;
    private static List<SetUpTile> m_setup_tiles;

    // public for test purposes
    public class SetUpTile
    {
        public SetUpTile (Tile p_tile, int p_x, int p_y)
        {
            this.tile = p_tile;
            this.x = p_x;
            this.y = p_y;
        }

        public Tile tile {
            get;
            set;
        }

        public int x {
            get;
            set;
        }

        public int y {
            get;
            set;
        }
    }

    public static List<SetUpTile> GetSetUpTiles ()
    {
        return TileManager.m_setup_tiles;
    }

    public static void LoadSetUpTiles (List<Tile> p_all_possible_tiles)
    {
        m_setup_tiles = new List<SetUpTile> ();

        using (StreamReader reader = File.OpenText(@"Assets/Data/setup_tiles.json")) {
            JSONArray arr = JSON.Parse (reader.ReadToEnd ()) as JSONArray;

            foreach (JSONNode pos_tile in arr) {
                int x = pos_tile ["x"].AsInt;
                int y = pos_tile ["y"].AsInt;
                string name = pos_tile ["tile_name"].Value;
                m_setup_tiles.AddRange (from tile in p_all_possible_tiles where tile.name == name select new SetUpTile (tile, x, y));
            }
        }
    }

    private void InitPlayers (int p_nb_players)
    {
        m_players = new List<Pair<Player, PlayerBurrough>>();
        string name;

        for (int i = 0; i < p_nb_players; i++) {
            name = "Player " + i;
            Player player = new Player (name);
            this.AddPlayer (player);
            SetUpTilesForPlayer (player);
        }

        CreatePlayersBurroughs();
    }

    private void CreatePlayersBurroughs ()
    {
        int i = 0;
        foreach (Pair<Player, PlayerBurrough> player in m_players) {

            PlayerBurrough burrough = PlayerBurrough.Instantiate(player.Key);
            player.Value = burrough;
            GameObject player_place = burrough.gameObject;
            player_place.transform.position = new Vector3 (10.0f * (i / 2), 0, 0);
            player_place.transform.rotation = Quaternion.Euler (new Vector3 (0, i % 2 == 0 ? 0 : 180, 0));
            ++i;
        }
    }

    // public for test purposes
    public void SetUpTilesForPlayer (Player p_player)
    {
        if (TileManager.m_setup_tiles != null) {
            foreach (var setupTile in m_setup_tiles) {
                TileInstance tileInstance = new TileInstance (setupTile.tile);
                p_player.AddTileInstance (tileInstance);
                tileInstance.position = new TilePosition (setupTile.x, setupTile.y);

                // Quick hack so that the setup tiles don't cost any money
                Suburbia.Bus.FireEvent(new EventTilePlayed(tileInstance, -setupTile.tile.price));
            }
        }
    }

    public Player this [int index] {
        get {
            return this.players [index];
        }
    }

    public List<Player> players {
        get {
            return this.m_players.ConvertAll(new Converter<Pair<Player, PlayerBurrough>, Player>(elem => elem.Key));
        }
    }

    public Dictionary<TileType, List<TriggerInstance>> subscribers {
        get {
            return this.m_subscribers;
        }
    }

    public void HandleTilePlayed (EventTilePlayed p_event)
    {
        TileInstance p_new_tile = p_event.tile_played;

        if (!Manages(p_new_tile.owner))
            return;
        // The player pays the tile
        int full_price = p_new_tile.description.price + p_event.price_overhead;
        if (p_new_tile.owner.money < full_price)
        {
            // This should not happen
            Debug.Log("The player doesn't have enough money to play this tile!");
            return;
        }
        p_new_tile.owner.money -= full_price;

        // Immediate effect of the new tile
        HandleNewTileImmediateEffect(p_new_tile);
        // Conditional effect of the new tile
        HandleNewTileConditionalEffect(p_new_tile);
        // Conditional effect on the other tiles
        EmitNewTileEvent(p_new_tile);
        // We subscribe the new tile for later conditional effects
        AddSubscriber(p_new_tile);
        // End of turn.
        Suburbia.Bus.FireEvent(new EventEndOfTurn(p_new_tile.owner));
    }

    public void HandleRedLinePassed (EventRedLine p_event)
    {
        if (!Manages (p_event.player))
            return;

        if (p_event.nb_red_lines <= 0)
            return;

        List<TriggerInstance> all_triggers = new List<TriggerInstance> ();
        foreach (List<TriggerInstance> list in m_subscribers.Values) {
            all_triggers.AddRange(list);
        }

        List<TriggerInstance> triggers_red_line = (from trigger in all_triggers
                                                    where trigger.trigger.when == ETileWhen.AFTER_RED_LINE
                                                    select trigger).ToList();

        foreach (TriggerInstance trigger in triggers_red_line) {
            trigger.Apply (trigger.owner.owner, p_event.nb_red_lines);
        }

    }

    public List<TileInstance> GetAdjacent (TileInstance p_tile)
    {
        return p_tile.GetAdjacentInstances ();
    }

    public List<TileInstance> GetAllTiles ()
    {
        List<TileInstance> tiles = new List<TileInstance> ();
        foreach (var player in m_players) {
            tiles.AddRange (player.Key.tiles);
        }

        return tiles;
    }

    public List<TileInstance> GetTilesOfPlayer (Player p_player)
    {
        if (Manages (p_player))
            return p_player.tiles;
        return null;
    }

    public List<TileInstance> GetTilesOfOtherPlayers (Player p_player)
    {
        List<TileInstance> tiles = new List<TileInstance> ();
        foreach (var player in m_players) {
            if (player.Key != p_player)
                tiles.AddRange (player.Key.tiles);
        }

        return tiles;
    }

    public List<TileInstance> GetAdjacentToOwnLake (Player p_player)
    {
        if (!Manages (p_player))
            return null;

        List<TileInstance> tiles = new List<TileInstance> ();
        foreach (TileInstance tile in p_player.tiles) {
            if (tile.IsAdjacentToLake ())
                tiles.Add (tile);
        }

        return tiles;
    }

    public List<TilePosition> GetFreePositionsForPlayer (Player p_player)
    {
        if (!Manages (p_player))
            return null;

        HashSet<TilePosition> free_pos = new HashSet<TilePosition> ();

        foreach (TileInstance tile in p_player.tiles) {
            free_pos.UnionWith (tile.GetAdjacentFreePositions ());
        }

        return free_pos.ToList ();
    }

    public void HandleNewTileImmediateEffect (TileInstance p_new_tile)
    {
        p_new_tile.ApplyImmediateEffect ();
    }

    public void HandleNewTileConditionalEffect (TileInstance p_new_tile)
    {
        // Apply all the triggers of the new tile :

        foreach (TriggerInstance trigger in p_new_tile.triggers) {
            if (trigger.trigger.when == ETileWhen.AFTER_RED_LINE || trigger.trigger.when == ETileWhen.AFTER) {
                continue;
            }

            List<TileInstance> tile_instances = null;

            switch (trigger.trigger.scope) {
            case ETileScope.ADJACENT:

                tile_instances = GetAdjacent (p_new_tile);
                break;

            case ETileScope.OWN:

                tile_instances = GetTilesOfPlayer (p_new_tile.owner);
                break;

            case ETileScope.GLOBAL:

                tile_instances = GetAllTiles ();
                break;

            case ETileScope.OTHER:

                tile_instances = GetTilesOfOtherPlayers (p_new_tile.owner);
                break;

            case ETileScope.ADJACENT_TO_OWN_LAKE:

                tile_instances = GetAdjacentToOwnLake (p_new_tile.owner);
                break;

            case ETileScope.NONE:

                Debug.LogError ("Bug found ! No tile here with a when always and a scope equal to none...");
                continue;
            }

            if (tile_instances == null) {
                Debug.LogWarning ("Tile instances empty !");
                continue;
            }

            TileType trigger_on_tile_type = trigger.trigger.type;
            int number_of_tiles = 0;

            foreach (TileInstance other in tile_instances) {
                if (other.IsOfType (trigger_on_tile_type))
                    number_of_tiles += 1;
            }

            trigger.Apply (p_new_tile.owner, number_of_tiles);
        }
    }

    // public for test purposes
    public static List<TriggerInstance> SortSubscribedTriggers (List<TriggerInstance> p_subscribed_triggers, TileInstance p_new_tile)
    {
        p_subscribed_triggers.Sort (
            (p_trigger1, p_trigger2) =>
            - p_trigger1.owner.IsAdjacentTo (p_new_tile).CompareTo (p_trigger2.owner.IsAdjacentTo (p_new_tile))
        );

        return p_subscribed_triggers;
    }

    public void EmitNewTileEvent (TileInstance p_new_tile)
    {

        foreach (TileType type in p_new_tile.types) {
            List<TriggerInstance> subscribed_triggers = new List<TriggerInstance>();

            m_subscribers.TryGetValue (type, out subscribed_triggers);
            if (subscribed_triggers == null)
                continue;

            subscribed_triggers = TileManager.SortSubscribedTriggers(subscribed_triggers, p_new_tile);

            foreach (TriggerInstance trigger in subscribed_triggers) {
                trigger.Apply (p_new_tile);
            }
        }
    }

    public void AddSubscriber (TriggerInstance p_trigger)
    {
        TileType type_trigger = p_trigger.trigger.type;

        List<TriggerInstance> list;
        if (!m_subscribers.ContainsKey (type_trigger)) {

            list = new List<TriggerInstance> ();
            list.Add (p_trigger);

            m_subscribers.Add (type_trigger, list);

        } else {
            m_subscribers.TryGetValue (type_trigger, out list);
            list.Add (p_trigger);
        }

    }

    public void AddSubscriber (TileInstance p_tile)
    {
        foreach (TriggerInstance trigger in p_tile.triggers) {
            AddSubscriber (trigger);
        }

    }

    // For testing purposes
    public void RemoveAllSubscribers ()
    {
        m_subscribers = new Dictionary<TileType, List<TriggerInstance>>();
    }

    public void AddPlayer (Player p_player)
    {
        m_players.Add (new Pair<Player, PlayerBurrough>(p_player, null));
    }

    public void RemovePlayer (Player p_player)
    {
        m_players.RemoveAll (elem => elem.Key == p_player);
    }

    public bool Manages (Player p_player)
    {
        return m_players.Exists(elem => elem.Key == p_player);
    }
}


