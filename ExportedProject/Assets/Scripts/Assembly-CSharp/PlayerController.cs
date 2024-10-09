using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
	public enum enInputType
	{
		PC = 0,
		MOBILE = 1
	}

	public float speedWalk = 3f;

	public float runSpeed;

	public float speedRotateJoystick = 2f;

	public float speedRotateSwipe = 0.8f;

	public float gravity = -9.8f;

	public float speedSave;

	public JoystickPlayerController joystickController;

	public enInputType inputType;

	[HideInInspector]
	public bool isDead;

	private CharacterController characterController;

	private Transform cameraTransform;

	private Vector3 motion;

	private Vector3 previousPosition;

	public bool isUnderWater { get; private set; }

	public bool isBaseTrigger { get; private set; }

	public Vector3 GetPubMotion()
	{
		return GetMotion();
	}

	private void Awake()
	{
		inputType = enInputType.MOBILE;
	}

	private void Start()
	{
		speedSave = speedWalk;
		characterController = GetComponent<CharacterController>();
		cameraTransform = base.transform;
		if (inputType != enInputType.MOBILE)
		{
			HideJoystick();
		}
		previousPosition = base.transform.position;
		speedRotateSwipe = PlayerPrefs.GetFloat(SaveManager.GetKeyProject() + "CameraSensa", 0.8f);
	}

	private void FixedUpdate()
	{
		if (!isDead)
		{
			if (isUnderWater)
			{
				motion = GetMotion();
				characterController.Move(base.transform.TransformDirection(motion) * Time.fixedDeltaTime);
			}
			else
			{
				motion = GetMotion();
				motion = base.transform.TransformDirection(motion);
				motion.y = gravity;
				motion *= Time.fixedDeltaTime;
				characterController.Move(motion);
			}
			if (Vector3.Distance(previousPosition, base.transform.position) > 2f && !isDead)
			{
				base.transform.position = previousPosition;
			}
			previousPosition = base.transform.position;
		}
	}

	private Vector3 GetMotion()
	{
		if (inputType == enInputType.PC)
		{
			return new Vector3(Input.GetAxis("Horizontal") * speedWalk, 0f, Input.GetAxis("Vertical") * speedWalk);
		}
		return new Vector3(joystickController.Horizontal() * speedWalk, 0f, joystickController.Vertical() * speedWalk);
	}

	public void TeleportToPoint(Vector3 _point)
	{
		base.transform.position = _point;
		previousPosition = _point;
		Invoke("SetIsDeath", 0.3f);
	}

	private void SetIsDeath()
	{
		isDead = false;
	}

	public void CameraRotate(Vector2 _angle)
	{
		if (!isDead)
		{
			base.transform.Rotate(Vector3.up * _angle.x * speedRotateSwipe, Space.World);
			cameraTransform.Rotate(Vector3.left * _angle.y * speedRotateSwipe);
			if (cameraTransform.eulerAngles.x > 60f && cameraTransform.eulerAngles.x < 180f)
			{
				cameraTransform.localEulerAngles = new Vector3(60f, 0f, 0f);
			}
			if (cameraTransform.eulerAngles.x < 300f && cameraTransform.eulerAngles.x > 180f)
			{
				cameraTransform.localEulerAngles = new Vector3(300f, 0f, 0f);
			}
		}
	}

	private void HideJoystick()
	{
		if (joystickController != null)
		{
			joystickController.gameObject.SetActive(false);
		}
	}

	public void SetIsUnderWater(bool _value)
	{
		if (!isBaseTrigger)
		{
			isUnderWater = _value;
		}
	}

	public void SetIsBaseTrigger(bool _value)
	{
		isBaseTrigger = _value;
		if (isBaseTrigger)
		{
			isUnderWater = false;
		}
		else
		{
			isUnderWater = true;
		}
	}
}
