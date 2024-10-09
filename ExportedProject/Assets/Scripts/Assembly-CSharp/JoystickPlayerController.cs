using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class JoystickPlayerController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler, IEventSystemHandler
{
	private Image background;

	private Image stick;

	private Vector2 inputVector;

	private void Start()
	{
		background = GetComponent<Image>();
		stick = base.transform.GetChild(0).GetComponent<Image>();
	}

	public void OnDrag(PointerEventData eventData)
	{
		Vector2 localPoint;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle(background.rectTransform, eventData.position, eventData.pressEventCamera, out localPoint))
		{
			localPoint.x /= background.rectTransform.sizeDelta.x;
			localPoint.y /= background.rectTransform.sizeDelta.y;
			inputVector = new Vector2(localPoint.x * 2f, localPoint.y * 2f);
			inputVector = ((!(inputVector.magnitude > 1f)) ? inputVector : inputVector.normalized);
			stick.rectTransform.anchoredPosition = new Vector3(inputVector.x * (background.rectTransform.sizeDelta.x / 2f), inputVector.y * (background.rectTransform.sizeDelta.y / 2f));
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		inputVector = Vector2.zero;
		stick.rectTransform.anchoredPosition = Vector3.zero;
	}

	public float Horizontal()
	{
		return inputVector.x;
	}

	public float Vertical()
	{
		return inputVector.y;
	}

	public void ResetTarget()
	{
		inputVector = Vector2.zero;
		stick.rectTransform.anchoredPosition = Vector3.zero;
	}
}
