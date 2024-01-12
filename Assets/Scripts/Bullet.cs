using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] float force = 500f;
    [SerializeField] GameObject explosion;

    private void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * force);
    }

    private void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject effect = Instantiate(explosion, transform.position, Quaternion.identity);

        Destroy(gameObject);
        Destroy(effect, 2f);
    }
}
