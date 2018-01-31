using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour {
    Animator animator;
    Player player;
    public new Rigidbody2D rigidbody2D;
    public float maxMoveSpeed = 10f;

    public Vector2 lastPos;

    public void Move(float horizontalMovement, float verticalMovement)
    {
        if (Mathf.Abs( horizontalMovement) > 0.01f ||Mathf.Abs( verticalMovement) > 0.01f)
        {
            //Debug.Log("Stop Interact");
            player.BreakInteraction();
        }

        //movement
        Vector2 movementVector = new Vector2(horizontalMovement,verticalMovement);
        this.rigidbody2D.velocity = movementVector * maxMoveSpeed * Time.fixedDeltaTime;

    }

	// Use this for initialization
	void Start () {
		if(this.rigidbody2D == null)
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        RotateDirection();
        
	}

    public void RotateDirection()
    {
        Vector2 curPos = (Vector2)this.transform.position;
        Vector2 delta = curPos - lastPos;
        if(delta.magnitude < 0.1f)
        {
            return;
        }
        this.transform.up = delta.normalized;

        lastPos = curPos;
    }
}
