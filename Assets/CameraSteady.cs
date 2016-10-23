using UnityEngine;
using System.Collections;

public class CameraSteady : MonoBehaviour
{

    public Transform target;
    private Vector3 offset;

    // Use this for initialization
    void Start()
    {
        offset = transform.position - target.position;
    }

    
    void Update()
    {

        transform.position = target.position + offset;

    }
}
