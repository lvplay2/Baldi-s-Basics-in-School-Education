using UnityEngine;

public class BsodaBullet : MonoBehaviour
{
	public float speed = 2f;

	private void Start()
	{
		Object.Destroy(base.gameObject, 6f);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Enemy"))
		{
			other.transform.parent.GetComponent<Character>().isAngry = false;
			other.transform.parent.GetComponent<Character>().MakeHappy();
			if ((bool)other.transform.parent.GetComponent<Girl>())
			{
				other.transform.parent.GetComponent<Girl>().isCaught = true;
				other.transform.parent.GetComponent<Girl>().RestTimeStart();
			}
			Object.Destroy(base.gameObject);
		}
	}

	private void Update()
	{
		Vector3 vector = new Vector3(base.transform.worldToLocalMatrix.MultiplyVector(base.transform.forward).x, 0f, base.transform.worldToLocalMatrix.MultiplyVector(base.transform.forward).z);
		base.transform.Translate(vector * speed * Time.deltaTime, Space.Self);
	}
}
