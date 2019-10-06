using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceRing : MonoBehaviour
{
	public bool hasWon = false;

	public bool Won {
		get
		{
			var buttons = GetComponentsInChildren<IceButton>();
			
			foreach (var button in buttons)
			{
				if (!button.Active)
					return false;
			}
			return true;
		}
	}
	public AudioClip victory;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if(Won && !hasWon)
		{
			hasWon = true;
			GetComponent<Renderer>().material.color = Color.blue;
			AudioSource.PlayClipAtPoint(victory, transform.position);
		}
	}
}
