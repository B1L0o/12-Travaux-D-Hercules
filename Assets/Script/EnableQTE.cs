using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableQTE : MonoBehaviour
{
    public GameObject QTE;

    private void Start()
    {
        StartCoroutine(EnableQTESystem());
    }

    IEnumerator EnableQTESystem()
    {
        yield return new WaitForSeconds(4);
        QTE.SetActive(true);
    }
}
