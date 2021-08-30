using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{

	public Pong pong;

	void OnBecameInvisible()
	{
		pong.Reset(transform.position.x);
	}
}