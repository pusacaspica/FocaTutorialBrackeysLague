﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    public float rotationSpeed = 5f;
    public Transform target;
    public NavMeshAgent agent;
    // Start is called before the first frame update
    void Start() {
        agent = GetComponent <NavMeshAgent> ();
    }

    void Update(){
        if(target!=null){
            agent.SetDestination(target.position);
            FaceTarget();
        }
    }

    public void MoveToPoint(Vector3 moveTo){
        agent.SetDestination(moveTo);
    }

    public void FollowTarget(Interactible toBeFollowed){
        agent.stoppingDistance = toBeFollowed.radius * 1f;
        agent.updateRotation = false;
        target = toBeFollowed.interactionTransform;
    }

    public void StopFollowingTarget(){
        agent.stoppingDistance = 0f;
        agent.updateRotation = true;
        target = null;
    }

    public void FaceTarget(){
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }
}
