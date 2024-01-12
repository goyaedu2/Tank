using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTank : MonoBehaviour
{
    [SerializeField] private Transform turret;
    Transform tr;

    private void Start()
    {
        tr = transform;
    }

    IEnumerator move()
    {
        Vector3 targetPos = new Vector3(tr.position.x,
            tr.position.y,
            tr.position.z);

        while (Vector3.Distance(tr.position, targetPos) >= 0.01f)
        {
            tr.position = Vector3.MoveTowards(tr.position, targetPos, 0.1f);
            yield return null;
        }
    }

    IEnumerator turnTurret(float rotateY, float duration)
    {
        float startTime = Time.time;
        float initTime = Time.time - startTime;
        Quaternion turretRot = turret.localRotation;

        while (initTime <= duration)
        {
            turret.localRotation = Quaternion.Slerp(turretRot,
                Quaternion.Euler(0, rotateY, 0), initTime / duration);
            initTime = Time.time - startTime;
            yield return null;
        }
    }

    IEnumerator startTank()
    {
        while (true)
        {
            yield return StartCoroutine(move());
        }
    }
}