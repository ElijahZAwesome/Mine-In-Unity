using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsTextSetting : MonoBehaviour {

	public Text TextObj;
	public string ObjName;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		TextObj.text = ObjName + ": " + Mathf.RoundToInt(GetComponent<Slider> ().value).ToString();
	}
}
