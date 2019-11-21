using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBars : MonoBehaviour
{
    [SerializeField]
    private Image _content;

    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }

    public void Bar(float value, float inMin, float inMax, float outMin, float outMax)
    {
        _content.fillAmount = Map(value, inMin, inMax, outMin, outMax);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (_masked)
        {
            if (_content.fillAmount <= 0)
                _mask.gameObject.SetActive(false);
            else
                _mask.gameObject.SetActive(true);
        }
        */
    }
}
