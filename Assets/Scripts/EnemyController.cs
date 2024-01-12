using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform[] waypoints;
    [SerializeField] GameObject turret;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] GameObject expolsion;

    [SerializeField] float patrolSpeed = 2f;
    [SerializeField] float chaseSpeed = 3f;
    [SerializeField] EnergyBar energyBar;

    GameObject player;
    int waypointIndex = 0;

    float bulletCoolTime = 2f;
    float previousTime = -999f;

    float hp = 1;

    public enum STATE { PATROL, CHASE, ATTACK }
    public STATE state;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        state = STATE.PATROL;
    }

    private void Update()
    {
        switch (state)
        {
            case STATE.PATROL:
                // Player와 거리 확인
                if (Vector3.Distance(player.transform.position, transform.position) < 50)
                {
                    state = STATE.CHASE;
                    break;
                }

                // 순찰
                if (Vector3.Distance(waypoints[waypointIndex].transform.position, transform.position) > 0.1f)
                {
                    Move(waypoints[waypointIndex].transform.position, patrolSpeed);
                }
                else
                {
                    waypointIndex++;
                    if (waypointIndex > waypoints.Length - 1)
                    {
                        waypointIndex = 0;
                    }
                }
                break;
            case STATE.CHASE:
                if (Vector3.Distance(player.transform.position, transform.position) < 30)
                {
                    state = STATE.ATTACK;
                    break;
                }
                else if (Vector3.Distance(player.transform.position, transform.position) >= 50)
                {
                    state = STATE.PATROL;
                    break;
                }
                Move(player.transform.position, chaseSpeed);
                break;
            case STATE.ATTACK:
                if (Vector3.Distance(player.transform.position, transform.position) >= 30)
                {
                    state = STATE.CHASE;
                    break;
                }

                // 공격
                Attack(player.transform.position);
                break;
        }
    }

    void Attack(Vector3 targetPosition)
    {
        Vector3 relativePosition = targetPosition - turret.transform.position;
        float angle = Mathf.Atan2(relativePosition.x, relativePosition.z) * Mathf.Rad2Deg;

        turret.transform.rotation = Quaternion.RotateTowards(turret.transform.rotation, 
            Quaternion.Euler(0, angle, 0), 1f);

        // 총알 발사
        if (previousTime + bulletCoolTime < Time.time)
        {
            Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            previousTime = Time.time;
        }
    }

    void Move(Vector3 targetPosition, float speed)
    {
        Vector3 relativePosition = targetPosition - transform.position;
        relativePosition.Normalize();

        transform.rotation = Quaternion.RotateTowards
            (transform.rotation, Quaternion.LookRotation(relativePosition), 1);

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        hp -= 0.1f;

        energyBar.SetValue(hp);
        Debug.Log("Enemy hp: " + hp);

        if (hp <= 0)
        {
            GameObject effect = Instantiate(expolsion, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(effect, 2f);
        }
    }
}
