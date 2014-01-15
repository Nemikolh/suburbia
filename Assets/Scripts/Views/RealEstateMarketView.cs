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

    public RealEstateMarketView ()
    {
        m_market = null;
    }

    // Use this for initialization
    void Start ()
    {
        Debug.Log("Loading Real Estate Market...");
        m_market = Suburbia.Market;
        m_tiles = new List<TileView> ();

        Vector3 screenPoint = new Vector3 (50, 50, 20);

        foreach (var tile in m_market.tiles) {

            screenPoint.x += 50;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint (screenPoint);

            m_tiles.Add (TileView.Instantiate (tile, worldPos));
        }
    }
    
    // Update is called once per frame
    void Update ()
    {

    }
}
