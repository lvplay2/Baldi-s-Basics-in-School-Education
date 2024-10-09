using UnityEngine;

public class ResetPickup : MonoBehaviour
{
	private Vector3 _startPos;

	private Vector3 _startRot;

	private CapsuleCollider _cc;

	private BoxCollider _box;

	private void Awake()
	{
		_startPos = base.transform.position;
		_startRot = base.transform.eulerAngles;
		_cc = GetComponent<CapsuleCollider>();
		_box = GetComponent<BoxCollider>();
	}

	private void OnEnable()
	{
		_cc.enabled = true;
		_box.enabled = false;
		base.transform.position = _startPos;
		base.transform.eulerAngles = _startRot;
		_cc.isTrigger = true;
	}
}
