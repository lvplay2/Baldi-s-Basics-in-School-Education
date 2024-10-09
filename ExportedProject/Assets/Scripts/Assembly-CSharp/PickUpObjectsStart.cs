using UnityEngine;

public class PickUpObjectsStart : MonoBehaviour
{
	private Vector3 _startVector;

	private Vector3 _startRot;

	private MeshRenderer _mr;

	private CapsuleCollider _capsuleCol;

	private void Awake()
	{
		_mr = GetComponent<MeshRenderer>();
		_capsuleCol = GetComponent<CapsuleCollider>();
		_startVector = base.transform.position;
		_startRot = base.transform.eulerAngles;
	}

	private void OnEnable()
	{
		base.transform.position = _startVector;
		_mr.enabled = true;
		_capsuleCol.enabled = true;
		base.transform.eulerAngles = _startRot;
	}
}
