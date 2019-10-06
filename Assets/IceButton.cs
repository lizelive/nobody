using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceButton : MonoBehaviour
{
	public float containedMass = 0;
	public float threshod = 100;

	public Color activeColor = Color.red;
	public bool Active => containedMass >= threshod;
	public AudioClip click;

    // Update is called once per frame
    void Update()
    {
		GetComponent<Renderer>().material.color = Active ? activeColor : Color.white;
    }

	private void OnTriggerEnter(Collider other)
	{
		var rb = other.attachedRigidbody;
		if (rb)
			containedMass += rb.mass;

		if (Active)
			AudioSource.PlayClipAtPoint(click, transform.position);
	}

	private void OnTriggerExit(Collider other)
	{
		var rb = other.attachedRigidbody;
		if (rb)
			containedMass -= rb.mass;

		if (!Active)
			AudioSource.PlayClipAtPoint(click, transform.position);
	}
}
