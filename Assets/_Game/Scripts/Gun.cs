using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public bool isCooledDown = false;
    public Vector3 bulletRotation = new Vector3(90f, 0f, 0f);
    public float speed;
    public float damage;
    public float ranged;
    public float pastDuration = 0f;
    public float durationLimit = 3f;
    public virtual void Fire(Vector3 bangDirection,GameObject attacker)
    {

    }
    public void OnInit()
    {

    }
}
