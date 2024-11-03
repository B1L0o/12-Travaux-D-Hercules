using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFireballExplosion : MonoBehaviour
{
    // Start is called before the first frame update
    private void Update()
    {
        Destroy(gameObject,0.5f);
    }
}
