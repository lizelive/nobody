using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
	public float health = 1;
	public float food = 1;
	public float staveDps = 0.4f;
	public float healPerSec = 0.04f;
	public float healFoodRatio = 1;
	public float foodBaseRatio = 0.005f;



	public AudioClip hurtSound;

	public float armor = 0;

	public GameObject dropOnDeath;


	public void Hurt(float ammount, Health by = null)
	{
		if (by == this) return;
		health -= ammount * Mathf.Exp(-armor);


		if (hurtSound)
			AudioSource.PlayClipAtPoint(hurtSound, transform.position);


		TryDie();
		
	}


	public bool essental;

	public void TryDie()
	{
		if (health <= 0)
		{
			if (dropOnDeath)
				Instantiate(dropOnDeath, transform.position, transform.rotation);

			if (essental)
			{
				UnityEngine.SceneManagement.SceneManager.LoadScene("gameover");
			}


			Destroy(gameObject);
		}
	}


	// Update is called once per frame
	void Update()
	{
		food -= Time.deltaTime * foodBaseRatio;

		food = Mathf.Max(0, food);

		if (food <= 0)
		{
			health -= staveDps * Time.deltaTime;

			TryDie();
		}



		if (food > 0.5f && health < 1)
		{
			var toHeal = Mathf.Min(Time.deltaTime * healPerSec, 1 - health, food / healFoodRatio);

			health += toHeal;
			food -= toHeal * healFoodRatio;
		}

	}
}
