using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadCanvas : MonoBehaviour {

    public string ANSWER;
    private string input;

    //game master
    public GameMaster gm;

    // Use this before initialization
    void Awake() {
        input = "";
        gm = GameObject.Find("Player").GetComponent<GameMaster>();
    }
	
	// Update is called once per frame
	void Update() {
		
	}

    public void enterPasscode(char number)
    {
        if (number == 'c')
        {
            input = "";
        }
        else if (number == 'x')
        {
            closeKeypad();
        }
        else
        {
            if (input.Length == 3)
                input = "";
            input += number;

            if (input == ANSWER)
            {
                Debug.LogError("Correct answer");
                closeKeypad();
                gm.changeRoom();
            }
            else if (input.Length == 3)
            {
                GetComponent<AudioSource>().Play();
            }
        }
    }

    private void closeKeypad()
    {
        input = "";
        gm.disableKeypadCanvas(this.transform.gameObject);
    }
}
