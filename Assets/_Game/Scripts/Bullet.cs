using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GameUnit
{
    public Transform visualTrans;
    public float rotationSpeed;
    public Vector3 direction = Vector3.zero;
    public Vector3 startPosition = Vector3.zero;
    public float range = 0f;
    public float speed = 0f;
    public GameObject attacker;
    public GameObject defender;
    public float damage;
    public void OnInit(Vector3 dir, Vector3 startPos, float rang, float spe,GameObject ob,float dama)
    {
        direction = dir;
        startPosition = startPos;
        range = rang;
        speed = spe;
        attacker = ob;
        damage = dama;
        transform.localRotation = Quaternion.Euler(90, 0, 0);
    }
    public virtual void Update()
    {
        if (GameManager.Instance.State == GameState.MainMenu)
            return;
        visualTrans.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        transform.Translate(direction * speed * Time.deltaTime,Space.World);
        if (Vector3.Distance(transform.position, startPosition) >= range)
        {
            Pools.Instance.Despawn(this);
        }
    }
}
