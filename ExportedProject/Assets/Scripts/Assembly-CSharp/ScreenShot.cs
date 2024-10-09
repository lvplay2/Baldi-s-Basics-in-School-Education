using UnityEngine;

public class ScreenShot : MonoBehaviour
{
	private int i;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			ScreenCapture.CaptureScreenshot("screenshot" + i + ".png", 2);
			i++;
		}
	}
}
