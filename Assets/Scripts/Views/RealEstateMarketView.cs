// --------------------------------------------------------------- //
// 
// Project : Suburbia
// Author  : Nemikolh
// All Wrongs Reserved.
// --------------------------------------------------------------- //
using UnityEngine;
using System.Collections.Generic;

public class RealEstateMarketView : MonoBehaviour
{
    private RealEstateMarket m_market;
    private List<TileView> m_tiles;
    private int m_width_tiles;
    private int m_delta_tile;
    private Camera m_cam;

    public RealEstateMarketView ()
    {
        m_market = null;
    }

    private void SetPositionOfTiles ()
    {
        m_width_tiles = m_delta_tile * m_market.tiles.Count;
        Vector3 screenPoint = new Vector3 ((Screen.width - m_width_tiles) / 2, 70, m_cam.nearClipPlane + 5);
        
        foreach (var tile in m_market.tiles) {
            
            Vector3 worldPos = m_cam.ScreenToWorldPoint (screenPoint);
            m_tiles.Add (TileView.Instantiate (tile, worldPos, 1));

            screenPoint.x += m_delta_tile;
        }
    }

    // Use this for initialization
    void Start ()
    {
        Debug.Log ("Loading Real Estate Market...");
        m_market = Suburbia.Market;
        m_tiles = new List<TileView> ();
        m_cam = GameObject.FindWithTag ("RealEstateCamera").camera;

        if(m_market.tiles.Count > 0)
        {
            Vector3 value = m_cam.WorldToScreenPoint(new Vector3(2,0));
            m_delta_tile = (int) value.x - Screen.width / 2;
        }
        else
            m_delta_tile = 85;
        SetPositionOfTiles ();
    }
    
    // On GUI is called on every event linked to GUI plus user input.
    void OnGUI ()
    {
        int top = Screen.height - 30;
        int offset = (Screen.width - m_width_tiles) / 2 - m_delta_tile / 2;
        GUI.Box (new Rect (offset, top, m_width_tiles, 25), "");
        int index = 0;
        foreach (var tile in m_tiles) {

            GUI.Label (new Rect (offset + m_delta_tile / 2 -10, top, 100, 25), "$ " + m_market.PriceOverheadForTileNumber (index++));

            offset += m_delta_tile;
        }
    }
}
