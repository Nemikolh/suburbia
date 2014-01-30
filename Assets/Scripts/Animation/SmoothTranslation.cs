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
    private static float TAN_ORIGIN = 3;
    private static float FACTOR = 1;
    private static float DELTA_T = 0.015f;
    private Vector3 m_translation;
    private bool m_is_transition_started;
    private bool m_is_arrived;
    private TranslationType m_transition_type;
    private float m_t;

    public void InitWith (Vector3 p_translation)
    {
        m_translation = p_translation;
        m_t = 0.0f;
        m_is_transition_started = false;
        m_is_arrived = true;
    }

    public void StartTransition ()
    {
        m_is_transition_started = true;
        m_is_arrived = false;
    }

    protected void SwitchTransition ()
    {
        m_is_arrived = false;
        m_is_transition_started = !m_is_transition_started;
    }

    protected void Update ()
    {
        if (!m_is_arrived && m_is_transition_started)
            UpdateToDestination ();
        if (!m_is_arrived && !m_is_transition_started)
            UpdateToOrigin ();
        if (m_is_arrived && m_is_transition_started &&
            m_transition_type == TranslationType.DESTROY_ON_DESTINATION)
            Destroy (this);
    }

    private void UpdateToDestination ()
    {
        m_t += DELTA_T;//Time.smoothDeltaTime;
        float delta_t = DELTA_T;//.smoothDeltaTime;

        if (m_t > FACTOR) {
            delta_t -= m_t - FACTOR;
            m_t = FACTOR;
            m_is_arrived = true;
        }

        this.transform.localPosition += MathCore.DerivSpline (m_t / FACTOR, TAN_ORIGIN, 0.7f) * delta_t * m_translation;
    }

    private void UpdateToOrigin ()
    {
        m_t -= DELTA_T;//Time.smoothDeltaTime;
        float delta_t = DELTA_T;//Time.smoothDeltaTime;

        if (m_t < 0) {
            delta_t += m_t;
            m_t = 0;
            m_is_arrived = true;
        }
        
        this.transform.localPosition -= MathCore.DerivSpline (m_t / FACTOR, TAN_ORIGIN, 0.7f) * delta_t * m_translation;
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


