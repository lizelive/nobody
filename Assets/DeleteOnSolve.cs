using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeleteOnSolve : MonoBehaviour
{

	public IceRing[] toSolve;
	public AudioClip winSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (toSolve.All(x => x.hasWon))
		{
			if(winSound)
				AudioSource.PlayClipAtPoint(winSound, transform.position);

			Destroy(gameObject);
		}
    }
}
