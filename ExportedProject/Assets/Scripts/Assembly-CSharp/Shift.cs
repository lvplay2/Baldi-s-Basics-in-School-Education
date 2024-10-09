using UnityEngine;
using UnityEngine.EventSystems;

public class Shift : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IEventSystemHandler
{
	public PlayerController playerController;

	public PlayerComponent playerComponent;

	public float energyСonsumption;

	public float RestTime;

	private bool restApply = true;

	public void OnPointerDown(PointerEventData eventData)
	{
		if (playerComponent.Energy > 0.1f && playerController.GetPubMotion() != Vector3.zero)
		{
			playerComponent.isRun = true;
		}
		else
		{
			playerComponent.isRun = false;
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		playerComponent.isRun = false;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftShift) && restApply && playerController.GetPubMotion() != Vector3.zero)
		{
			if (playerComponent.Energy > 0.09f)
			{
				playerComponent.isRun = true;
			}
			else
			{
				playerComponent.isRun = false;
			}
		}
		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			playerComponent.isRun = false;
		}
		if (playerComponent.isRun && playerComponent.Energy >= 0.1f && playerController.GetPubMotion() != Vector3.zero)
		{
			playerController.speedWalk = playerController.runSpeed;
			playerComponent.Energy -= energyСonsumption * Time.deltaTime;
		}
		if (playerComponent.Energy < 0.1f)
		{
			playerController.speedWalk = playerController.speedSave;
			playerComponent.Energy = 0.1f;
			playerComponent.isRun = false;
			StopAllCoroutines();
		}
		if (!playerComponent.isRun && playerComponent.Energy < 1f)
		{
			playerController.speedWalk = playerController.speedSave;
		}
	}
}
