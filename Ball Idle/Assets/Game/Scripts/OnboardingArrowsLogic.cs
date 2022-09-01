using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnboardingArrowsLogic : MonoBehaviour
{

    public MeshRenderer normalArrow;
    public IndicatorLogic indicatorScript;

    void OnDisable()
    {
        normalArrow.gameObject.SetActive(true);
        indicatorScript.SetMeshRenderer(normalArrow);
        Destroy(gameObject);
    }
}
