using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bullet3 : Bullet
{
    bool isComeback = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == attacker)
        {
            return;
        }
        if (other.CompareTag("NPC") || other.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<Character>().CalcuDameAndDie(damage))
            {
                attacker.GetComponent<Character>().AddPoint();
            }
            Pools.Instance.Despawn(this);
        }

    }
    public override void Update()
    {
        if (GameManager.Instance.State == GameState.MainMenu)
            return;

        if (!isComeback)
        {
            visualTrans.Rotate(0, 0, rotationSpeed * Time.deltaTime);
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
            if (Vector3.Distance(transform.position, startPosition) >= range)
            {
                isComeback = true;
            }
        }
        else
        {
            visualTrans.Rotate(0, 0, rotationSpeed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, attacker.transform.position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, attacker.transform.position) <= 3)
            {
                isComeback = false;
                Pools.Instance.Despawn(this);
            }
        }
    }
}
