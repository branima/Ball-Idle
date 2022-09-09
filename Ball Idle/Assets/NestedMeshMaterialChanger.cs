using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestedMeshMaterialChanger : MonoBehaviour
{

    public Material material;

    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<Renderer>().material = material;
    }
}
