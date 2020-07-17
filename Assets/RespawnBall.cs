using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnBall : MonoBehaviour
{
	public Transform ballStartPos;
	Rigidbody rBody;
	public float minResetPos = -1;
	float resetTimer;
	public float minMagnitude = 0.1f;

	void Start()
	{
		rBody = GetComponent<Rigidbody>();
		rBody.maxAngularVelocity = 100;
	}

	public void ResetBallPos()
	{
		
		rBody.position = ballStartPos.position;
		rBody.velocity = rBody.angularVelocity = Vector3.zero;
	}

	void Update()
	{

		if (transform.position.y < minResetPos)
		{
			ResetBallPos();
		}

		if (rBody.velocity.sqrMagnitude < minMagnitude)
		{
			resetTimer += Time.deltaTime;
			if (resetTimer > 10)
			{
				ResetBallPos();
				resetTimer = 0;
			}
		}
		else
		{
			resetTimer = 0;
		}
	}
}
