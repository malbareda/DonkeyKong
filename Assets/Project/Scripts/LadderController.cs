using UnityEngine;
using System.Collections;

public class LadderController : MonoBehaviour
{
	public void OnTriggerStay2D (Collider2D collider)
	{
		if (collider.gameObject.GetComponent<PlayerController>() != null)
		{
			collider.gameObject.GetComponent<PlayerController>().IsOnLadder = true;
		}
	}
}