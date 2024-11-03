using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public Animator camAnim;

    public void camShake()
    {
        camAnim.SetTrigger("Shake");
    }

    public void bossWalkCamShake()
    {
        camAnim.SetTrigger("ShakeBossWalk");
    }

    public void bossScreamCamShake()
    {
        camAnim.SetTrigger("ShakeBossScream");
    }
}
