using UnityEngine;

public class DoorOpen : MonoBehaviour
{
	public GameHelper gameHelper;

	private Animator anim;

	private void Start()
	{
		anim = GetComponent<Animator>();
	}

	private void Update()
	{
		if (Vector3.Distance(base.transform.position, gameHelper.playerController.transform.position) <= 4f)
		{
			if (!anim.GetBool("Open"))
			{
				anim.SetBool("Open", true);
			}
		}
		else if (anim.GetBool("Open"))
		{
			anim.SetBool("Open", false);
		}
	}
}
