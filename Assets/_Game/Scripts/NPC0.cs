using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class NPC0 : Character
{
    Vector3 destination;
    public float patrolRadius=4f;
    public NavMeshAgent agent;
    public NPCIndicatorManager indicatorManager;
    public override void OnEnable()
    {
        base.OnEnable();
        ChangeState(new NPCIdleState());
    }
    public override bool CalcuDameAndDie(float dama)
    {
        if (hp <= 0)
            return false;
        hp = hp - dama;
        if (hp <= 0)
        {
            ChangeState(new NPCDieState());
            return true;
        }
        else
        {
            return false;
        }
    }
    public override void Die()
    {
        pastTime = pastTime + Time.deltaTime;
        if (pastTime >= timeLimit)
        {
            pastTime = 0f;
            NPCSpawner.Instance.SpawnWithWaiting(this.PoolType);
            indicatorManager.RemoveNPC(transform);
            Pools.Instance.Despawn(this);

        }
    }
    public override bool isFindedRandomDestination()
    {
        Vector2 random2D = Random.insideUnitCircle * patrolRadius;
        Vector3 randomPosition = new Vector3(random2D.x, TF.position.y, random2D.y) + transform.position;

        NavMeshHit navHit;
        if (NavMesh.SamplePosition(randomPosition, out navHit, patrolRadius, NavMesh.AllAreas))
        {
            destination = navHit.position;
            return true;
        }
        else
            return false;
    }
    public override void PauseRun()
    {
        agent.isStopped = true;
    }
    public override void ResumeRun()
    {
        agent.isStopped = false;
    }
    //---------------------------------------------------------------------------------------------------------------------
    public override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        detectionLayer = LayerMask.GetMask("NPC", "Player");
    }
    public override void Update()
    {
        if (GameManager.Instance.State != GameState.MainMenu&&GameManager.Instance.State!=GameState.GameVictory)
        {
            base.Update();
        }
        else
        {
            indicatorManager.RemoveNPC(transform);
            Pools.Instance.Despawn(this);

        }
    }
    public override bool GetInputForMove()
    {
        if (agent.pathPending || agent.remainingDistance <=0.1f)
        {
            return false;
        }
        else return true;
    }
    public override void Move()
    {
        agent.SetDestination(destination);
    }
    public override bool IsAllowedRun()
    {
        if(!agent.isStopped)
        {
            return true;
        }
        else
            return false;
    }
}
