// --------------------------------------------------------------- //
// 
// Project : Suburbia
// Author  : Nemikolh
// All Wrongs Reserved.
// --------------------------------------------------------------- //
using System;
using UnityEngine;

public enum TranslationType
{
    KEEP_ON_DESTINATION = 1,
    DESTROY_ON_DESTINATION = 2
}

public class SmoothTranslation : ImprovedBehavior<SmoothTranslation, TranslationType>
{
    private double LAMBDA = 1.5;
    private double FACTOR;
    private Vector3 m_destination;
    private Vector3 m_origin;
    protected bool m_is_transition_started;
    private TranslationType m_transition_type;
    private float m_t;

    public void InitWith (Vector3 p_destination)
    {
        FACTOR = (1.0 - Math.Exp (- 5 * LAMBDA * LAMBDA)) / (1.0 - Math.Exp (- 0.25 * LAMBDA * LAMBDA));
        m_destination = p_destination;
        m_origin = transform.position;
        m_t = 0.0f;
        m_is_transition_started = false;
    }

    public void StartTransition ()
    {
        m_is_transition_started = true;
        LAMBDA = 5;
    }

    protected void Update ()
    {
        if (m_is_transition_started && 
            ((transform.position - m_destination).magnitude < m_destination.magnitude * 0.01f)) {
            //m_t = (float)(LAMBDA * 0.25);
            transform.position = m_destination;
            if (m_transition_type == TranslationType.DESTROY_ON_DESTINATION)
                Destroy (this);
        } else {

            if (m_is_transition_started) {
                m_t += Time.smoothDeltaTime;
            } else {
                m_t -= Time.smoothDeltaTime;
            }

            if (m_t < 0) {
                m_t = 0;
            }

            Vector3 delta = (m_destination - m_origin) * (float)FACTOR;
            this.transform.position = ((float)(1.0 - Math.Exp (- m_t * LAMBDA))) * delta + m_origin;
        }
    }

    public override void Construct (TranslationType p_type)
    {
        m_transition_type = p_type;
    }

    public static void Translate (GameObject p_object, Vector3 p_destination, float p_delay, TranslationType p_type)
    {
        SmoothTranslation trans = SmoothTranslation.Create (p_object, p_type);
        trans.InitWith (p_destination);
        trans.Delay (trans.StartTransition, p_delay);
    }
}


