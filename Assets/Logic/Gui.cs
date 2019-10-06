using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Gui : MonoBehaviour
{
	public float food = 1, health = 1;
	public Image foodbar, healthbar;


	public Text dialogueText;
	public Dialogue currentDialogue;
	public GameObject dialogueGo;

	// Start is called before the first frame update

	// Update is called once per frame
	void Update()
    {




		dialogueGo.SetActive(currentDialogue);
		if (currentDialogue) {
			dialogueText.text = currentDialogue.text;


			

			var select = -1;
			for (int i = 0; i < 9; i++)
			{
				var nice = Input.GetKeyDown((KeyCode)(KeyCode.Alpha1 + i));
				if (nice)
				{
					select = i+1;
					break;
				}
			}

			if (select != -1)
				print(select);


			var next = Input.GetKeyDown(KeyCode.E);
			var noOptions = currentDialogue.options == null || currentDialogue.options.Length == 0;
			if (next && noOptions)
				currentDialogue = currentDialogue.next;


		}


		foodbar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 1000 * food);
		healthbar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 1000 * health);

	}
}
