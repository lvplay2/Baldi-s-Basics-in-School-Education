using UnityEngine;

public class CleanerArea : MonoBehaviour
{
	public Character cleaner;

	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "Player")
		{
			cleaner.GetComponent<AudioSource>().enabled = true;
			cleaner.isAngry = true;
		}
	}
}
