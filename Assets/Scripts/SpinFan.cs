using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinFan : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 90.0f; 

    void Update()
    {
        RotateZ();
    }

    private void RotateZ()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
