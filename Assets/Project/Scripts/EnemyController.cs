using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
	#region Constants
	private float SPEED = 4;
	#endregion Constants

	#region Fields
	private Vector2 direction;
	#endregion Fields

	#region Methods
	public void Start ()
	{
		this.direction = Vector2.right;
	}

	public void Update ()
	{
		this.GetComponent<Rigidbody2D>().velocity = new Vector3
		(
			SPEED * direction.x,
			this.GetComponent<Rigidbody2D>().velocity.y
		);

		if (this.transform.position.y < -10)
		{
			GameObject.Destroy(this.gameObject);
		}
	}

	public void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.gameObject.tag == "Wall")
		{
			GameObject wall = collision.gameObject;

			if (this.transform.position.x < wall.transform.position.x && this.GetComponent<Rigidbody2D>().velocity.x >= -0.01f ||
				this.transform.position.x > wall.transform.position.x && this.GetComponent<Rigidbody2D>().velocity.x <= 0.01f)
			{
				direction *= -1;
				this.GetComponentInChildren<Rotator>().speed *= -1;
			}
		}
	}
	#endregion Methods
}