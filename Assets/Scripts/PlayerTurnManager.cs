// --------------------------------------------------------------- //
//
// Project : Suburbia
// Authors : Nemikolh, Pierre mourlanne
// All Wrongs Reserved.
// --------------------------------------------------------------- //
using System;

public sealed class PlayerTurnManager : HandlerClickOnTileFromREM, HandlerClickOnFreePosition
{
    private TileInstance m_current_tile_chosen;
    private int m_index_in_REM;

    public PlayerTurnManager ()
    {
        Suburbia.Bus.AddHandler (EventClickOnFreePosition.TYPE, this);
        Suburbia.Bus.AddHandler (EventClickOnTileFromREM.TYPE, this);
    }

    public void HandleClickOnTileFromREM (EventClickOnTileFromREM p_event)
    {
        // TODO check that the current guy has enough money
        m_current_tile_chosen = p_event.tile;
        m_index_in_REM = p_event.index_in_REM;
    }

    public void HandleClickOnFreePosition (EventClickOnFreePosition p_event)
    {
        // The tile has been placed, we emit the tile played event.
        if (m_current_tile_chosen != null) {

            // We place the new tile on the player ground :
            m_current_tile_chosen.position = p_event.position;
            Suburbia.ActivePlayer.AddTileInstance (m_current_tile_chosen);
            Suburbia.Bus.FireEvent (new EventSendTileToPosition (Suburbia.ActivePlayer, p_event.position, m_index_in_REM));

            // Transmit sub events.
            Suburbia.Bus.FireEvent (new EventRemoveFreePositionOfPlayer (Suburbia.ActivePlayer));
            Suburbia.Bus.FireEvent (new EventTilePlayed (this.m_current_tile_chosen, 
                                                         Suburbia.Market.PriceOverheadForTileNumber (this.m_index_in_REM)));
            // We remove the tile in the REM.
            Suburbia.Market.RemoveTile(this.m_index_in_REM);
            m_current_tile_chosen = null;
        }
    }


}


