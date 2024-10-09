using System.Collections;
using UnityEngine;

public class PersonControl : MonoBehaviour
{
	private Animator _anim;

	private Coroutine _walkPersonCor;

	private Vector3 _worldTransform;

	private Rigidbody _rb;

	private GameObject _getTargetForLowObj;

	private Vector3 _posForLowerObj;

	private CapsuleCollider _capsuleForLowerObj;

	public Transform _pointForParentDown;

	public Transform _pointForParentPickUp;

	public float _speedMove = 10f;

	public bool _isWalk;

	private bool _isHandUse;

	public Transform _pointForPickUp;

	public GameObject _imagePicjUp;

	public GameObject _imageLowerObj;

	[HideInInspector]
	public GameObject _getObjectPickUp;

	private void Awake()
	{
		_isHandUse = false;
		_rb = GetComponent<Rigidbody>();
	}

	private void OnEnable()
	{
		_anim = GetComponent<Animator>();
		_isWalk = false;
	}

	private void Update()
	{
		if (Input.GetKey(KeyCode.W))
		{
			TouchButtonWalk();
		}
		if (Input.GetKeyUp(KeyCode.W))
		{
			TouchButtonWalkEnd();
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "objPickup" && !_isHandUse)
		{
			_getObjectPickUp = other.gameObject;
			_imagePicjUp.SetActive(true);
		}
		else if (other.tag == "objLower" && _isHandUse)
		{
			_imageLowerObj.SetActive(true);
			_posForLowerObj = other.transform.position;
			_capsuleForLowerObj = other.GetComponent<CapsuleCollider>();
			_getTargetForLowObj = other.gameObject;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "objPickup")
		{
			_imagePicjUp.SetActive(false);
		}
		else if (other.tag == "objLower")
		{
			_imageLowerObj.SetActive(false);
		}
	}

	public void TouchButtonWalkEnd()
	{
		_anim.SetFloat("Speed", 0f);
		_isWalk = false;
		_rb.velocity = Vector3.zero;
	}

	public void TouchButtonWalk()
	{
		if (!_anim.GetBool("isDeath"))
		{
			_anim.SetFloat("Speed", 1f);
			_isWalk = true;
			_walkPersonCor = StartCoroutine(WalkPerson());
		}
	}

	public void PickupObject()
	{
		_isHandUse = true;
		_getObjectPickUp.GetComponent<CapsuleCollider>().enabled = false;
		_getObjectPickUp.transform.position = _pointForPickUp.position;
		_getObjectPickUp.transform.SetParent(_pointForPickUp);
		_imagePicjUp.SetActive(false);
	}

	public void DownObject()
	{
		if (_isHandUse)
		{
			_capsuleForLowerObj.enabled = false;
			_getObjectPickUp.transform.SetParent(null);
			_getObjectPickUp.transform.position = _posForLowerObj;
			_getObjectPickUp.transform.eulerAngles = Vector3.zero;
			_isHandUse = false;
			_imageLowerObj.SetActive(false);
			_getTargetForLowObj.GetComponent<MeshRenderer>().enabled = false;
		}
	}

	private IEnumerator WalkPerson()
	{
		while (_isWalk)
		{
			_worldTransform = base.transform.TransformDirection(Vector3.forward);
			_rb.velocity = _worldTransform * _speedMove * Time.deltaTime;
			yield return null;
		}
	}
}
