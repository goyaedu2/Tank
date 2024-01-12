using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    [SerializeField] WheelCollider[] wheelColliders;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] GameObject expolsion;
    [SerializeField] GameObject turret;

    float hp = 1f;

    Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float v = Input.GetAxis("Vertical");

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

        float h = Input.GetAxis("Horizontal");

        for (int i = 0; i < 2; i++)
        {
            wheelColliders[i].steerAngle = h * 45f;
        }

        // ÃÑ¾Ë ¹ß»ç
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            turret.transform.Rotate(-Vector3.up * 20 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E))
        {
            turret.transform.Rotate(Vector3.up * 20 * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        hp -= 0.1f;
        Debug.Log("hp: " + hp);

        if (hp <= 0)
        {
            GameObject effect = Instantiate(expolsion, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(effect, 2f);
        }
    }
}
