// --------------------------------------------------------------- //
// 
// Project : Suburbia
// Author  : Nemikolh
// All Wrongs Reserved.
// --------------------------------------------------------------- //
using System;
using UnityEngine;

public class SmoothTranslation : MonoBehaviour
{
    private static double LAMBDA = 1.5;
    private static double FACTOR = (1.0 - Math.Exp( - 5 * LAMBDA * LAMBDA )) / (1.0 - Math.Exp( - 0.25 * LAMBDA * LAMBDA));
    private Vector3 m_destination;
    private Vector3 m_origin;
    protected bool m_is_transition_started;
    private float m_t;

    public void InitWith (Vector3 p_destination)
    {
        m_destination = p_destination;
        m_origin = transform.position;
        m_t = 0.0f;
        m_is_transition_started = false;
    }

    protected void Update ()
    {
        if (m_is_transition_started) {
            m_t += Time.smoothDeltaTime;
        } else {
            m_t -= Time.smoothDeltaTime;
        }

        if (m_t < 0) {
            m_t = 0;
        } else if (m_t > LAMBDA * 0.25) {
            m_t = (float) (LAMBDA * 0.25);
            transform.position = m_destination;
        } else {
            Vector3 delta  = (m_destination - m_origin) * (float) FACTOR;
            this.transform.position = ((float)(1.0 - Math.Exp (- m_t * LAMBDA))) * delta + m_origin;

        }
    }

}


