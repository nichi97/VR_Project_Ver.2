using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hint : MonoBehaviour {

	public GameObject textBox;
	public Text textField;

	[SerializeField]
	public TextAsset textFile;

	// Use this for initialization
	void Start () {
		textBox = this.transform.Find("Panel").gameObject;//.getGetComponent<GameObject>();
		textField = textBox.transform.Find("Text").gameObject.GetComponent<Text>();
		textField.text = textFile.text;
	}
	
	// Update is called once per frame
	void Update () {
		//if (GvrEditorEmulator.Instance.Triggered)
		//	textBox.SetActive(false);
	}
}
