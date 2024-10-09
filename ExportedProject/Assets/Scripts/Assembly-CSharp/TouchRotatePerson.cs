using UnityEngine;
using UnityEngine.EventSystems;

public class TouchRotatePerson : MonoBehaviour, IDragHandler, IBeginDragHandler, IEventSystemHandler
{
	private float _prevY;

	private float _nextY;

	private float _delta;

	private Vector3 _vectorForInsertEulerPerson;

	public Transform _anglePerson;

	public void OnBeginDrag(PointerEventData eventData)
	{
		_prevY = eventData.position.x;
	}

	public void OnDrag(PointerEventData eventData)
	{
		_nextY = eventData.position.x;
		_delta = (0f - (_nextY - _prevY)) / 6.5f;
		_vectorForInsertEulerPerson = _anglePerson.eulerAngles;
		_vectorForInsertEulerPerson.y -= _delta;
		_anglePerson.eulerAngles = _vectorForInsertEulerPerson;
		_prevY = eventData.position.x;
	}
}
