using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour
{
	#region Unity Fields
	public float speed = 200;
	#endregion Unity Fields
	
	#region Methods
	public void Update ()
	{
		this.transform.localEulerAngles = new Vector3
		(
			this.transform.localEulerAngles.x,
			this.transform.localEulerAngles.y + speed * Time.deltaTime,
			this.transform.localEulerAngles.z
		);
	}
	#endregion Methods
}
