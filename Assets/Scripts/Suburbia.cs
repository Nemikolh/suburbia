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

public sealed class Suburbia
{

    private Suburbia ()
    {
        m_eventBus = new EventBus ();
    }

    private EventBus m_eventBus;
    private RealEstateMarket m_market;

    public void StartGame (int p_nb_players)
    {
        m_market = new RealEstateMarket (p_nb_players);
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

    private static readonly Suburbia m_instance = new Suburbia ();
}

