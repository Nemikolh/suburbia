// --------------------------------------------------------------- //
//
// Project : Suburbia
// Authors : Nemikolh, Pierre mourlanne
// All Wrongs Reserved.
// --------------------------------------------------------------- //
using System;
using UnityEngine;

public sealed class Suburbia : HandlerEndOfTurn, HandlerLastTurn, HandlerEndOfGame
{
    private Suburbia ()
    {
        m_eventBus = new EventBus ();

        m_eventBus.AddHandler(EventEndOfTurn.TYPE, this);
        m_eventBus.AddHandler(EventLastTurn.TYPE, this);
        m_eventBus.AddHandler(EventEndOfGame.TYPE, this);
    }

    private EventBus m_eventBus;
    private RealEstateMarket m_market;
    private TileManager m_game_manager;
    private PlayerTurnManager m_turn_manager;
    private int m_remaining_turns;
    private int m_current_player;

    public void StartGame (int p_nb_players)
    {
        m_market = new RealEstateMarket (p_nb_players);
        TileManager.LoadSetUpTiles (m_market.Stacks.LoadedTiles);
        TileView.InitProperties ();
        m_game_manager = new TileManager (p_nb_players);
        m_current_player = 0;
        m_remaining_turns = -1;
        m_turn_manager = new PlayerTurnManager();
        m_eventBus.FireEvent(new EventGameStarted());
    }

    public void ClearGame()
    {
        m_market = null;
        m_game_manager = null;
        m_remaining_turns = -1;
    }

    public void HandleEndOfTurn (EventEndOfTurn p_event)
    {
        if (m_game_manager == null)
            return;
        ActivePlayer.CleanUp();
        m_current_player = (m_current_player + 1) % m_game_manager.players.Count;
        this.m_remaining_turns -= 1;
        if (m_remaining_turns == 0)
            Suburbia.Bus.FireEvent(new EventEndOfGame());
    }

    public void HandleOneMoreTurn (EventLastTurn p_event)
    {
        // We get the index of the player who just drew the tile
        int index = m_game_manager.players.IndexOf(p_event.player);
        if (index == -1)
            Debug.Log("Could not find player!");
        int nb_turns = m_game_manager.players.Count - index;  // To finish this turn
        nb_turns += m_game_manager.players.Count;  // To play one more turn
        m_remaining_turns = nb_turns;
    }

    public void HandleEndOfGame (EventEndOfGame p_event)
    {
        //TODO
    }


    public static Suburbia App {
        get {
            return m_instance;
        }
    }

    public static EventBus Bus {
        get {
            return m_instance.m_eventBus;
        }
    }

    public static RealEstateMarket Market {
        get {
            return m_instance.m_market;
        }
    }

    public static TileManager Manager {
        get {
            return m_instance.m_game_manager;
        }
    }

    public static int nb_turns_remaining {
        get {
            return m_instance.m_remaining_turns;
        }
    }

    public static Player ActivePlayer {
        get {
            return m_instance.m_game_manager[m_instance.m_current_player];
        }
    }

    private static readonly Suburbia m_instance = new Suburbia ();
}


