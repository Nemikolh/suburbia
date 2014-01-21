// --------------------------------------------------------------- //
// 
// Project : Suburbia
// Authors : Nemikolh, Pierre mourlanne
// All Wrongs Reserved.
// --------------------------------------------------------------- //
using UnityEngine;
using System.Collections.Generic;

public class PlayerBurrough : MonoBehaviour, HandlerClickOnTileFromREM, HandlerRemoveFreePositionOfPlayer, HandlerSendTileToPosition
{
    private static Object RESOURCE = Resources.Load ("Prefabs/PlayerBurrough");
    private Player m_player;
    private List<FreePositionView> m_free_positions;

    public static PlayerBurrough Instantiate (Player p_owner)
    {
        // Creation of the new instance.
        GameObject _new_instance = UnityEngine.Object.Instantiate (RESOURCE) as GameObject;
            
        // Get the script associated with the new tile.
        PlayerBurrough _this = _new_instance.AddComponent<PlayerBurrough> ();

        // Set the common properties
        _this.m_player = p_owner;
        _this.InitSetUpTiles ();
            
        return _this;
    }
    
    // Use this for initialization
    private void InitSetUpTiles ()
    {
        Suburbia.Bus.AddHandler (EventClickOnTileFromREM.TYPE, this);
        Suburbia.Bus.AddHandler (EventRemoveFreePositionOfPlayer.TYPE, this);
        Suburbia.Bus.AddHandler (EventSendTileToPosition.TYPE, this);

        foreach (TileInstance tile in m_player.tiles) {
            TileView.InstantiateWithParent (tile, this.transform);
        }

        m_free_positions = new List<FreePositionView> ();
    }
    
    public void HandleClickOnTileFromREM (EventClickOnTileFromREM p_event)
    {
        if (m_free_positions.Count > 0) {
            return;
        }

        if (m_player == p_event.current) {
            foreach (TilePosition pos in Suburbia.Manager.GetFreePositionsForPlayer (m_player)) {
                m_free_positions.Add (FreePositionView.InstantiateWithParent (pos, this.transform, this.m_player));
            }
        }
    }

    public void HandleRemoveFreePositionOfPlayer (EventRemoveFreePositionOfPlayer p_event)
    {
        if (p_event.player == m_player) {
            foreach (FreePositionView free_position in m_free_positions) {
                Destroy (free_position.gameObject);
            }
            m_free_positions.Clear ();
        }
    }

    public void HandleSendTileToPosition (EventSendTileToPosition p_event)
    {
        // TODO : Smooth transition
        if(p_event.player == m_player)
        {
            TileView.InstantiateWithParent(Suburbia.Market[p_event.index], this.transform);
        }
    }
}
