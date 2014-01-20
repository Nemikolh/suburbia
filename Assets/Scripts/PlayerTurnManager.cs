// --------------------------------------------------------------- //
// 
// Project : Suburbia
// Authors : Nemikolh, Pierre mourlanne
// All Wrongs Reserved.
// --------------------------------------------------------------- //
using System;

public sealed class PlayerTurnManager : HandlerClickOnTileFromREM, HandlerClickOnFreePosition, HandlerEndOfTurn
{
    private Player m_active_player;
    private TileInstance m_current_tile_choosen;

    public PlayerTurnManager ()
    {
        Suburbia.Bus.AddHandler(EventClickOnFreePosition.TYPE, this);
        Suburbia.Bus.AddHandler(EventClickOnTileFromREM.TYPE, this);
        Suburbia.Bus.AddHandler(EventEndOfTurn.TYPE, this);

        m_active_player = Suburbia.ActivePlayer;
    }

    public void HandleClickOnTileFromREM(EventClickOnTileFromREM p_event)
    {
        // TODO check that the current guy has enough money
        if(m_active_player != null)
        {
            m_current_tile_choosen = p_event.tile;
        }
    }

    public void HandleEndOfTurn(EventEndOfTurn p_event)
    {
        // The player has end his turned let get the next one.
        m_active_player = Suburbia.ActivePlayer;
    }

    public void HandleClickOnFreePosition(EventClickOnFreePosition p_event)
    {
        // The tile has been placed, we emit the tile played event.
        if(m_active_player != null && m_current_tile_choosen != null)
        {
            Suburbia.Bus.FireEvent(new EventTilePlayed(this.m_current_tile_choosen));
            m_current_tile_choosen = null;
        }
    }
}


