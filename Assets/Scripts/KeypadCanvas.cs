using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadCanvas : MonoBehaviour {

    public string ANSWER;
    public DoorObject door;
    
    private string input;
    private GameMaster gm;
    private AudioSource audioSource;

    // Use this before initialization
    void Awake() {
        input = "";

        initGMIfNeeded();
        audioSource = GetComponent<AudioSource>();
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
                Debug.Log("Correct answer");
                closeKeypad();

                if (door.isLocked()) {
                    door.unlockDoor();
                    door.passDoor(); // TODO for test
                    //gm.changeRoom(); TODO make the door an interactive object
                }
            }
            else if (input.Length == 3)
            {
                audioSource.Play(); // playing the audio for wrong answer
            }
        }
    }

    private void closeKeypad()
    {
        input = "";
        disableKeypadCanvas();
    }

    /* Enable and disable can be called before Start/Awake, hence needs to initialize gm on need basis */
    public void enableKeypadCanvas()
    {
        initGMIfNeeded();
        gm.disableGazer();
        this.gameObject.SetActive(true);
    }

    public void disableKeypadCanvas()
    {
        initGMIfNeeded();
        gm.enableGazer();
        this.gameObject.SetActive(false);
    }
    private void initGMIfNeeded()
    {
        if (gm == null)
        {
            gm = GameObject.Find("Player").GetComponent<GameMaster>();
        }
    }
}
