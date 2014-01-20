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
    }

    private EventBus m_eventBus;
    private RealEstateMarket m_market;
    private TileManager m_game_manager;

    public void StartGame (int p_nb_players)
    {
        m_market = new RealEstateMarket (p_nb_players);
        TileManager.LoadSetupTiles (m_market.Stacks.LoadedTiles);
        TileView.InitProperties ();
        m_game_manager = new TileManager (p_nb_players);
    }

    public void HandleEndOfTurn (EventEndOfTurn p_event)
    {
        //TODO
    }

    public void HandleOneMoreTurn (EventLastTurn p_event)
    {
        //TODO
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

    private static readonly Suburbia m_instance = new Suburbia ();
}


