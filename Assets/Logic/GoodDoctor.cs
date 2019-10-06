using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodDoctor : MonoBehaviour
{

	public enum Surgury
	{
		Leg,
		Arm,
		Core,
		Head
	}

	public Surgury free;
	public Dialogue congrats;
	public AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		// FREE SURGURY

		var player = FindObjectOfType<Player>();
		var playerDistance = Vector3.Distance(transform.position, player.transform.position);
		if(playerDistance < 3 && Input.GetKeyDown(KeyCode.O))
		{
			switch (free)
			{
				case Surgury.Leg:
					player.augLeg = true;
					break;
				case Surgury.Arm:
					player.augArm = true;
					break;
				case Surgury.Core:
					player.augChest = true;
					break;
				case Surgury.Head:
					player.augHead = true;
					break;
			}

			player.SetDia(congrats);

			AudioSource.PlayClipAtPoint(clip, transform.position);

		}
	}
}
