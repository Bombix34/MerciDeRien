using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class DayLightCycle : MonoBehaviour
{
    [SerializeField]
    private PostProcessVolume m_postProcessVolume;

    [SerializeField]
    private Gradient m_colorOverLightAmount;

    [SerializeField, Tooltip("24H duration in seconds"), UnityEngine.Min(10.0F)]
    private float m_cycleDuration = 100;

    [SerializeField, Range(0.0F, 1.0F)]
    private float m_currentTime = 0.5F;


    private ColorGrading m_colorGradingModule;

    void Start()
    {
        m_postProcessVolume.profile.TryGetSettings<ColorGrading>(out m_colorGradingModule);

        if(m_colorGradingModule == null)
        {
            Debug.LogWarning("Missing post processing volume reference!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_currentTime += Time.deltaTime / m_cycleDuration;
        m_currentTime = Mathf.Repeat(m_currentTime, 1.0F);

        // Generate a light value from tiime (0 Night 1 Day)
        float lightValue = Mathf.PingPong(m_currentTime, 0.5F) * 2.0F;
      
        if (m_colorGradingModule != null)
        {
            // Change post processing color
            m_colorGradingModule.colorFilter.Override(m_colorOverLightAmount.Evaluate(lightValue));
        }
    }

    void OnDisable()
    {
        // Reset post processing profile
        if (m_colorGradingModule != null)
        {
            m_colorGradingModule.colorFilter.overrideState = false;
            m_colorGradingModule.colorFilter.value = Color.white;
        }
    }
}
