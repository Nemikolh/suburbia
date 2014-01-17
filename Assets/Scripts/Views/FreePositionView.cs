// --------------------------------------------------------------- //
// 
// Project : Suburbia
// Authors : Nemikolh, Pierre mourlanne
// All Wrongs Reserved.
// --------------------------------------------------------------- //
using System;
using UnityEngine;

public class FreePositionView : MonoBehaviour
{
    private static UnityEngine.Object RESOURCE = Resources.Load ("Prefabs/FreePosition");

    private TilePosition m_position;
    private Player m_player;

    public static FreePositionView InstantiateWithParent (TilePosition p_position, Transform p_parent, Player p_owner)
    {
        try {
            // Creation of the new instance.
            GameObject _new_instance = UnityEngine.Object.Instantiate (RESOURCE) as GameObject;
            _new_instance.transform.parent = p_parent;
            
            // Get the script associated with the new tile.
            FreePositionView _this = _new_instance.AddComponent<FreePositionView> ();
            
            // Set the common properties
            _this.m_position = p_position;
            _this.m_player = p_owner;
            
            return _this;
            
        } catch (Exception) {
            Debug.LogError ("Error while instantiating !");
            return null;
        }
    }

    void OnMouseEnter()
    {
    }

    void OnMouseExit()
    {
    }

    void OnMouseDown()
    {
        Debug.Log ("Click on the tile position !");
    }
}


