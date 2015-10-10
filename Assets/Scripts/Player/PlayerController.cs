using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class PlayerController : MonoBehaviour {

    public enum JumpState
    {
        JUMPING,FALLING,NONE
    }
    public enum MoveState
    {
        WALKING,RUNNING,STILL
    }

    JumpState jumpState = JumpState.NONE;
    MoveState moveState = MoveState.STILL;

    float JUMP_FORCE = 700f;
    float SCALE_AMOUNT_X = .5f;
    float SCALE_AMOUNT_Y = 2.5f;
    float JUMP_TIME = .5f;
    float WALK_TIME_SPEEDUP = .75f;
    float SLOW_VEL_X = 30f;
    float FAST_VEL_X = 50f;

    int walkingDirection = 1;

    float jumpCounter = 0f;
    float walkCounter = 0f;

    Rigidbody2D rigidBody;
    SpriteRenderer renderer;
    Vector3 startPos;

	// Use this for initialization
	void Start () {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        renderer = gameObject.GetComponent<SpriteRenderer>();
        this.changeColor(Color.red);
        startPos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        //Control Character

        if (jumpState == JumpState.JUMPING)
        {
            jumpCounter += Time.deltaTime;
            gameObject.transform.localScale =
                Vector3.Lerp(gameObject.transform.localScale, new Vector3(SCALE_AMOUNT_X, SCALE_AMOUNT_Y, 1), Time.deltaTime);
            if (jumpCounter > JUMP_TIME)
            {
                jumpState = JumpState.FALLING;
                jumpCounter = 0f;
            }
        }
        else if (jumpState == JumpState.FALLING)
        {
            jumpCounter += Time.deltaTime;
            Vector3 tempVec = Vector3.Lerp(gameObject.transform.localScale, new Vector3(1/SCALE_AMOUNT_X, 1/SCALE_AMOUNT_Y, 1), Time.deltaTime);
            tempVec.x = Mathf.Min(tempVec.x, 1);
            tempVec.y = Mathf.Max(tempVec.y, 1);
            tempVec.z = 1;
            gameObject.transform.localScale = tempVec;

            if (jumpCounter > JUMP_TIME)
            {
                jumpCounter = 0f;
            }
        }
        
        if (moveState == MoveState.WALKING)
        {
            rigidBody.velocity = Vector2.Lerp(rigidBody.velocity, new Vector2(walkingDirection * SLOW_VEL_X, 0f), 2 * Time.deltaTime);
            walkCounter += Time.deltaTime;
            if (walkCounter > WALK_TIME_SPEEDUP)
            {
                moveState = MoveState.RUNNING;
            }
        }
        else if (moveState == MoveState.RUNNING)
        {
            rigidBody.velocity = Vector2.Lerp(rigidBody.velocity, new Vector2(walkingDirection * FAST_VEL_X, 0f), 2 * Time.deltaTime);
        }
        else if (moveState == MoveState.STILL)
        {
            rigidBody.velocity = Vector2.Lerp(rigidBody.velocity, new Vector2(0, 0f), Time.deltaTime);
        }
	}

    public void respawn()
    {
        this.transform.position = startPos;
        rigidBody.velocity = new Vector2(0, 0);
    }

    public void setNotWalking()
    {
        moveState = MoveState.STILL;
        walkCounter = 0f;
    }

    public void setWalking(float xAxisAmount)
    {
        moveState = MoveState.WALKING;
        walkingDirection = (int)(xAxisAmount / Mathf.Abs(xAxisAmount));
    }

    public void changeColor(Color color) {
        renderer.color = color;
        TellPlatformsColorChanged(color);
    }

    private void TellPlatformsColorChanged(Color c)
    {
        PlatformController[] platforms = FindObjectsOfType<PlatformController>();
        foreach (PlatformController platform in platforms)
        {
            platform.CharacterColorChanged(c);
        }
    }

    public void jump()
    {
        if (!(jumpState == JumpState.JUMPING || jumpState == JumpState.FALLING))
        {
            rigidBody.AddForce(new Vector2(0, JUMP_FORCE));
            jumpState = JumpState.JUMPING;
        }
    }

    //Reset the jump
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == Tags.PLATFORM)
        {
            jumpState = JumpState.NONE;
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
