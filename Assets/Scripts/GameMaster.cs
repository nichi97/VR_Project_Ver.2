using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

    public GameObject readingCanvas;
    
    private int curRoom;

    //gazer related
    private Gazer gazer;
    private MeshRenderer reticle;

    public Vector3[] camPosInRoom = { 
        new Vector3(0, 17, 0),
        new Vector3(60, 17, -10),
        new Vector3(60, 17, 100)
    };

    private const int MAX_ROOM_NUM = 3; 

    // Use this for initialization
    void Start () {
        //gazer
        gazer = GameObject.Find("Main Camera").GetComponent<Gazer>();
        reticle = GameObject.Find("GvrReticlePointer").GetComponent<MeshRenderer>();

        //set initial position
        curRoom = 0;
        this.gameObject.transform.localPosition = camPosInRoom[curRoom];
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void changeRoom(int doorNum)
    {
        if (doorNum >= MAX_ROOM_NUM - 1) return;

        if (curRoom == doorNum) { curRoom = doorNum + 1; }
        else if (curRoom == doorNum + 1) { curRoom = doorNum; }
        else { Debug.LogError("Incorrect changing room behavior " + curRoom + doorNum); } //error

        this.transform.position = camPosInRoom[curRoom];

        if (curRoom == 0)
        {
            
        }
        else if (curRoom == 1)
        {
        }
        else // curRoom == 2
        {
            // game clear
            
        }

    }

    public void enableGazer() { gazer.enabled = true; }
    public void disableGazer() { gazer.enabled = false; }
    public void enableReticle() { reticle.enabled = true; }
    public void disableReticle() { reticle.enabled = false; }
    public ReadingCanvas getReadingCanvas() { return readingCanvas.GetComponent<ReadingCanvas>(); }
    public int getCurRoom() { return curRoom; }

}
