 
using UnityEngine;

public class camFollow : MonoBehaviour
{

    public GameObject player;
    public float timeOffset;
    public Vector3 posOffset;


    private Vector3 velocity;
    void Update()
    {
        Debug.Log("AAAAAA");
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + posOffset, ref velocity,
            timeOffset);
    }
}

