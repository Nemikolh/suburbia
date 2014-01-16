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

//    public float Scale {
//        get {
//            return m_scale;
//        }
//        set {
//            m_scale = value > 0.0f ? value : 1.0f;
//        }
//    }

    public float m_scale;
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
        Camera _camera = GameObject.FindWithTag("RealEstateCamera").camera;

        Vector3 screenPoint = new Vector3 (50, 50, _camera.nearClipPlane + 5);

        foreach (var tile in m_market.tiles) {

            screenPoint.x += 85;
            Vector3 worldPos = _camera.ScreenToWorldPoint (screenPoint);

            m_tiles.Add (TileView.Instantiate (tile, worldPos, m_scale));
        }
    }
    
    // Update is called once per frame
    void Update ()
    {

    }
}
