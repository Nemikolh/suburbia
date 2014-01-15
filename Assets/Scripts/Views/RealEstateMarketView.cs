using UnityEngine;
using System.Collections;

public class RealEstateMarketView : MonoBehaviour
{

    private RealEstateMarket m_market;

    public RealEstateMarketView ()
    {
        m_market = null;
    }

    // Use this for initialization
    void Start ()
    {
        m_market = Suburbia.Market;
    }
    
    // Update is called once per frame
    void Update ()
    {
    
    }
}
