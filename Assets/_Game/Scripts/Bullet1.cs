using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet1 : Bullet
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject==attacker)
        {
            return;
        }
        if (other.CompareTag("NPC")||other.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<Character>().CalcuDameAndDie(damage))
            {
                attacker.GetComponent<Character>().AddPoint();
            }
            Pools.Instance.Despawn(this);
        }

    }
}
