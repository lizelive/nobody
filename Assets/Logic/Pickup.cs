﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
	public float food;
	public float range;
	public bool winTheGame;
	private void OnCollisionEnter(Collision collision)
	{
		var go = collision.gameObject;
		var player = go.GetComponent<Player>();
		if (player)
		{

			if (winTheGame)
			{
				UnityEngine.SceneManagement.SceneManager.LoadScene("win");

			}
			player.Health.food += food;
			food = 0;
			Destroy(gameObject);
		}
	}
}
