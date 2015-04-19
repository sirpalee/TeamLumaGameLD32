﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private static PlayerController instance = null;

    PlayerController()
    {
        instance = this;
    }

    public static PlayerController Instance
    {
        get
        {
            return instance;
        }
    }

    private Animator m_animator;

    public float walkSpeed = 1.0f;

    [HideInInspector]
    public float expectedDeathTime;

    public float initialLifeTime = 600;

    // Use this for initialization
    void Start ()
    {
        m_animator = gameObject.GetComponentInChildren<Animator>();
        expectedDeathTime = Time.time + initialLifeTime;
    }
	
    // Update is called once per frame
    void FixedUpdate ()
    {
        if (Camera.main == null) // ie the camera has not be done shit with
            return;
        Ray rayFromMouse = Camera.main.ScreenPointToRay(Input.mousePosition);

        Vector3 groundPos = rayFromMouse.origin - rayFromMouse.direction * (rayFromMouse.origin.z / rayFromMouse.direction.z);

        Vector3 aimingDirection = groundPos - transform.position;
        transform.rotation = Quaternion.FromToRotation(new Vector3(0.0f, 1.0f, 0.0f), aimingDirection);

        Vector3 speed = new Vector3(0.0f, 0.0f);
        if (Input.GetKey(KeyCode.W))
            speed.y = Time.fixedDeltaTime;
       else if (Input.GetKey(KeyCode.S))
           speed.y = -Time.fixedDeltaTime;

        if (Input.GetKey(KeyCode.A))
           speed.x = -Time.fixedDeltaTime;
        else if (Input.GetKey(KeyCode.D))
           speed.x = Time.fixedDeltaTime;

        speed = speed * walkSpeed;

        if (speed.sqrMagnitude > 0.001f)
            m_animator.SetBool("walk", true);
        else
            m_animator.SetBool("walk", false);

        transform.position = speed + transform.position;

        // move the camera towards the player
        Vector3 camPos = Camera.main.transform.position;
		Vector3 targetPos = transform.position;
		targetPos.z = camPos.z;

		Camera.main.transform.position = Vector3.MoveTowards(camPos, targetPos, (targetPos - camPos).magnitude / 2.0f);
    }

    public void RecieveItem(string ItemTypeName)
    {
    }
}
