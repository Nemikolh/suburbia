// --------------------------------------------------------------- //
//
// Project : Suburbia
// Authors : Nemikolh, Pierre mourlanne
// All Wrongs Reserved.
// --------------------------------------------------------------- //
using System;

public sealed class PlayerTurnManager : HandlerClickOnTileFromREM, HandlerClickOnFreePosition
{
    private TileInstance m_current_tile_choosen;

    public PlayerTurnManager ()
    {
        Suburbia.Bus.AddHandler (EventClickOnFreePosition.TYPE, this);
        Suburbia.Bus.AddHandler (EventClickOnTileFromREM.TYPE, this);
    }

    public void HandleClickOnTileFromREM (EventClickOnTileFromREM p_event)
    {
        // TODO check that the current guy has enough money
        m_current_tile_choosen = p_event.tile;
    }

    public void HandleClickOnFreePosition (EventClickOnFreePosition p_event)
    {
        // The tile has been placed, we emit the tile played event.
        if (m_current_tile_choosen != null) {

            // We place the new tile on the player ground :
            m_current_tile_choosen.position = p_event.position;
            Suburbia.ActivePlayer.AddTileInstance (m_current_tile_choosen);
            Suburbia.Bus.FireEvent (new EventSendTileToPosition (Suburbia.ActivePlayer, p_event.position));

            // Transmit sub events.
            Suburbia.Bus.FireEvent (new EventRemoveFreePositionOfPlayer (Suburbia.ActivePlayer));
            Suburbia.Bus.FireEvent (new EventTilePlayed (this.m_current_tile_choosen));

            m_current_tile_choosen = null;
        }
    }


}


