using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsToggleUpdate : MonoBehaviour {

	public Toggle ToggleButton;
	public Text ToggleText;
	public string ToggleName;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (ToggleButton.isOn == true) {
			ToggleText.text = ToggleName + ": On";
		}
		if (ToggleButton.isOn == false) {
			ToggleText.text = ToggleName + ": Off";
		}
	}
}
