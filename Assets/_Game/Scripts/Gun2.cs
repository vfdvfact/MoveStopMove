using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun2 : Gun
{
    public Vector3[] BangDirections;
    public float spreadAngle = 25f;
    int batchAmout=3;
    void Start()
    {
        BangDirections = new Vector3[batchAmout];
    }

    // Update is called once per frame
    void Update()
    {
        CoolDown();
    }
    public override void Fire(Vector3 bangDirection,GameObject attacker)
    {
        bangDirection = bangDirection.normalized;
        BangDirections[0] = bangDirection;
        BangDirections[1] = Quaternion.Euler(0, spreadAngle, 0) * bangDirection;
        BangDirections[2] = Quaternion.Euler(0, -spreadAngle, 0) * bangDirection;
        for (int i = 0; i < BangDirections.Length; i++)
        {
            Pools.Instance.Spawn<Bullet>(PoolType.Bullet2, transform.position, Quaternion.identity).OnInit(BangDirections[i], transform.position, ranged, speed, attacker, damage);
        }
        pastDuration = 0f;
        isCooledDown = false;
    }
    void CoolDown()
    {
        if (pastDuration >= durationLimit)
            return;
        pastDuration += Time.deltaTime;
        if (pastDuration >= durationLimit)
            isCooledDown = true;
    }
}
