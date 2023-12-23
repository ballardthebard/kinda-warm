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

        Animator animator = GetComponent<Animator>();

        if (moveAction != null)
        {
            moveAction.Move();
            animator.SetLayerWeight(1, 1);
        }

        if (attackAction != null)
        {
            attackAction.Attack();

            if (moveAction != null)
                animator.SetLayerWeight(3, 1);
            else
                animator.SetLayerWeight(2, 1);
        }
    }

    public void StopEnemyActions()
    {
        if (moveAction != null)
            moveAction.StopMove();

        if (attackAction != null)
            attackAction.StopAttack();
    }
}