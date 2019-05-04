using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimation : UnitComponent
{
    private Animator m_animator;

    protected override void Start()
    {
        base.Start();

        m_animator = GetComponentInChildren<Animator>();

        m_unitView.UnitMovement.UnitMoving += Move;
        m_unitView.UnitMovement.UnitStoppedMoving += Idle;
    }

    private void Move()
    {
        m_animator.SetBool("Moving", true);
    }

    private void Idle()
    {
        m_animator.SetBool("Moving", false);
    }
}
