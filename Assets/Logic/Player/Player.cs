using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

	public float swingCooldown;
	public bool augHead, augLeg, augArm, augChest;

	public float superMoveSpeedMul = 1.5f;
	public SkinnedMeshRenderer head, leg, arm, chest;
	public Animator ac;


	public float baseDamage = 1;
	public float superDamage = 3;


	public Gui gui;
	// Start is called before the first frame update
	void Start()
	{

	}
	public float turnSpeed = 60;
	public float moveForce = 1000;
	public float jumpSpeed = 3;
	public float superSpeedMul = 2;


	bool canJump = true;

	public void OnCollisionEnter(Collision collision)
	{
		canJump = true;
	}


	public void SetDia(Dialogue d)
	{
		if (!gui.currentDialogue)
		{
			gui.currentDialogue = d;
			gui.dialoguePos = transform.position;
		}
	}
	private void FixedUpdate()
	{
		var mul = augLeg ? superSpeedMul : 1;
		var doJump = canJump && Input.GetKey(KeyCode.Space);

		var jumpForce = doJump ? jumpSpeed : 0;
		if (doJump) { canJump = false; }
		var turn = Input.GetAxis("Horizontal");
		transform.Rotate(0, turnSpeed * turn * Time.fixedDeltaTime, 0);

		var movement = new Vector3(0, 0, Input.GetAxis("Vertical"));
		var force = moveForce * Camera.main.transform.TransformVector(movement);
		var rb = GetComponent<Rigidbody>();
		rb.AddForce(mul * force);

		ac.SetFloat("Forward", rb.velocity.magnitude);
		ac.SetBool("OnGround", canJump);
		ac.SetFloat("Turn", turn);
		rb.AddForce(0, mul * jumpForce, 0, ForceMode.VelocityChange);

	}

	public Health Health => GetComponent<Health>();

	// Update is called once per frame
	private void Update()
	{
		if (augChest && augHead && augLeg && augArm)
		{
			SceneManager.LoadScene("fullconversion");
		}

		swingCooldown -= Time.deltaTime;
		if (Input.GetKeyDown(KeyCode.Q) && swingCooldown < 0)
		{
			ac.SetTrigger("Punch");


			swingCooldown = 1;
			var healths = FindObjectsOfType<Health>();

			var cool = Physics.OverlapSphere(transform.position, 2);

			foreach (var thing in cool)
			{
				var health = thing.GetComponentInParent<Health>();
				if (health)
				{
					health.Hurt(augArm ? 1 : 0.3f, Health);
				}
			}
		}



		if (Vector3.Distance(transform.position, gui.dialoguePos) > gui.maxDialogueDist)
		{
			gui.currentDialogue = null;
		}

		if (!gui.currentDialogue && Input.GetKeyUp(KeyCode.E))
		{
			gui.Hint = "Press E to Speak";
			var dl = FindObjectsOfType<DialogueThing>();

			var minDistance = gui.maxDialogueDist;
			Dialogue best = null;
			foreach (var d in dl)
			{
				var dist = Vector3.Distance(transform.position, d.transform.position);

				if (dist < minDistance)
				{
					best = d.dialogue;
					minDistance = dist;
				}
			}
			SetDia(best);
		}

		else if (gui.currentDialogue)
		{
			gui.Hint = "press E for next";
		}
		else
		{


			gui.Hint = "put horizen here";
		}




		gui.food = Health.food;
		gui.health = Health.health;


		head.enabled = augHead;


		leg.enabled = augLeg;

		arm.enabled = augArm;
		chest.enabled = augChest;
		var rb = GetComponent<Rigidbody>();

		rb.mass = augChest ? 55 : 50;
		Health.armor = augChest ? 0 : 1;

		// reduce food consuption
	}
}
