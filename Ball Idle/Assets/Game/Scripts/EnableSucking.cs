using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableSucking : MonoBehaviour
{

    public GameObject suckingEffect;

    public void EnableSuckingEffect() => suckingEffect.SetActive(true);
}
