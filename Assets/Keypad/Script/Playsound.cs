using UnityEngine;
using System.Collections;

public class Playsound : MonoBehaviour 
{
    public char number;

	public void Clicky (){
		GetComponent<AudioSource>().Play();
        this.transform.parent.parent.Find("KeypadCanvas").GetComponent<KeypadCanvas>().enterPasscode(number);
	}


}
