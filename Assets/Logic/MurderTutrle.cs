using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MurderTutrle : MonoBehaviour
{

	public enum TState
	{
		Idle,
		Murder,
		Death,
		Kill
	}

	public float range = 10;
	public float forgetRange = 20;
	public float killRange = 3;

	public TState state;
	public Player agro;
	public float damage = 0.1f;


	public float rageBuild;
	public float rageTime = 10;
	public float rageForce;

	// Start is called before the first frame update
	void Start()
	{

	}

	Rigidbody rb => GetComponent<Rigidbody>();
	public void OnCollisionEnter(Collision collision)
	{
		var player = collision.gameObject.GetComponent<Health>();
		if (player && rb.velocity.magnitude > 3 )
			player.Hurt(0.3f);
	}
	// Update is called once per frame
	void Update()
	{

		var player = FindObjectOfType<Player>();

		var playerDistance = Vector3.Distance(transform.position, player.transform.position);



		if (state == TState.Idle)
		{
			if (playerDistance < range)
			{
				state = TState.Murder;
				agro = player;
			}
		}


		if (state == TState.Murder)
		{


		
				rageBuild += Time.deltaTime;
			if (Vector3.Angle(Vector3.up, transform.up) < 45)
			{
				if (rageBuild < rageTime-1)
				{
					var lookat = player.transform.position;
					lookat.y = transform.position.y;
					transform.LookAt(lookat);
				}
				if (rageBuild > rageTime)
				{
					rb.velocity = transform.forward * rageForce + Vector3.up;
					rageBuild = 0;
				}
			}
			else
			{

				if (rageBuild > rageTime)
				{
					GetComponent<Health>().Hurt(.1f);	
					rageBuild = 0;
				}

				// play helpless wobble noises.
			}



			// kill kill killl




			//if (playerDistance < killRange)
			//{
			//	// kill them dead
			//	player.health -= damage * Time.deltaTime;
			//}

		}
	}
}
