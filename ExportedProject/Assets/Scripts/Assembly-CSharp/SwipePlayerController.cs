using UnityEngine;
using UnityEngine.EventSystems;

public class SwipePlayerController : MonoBehaviour, IDragHandler, IEventSystemHandler
{
	private PlayerController playerController;

	private void Start()
	{
		playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}

	public void OnDrag(PointerEventData eventData)
	{
		Vector2 delta = eventData.delta;
		playerController.CameraRotate(delta);
	}
}
