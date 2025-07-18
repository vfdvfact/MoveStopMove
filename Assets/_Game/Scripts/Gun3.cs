using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun3 : Gun
{
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CoolDown();
    }
    public override void Fire(Vector3 bangDirection, GameObject attacker)
    {
        Pools.Instance.Spawn<Bullet>(PoolType.Bullet3, transform.position, Quaternion.identity).OnInit(bangDirection, transform.position, ranged, speed, attacker, damage);
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
