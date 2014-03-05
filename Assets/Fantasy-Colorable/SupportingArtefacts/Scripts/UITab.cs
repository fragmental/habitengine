using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Simple tabbed frame implementation.
/// </summary>
public class UITab : MonoBehaviour {
	
	public UISprite tabSprite;
	public bool isActive = false;
	public GameObject contentPanel;
	public int foregroundDepth;
	public int backgroundDepth;
	public string selectedSpriteName;
	public string deselectedSpriteName;
	
	private int myPosition;
	private List<UITab> tabSprites;
	
	void Start() {
		tabSprites = gameObject.transform.parent.gameObject.GetComponentsInChildren(typeof(UITab)).Select(t=>(UITab)t).ToList ();
		for (int i = 0; i < tabSprites.Count; i++) {
			if (tabSprites[i].gameObject == gameObject) {
				myPosition = i;
				break;
			}
		}
	}
	
	public void OnClick() {
		for (int i = 0; i < tabSprites.Count; i++) {
			if (i == myPosition) {
				tabSprites[i].contentPanel.SetActive(true);
				tabSprites[i].tabSprite.depth = foregroundDepth;
				if (selectedSpriteName.Length > 0) tabSprites[i].tabSprite.spriteName = selectedSpriteName;
			} else {
				tabSprites[i].contentPanel.SetActive(false);
				tabSprites[i].tabSprite.depth = backgroundDepth;
				if (deselectedSpriteName.Length > 0) tabSprites[i].tabSprite.spriteName = deselectedSpriteName;
			}
		}
	}
	
}
