using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class Character : GameUnit
{
    IState<Character> currentState;
    public Transform gunHoldingHand;
    public Animator animator;
    public Gun currentGun;
    public float detectionRadius = 4f;
    public LayerMask detectionLayer;
    public float hp,maxHp;
    public float point=0f;
    public float pointMax=3;
    public bool aniWaiting=false;
    public Collider target;
    public float pastTime;
    public float timeLimit = 2f;
    public WeaponDataSO weaponData;
    public bool isDead=false;
    public virtual void OnEnable()
    {
        isDead = false;
        hp = maxHp;
        target = null;
        pastTime = 0f;
        //changestate
    }
    public Collider GetFilteredCollider()
    {
        Collider[] allHits = Physics.OverlapSphere(TF.position, detectionRadius, detectionLayer);
        // Filter out this GameObject's own collider(s)
        Collider[] filtered = allHits
            .Where(col => col.gameObject != gameObject)
            .ToArray();
        if (filtered.Length > 0 && filtered[0].gameObject.GetComponent<Character>().isDead == false)
        {            
            return target = filtered[0];
        }
        else
        {
            return null;
        }
    }
    public virtual void Die()
    {

    }
    public void ChangeState(IState<Character> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = state;
        currentState.OnEnter(this);
    }
    public void SetAnim(string anim)
    {
        animator.SetTrigger(anim);
    }
    public virtual void AddPoint()
    {
        point++;
        target = null;
    }
    public void LookAt(Vector3 dir)
    {
        if (dir != Vector3.zero)
        {
            TF.rotation = Quaternion.LookRotation(dir);
        }
    }
    /// -----------------------------------------------------------------------------------------------------------------------------
    public virtual void Start()
    {
        InstanGunFromResources();
    }
    public virtual void Update()
    {
        if (GameManager.Instance.State == GameState.GamePlay)
        {
            currentState.OnExecute(this);
        }
    }
    public virtual void InstanGunFromResources()
    {
        currentGun = GameObject.Instantiate(weaponData.weapons[0].weapon, gunHoldingHand);
        currentGun.transform.rotation = Quaternion.Euler(new Vector3(-180, 112, -14));
        currentGun.gameObject.SetActive(true);
    }
    public Vector3 CalcuBangDirection(Vector3 startPoint, Vector3 endPoint)
    {
        return new Vector3(endPoint.x - startPoint.x,0, endPoint.z - startPoint.z);
    }
    public bool IsTargetStillInRange()
    {
        if (target!=null&& Vector3.Distance(target.transform.position, transform.position) < detectionRadius)
        {
            return true;
        }
        else
            return false;
    }
    public virtual bool CalcuDameAndDie(float dama)
    {
        return false;
    }
    public virtual bool GetInputForMove()
    {
        return false;
    }
    public virtual void Move()
    {

    }
    public virtual bool isFindedRandomDestination()
    {
        return false;
    }
    public bool CanSeeTarget()
    {
        Vector3 direction = CalcuBangDirection(transform.position, target.transform.position).normalized;
        // Do the raycast to see if there's a wall in the way
        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, detectionRadius))
        {
            if (hit.transform == target.transform)
            {
                // Direct line of sight
                Debug.Log("see");
                return true;
            }
        }

        // Something else (like a wall) is in the way
        return false;
    }
    public void Fire()
    {
        currentGun.Fire(CalcuBangDirection(currentGun.transform.position, target.transform.position), gameObject);
    }
    public void TriggerFireInTime()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            float animationTime;
            animationTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime; // Get the normalized time (0-1)

            // Trigger method at specific point in animation
            if (animationTime >= 0.5f) // Example: Trigger around 50% of animation
            {
                //Debug.Log("Attack");
                Fire();
                aniWaiting = false;
            }
        }
    }
    public virtual void PauseRun()
    {

    }
    public virtual void ResumeRun()
    {

    }
    public virtual void WaitFailUI()
    {

    }
    public virtual bool IsAllowedRun()
    {
        return true ;
    }
}

