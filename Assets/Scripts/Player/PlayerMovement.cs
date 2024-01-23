using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameObject parent;
    private float yOffset;
    private void Start()
    {
        parent = transform.parent.gameObject;
    }
    void Update()
    {
        Vector3 newPos = parent.transform.position;
        transform.position = newPos;
    }
}
