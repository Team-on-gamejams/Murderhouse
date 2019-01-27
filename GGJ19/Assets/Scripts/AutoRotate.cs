using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    public float speedRotate;

    void Update()
    {
        transform.Rotate(Vector3.forward * speedRotate, Space.World);
    }
}
