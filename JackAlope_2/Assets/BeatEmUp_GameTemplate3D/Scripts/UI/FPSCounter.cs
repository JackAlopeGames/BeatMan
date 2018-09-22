using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class FPSCounter : MonoBehaviour {

   
	public float frequency = 0.5f;
	public int FramesPerSec { get; protected set; }
	private Text text;
    private GameObject FpsManager;
	private void OnEnable() {
        try
        {
            this.FpsManager = GameObject.FindGameObjectWithTag("FpsManager");
            if (!this.FpsManager.GetComponent<SetTargetFps>().visible)
            {
                this.gameObject.SetActive(false);
            }
        }
        catch { }
		text = GetComponent<Text>();
		text.enabled = true;
        try
        {
            if (this.FpsManager.GetComponent<SetTargetFps>().visible)
            {
                StartCoroutine(FPS());
            }
        }
        catch { }
       
    }

	private IEnumerator FPS() {
		for(;;){
			// Capture frame-per-second
			int lastFrameCount = Time.frameCount;
			float lastTime = Time.realtimeSinceStartup;
			yield return new WaitForSeconds(frequency);
			float timeSpan = Time.realtimeSinceStartup - lastTime;
			int frameCount = Time.frameCount - lastFrameCount;

			// Display
			FramesPerSec = Mathf.RoundToInt(frameCount / timeSpan);
			text.text = "FPS: " + FramesPerSec.ToString();
		}
	}

    public void HideFps()
    {
        this.GetComponent<Text>().enabled = false;
    }
    public void ShowFPS()
    {
        this.GetComponent<Text>().enabled = true;
    }
}