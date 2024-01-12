using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] WheelCollider[] wheelColliders;

    Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float v = Input.GetAxis("Vertical1");

        foreach (var wheelCollider in wheelColliders)
        {
            wheelCollider.brakeTorque = 0f;
            wheelCollider.motorTorque = v * 500f;
        }

        if (v == 0)
        {
            foreach (var wheelCollider in wheelColliders)
            {
                wheelCollider.brakeTorque = 1000f;
            }
        }

        float h = Input.GetAxis("Horizontal1");

        for (int i = 0; i < 2; i++)
        {
            wheelColliders[i].steerAngle = h * 45f;
        }
    }
}
