using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private IMove moveAction;
    private IAttack attackAction;

    private void Start()
    {
        moveAction = GetComponent<IMove>();
        attackAction = GetComponent<IAttack>();

        if (moveAction != null)
            moveAction.Move();

        if (attackAction != null)
            attackAction.Attack();
    }

    public void StopEnemyActions()
    {
        if (moveAction != null)
            moveAction.StopMove();

        if (attackAction != null)
            attackAction.StopAttack();
    }
}