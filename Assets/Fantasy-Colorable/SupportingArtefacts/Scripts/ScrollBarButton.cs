using UnityEngine;
using System.Collections;

/// <summary>
/// Scroll bar button lets users tap a button to move their scroll bars.
/// </summary>
public class ScrollBarButton : MonoBehaviour {
	
	public UIScrollBar scrollBar;
	public float amount = 0.1f;
	public float acceleration = 0.0f;
	
	private float currentAmount = 0.0f;
	
	public void OnPress(bool pressed) {
		if (pressed) {
			StartCoroutine("Scroll");	
		}else {
			StopCoroutine("Scroll");	
		}
	}
	
	private IEnumerator Scroll() {
		currentAmount = 0.0f;
		while (true) {
			currentAmount += amount * acceleration;
			scrollBar.scrollValue += currentAmount * Time.deltaTime;
			yield return 0;
		}
	}
}
