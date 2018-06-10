using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour
{
	#region Delegates
	public delegate void OnHitSpikeAction();
	public delegate void OnHitGoombaAction();
	public delegate void OnHitOrbAction();
    public delegate void OnHitHammerAction();
    #endregion Delegates

    #region Events
    public OnHitGoombaAction OnHitGoomba;
	public OnHitSpikeAction OnHitSpike;
	public OnHitOrbAction OnHitOrb;
    public OnHitHammerAction OnHitHammer;
    #endregion Events

    #region Constants
    private float SPEED = 1000;
	private float JUMP_SPEED = 500;
	#endregion Constants

	#region Fields
	private Vector3 leftBound;
	private Vector3 rightBound;
	private bool canJump;
	private bool isOnLadder;
	private bool climbing;
    private bool hammer;
    #endregion Fields

    #region Properties
    public bool IsOnLadder { set { isOnLadder = value; } }
	#endregion Properties

	#region Methods
	public void Update ()
	{
		ProcessInput();

		if (climbing)
		{
			this.GetComponent<Rigidbody2D>().gravityScale = 0;
		}
		else
		{
			this.GetComponent<Rigidbody2D>().gravityScale = 1;
		}
	}

	private void ProcessInput ()
	{
		if (Input.GetKey("left") || Input.GetKey("a")) { this.GetComponent<Rigidbody2D>().AddForce(Vector2.left * SPEED * Time.deltaTime); }
		if (Input.GetKey("right") || Input.GetKey("d")) { this.GetComponent<Rigidbody2D>().AddForce(Vector2.right * SPEED * Time.deltaTime); }

		if (!isOnLadder||hammer)
		{
			climbing = false;

			if (Input.GetKeyDown("up") || Input.GetKeyDown("w") || Input.GetKeyDown("space"))
			{
				Jump();
			}
		}
		else
		{
                Debug.Log("escales");
                if (Input.GetKey("up") || Input.GetKey("w")) climbing = true;

                if (climbing)
                {
                    if (Input.GetKey("up") || Input.GetKey("w")) { this.GetComponent<Rigidbody2D>().AddForce(Vector2.up * SPEED * Time.deltaTime); }
                    if (Input.GetKey("down") || Input.GetKey("s")) { this.GetComponent<Rigidbody2D>().AddForce(Vector2.down * SPEED * Time.deltaTime); }
                }

                isOnLadder = false;
            
		}
	}

    internal void hammerTime()
    {
        hammer = true;
        GetComponent<AudioSource>().Play();
        Invoke("stopHammerTime", 7.0f);
        print("yo");
    }


    private void stopHammerTime()
    {
        hammer = false;
        GetComponent<AudioSource>().Stop();
    }

    private void Jump (bool force = false)
	{
		if (canJump || force)
		{
			canJump = false;

			this.GetComponent<Rigidbody2D>().AddForce(Vector2.up * JUMP_SPEED);
		}
	}

	public void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.gameObject.tag == "Bound")
		{
			canJump = true;
		}

		if (collision.gameObject.GetComponent<SpikeController>() != null)
		{
            if (hammer)
            {

            }
			else if (OnHitSpike != null)
			{
				OnHitSpike();
			}
		}
		else if (collision.gameObject.GetComponent<EnemyController>() != null)
		{
			EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();

            if (hammer)
            {
                GameObject.Destroy(collision.gameObject);
                if (OnHitGoomba != null)
                {
                    OnHitGoomba();
                }
            }
			
			else if (this.transform.position.y > enemy.transform.position.y + enemy.GetComponent<BoxCollider2D>().size.y / 2)
			{
				GameObject.Destroy(collision.gameObject);

				Jump(true);

				if (OnHitGoomba != null)
				{
					OnHitGoomba();
				}
			}
			else
			{
				if (OnHitSpike != null) { OnHitSpike(); }
			}
		}
		else if (collision.gameObject.GetComponent<OrbController>() != null)
		{
			if (OnHitOrb != null)
			{
				OnHitOrb();
			}
		}
        else if (collision.gameObject.GetComponent<HamerController>() != null)
        {
            if (OnHitHammer != null)
            {
                Destroy(collision.gameObject);
                OnHitHammer();
                
            }
        }
    }
	#endregion Methods
}