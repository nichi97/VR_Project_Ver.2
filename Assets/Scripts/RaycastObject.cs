﻿using System;
using Unity.UNetWeaver;
using UnityEngine;
using UnityEngine.UI;

public class RaycastObject : MonoBehaviour {

    // HintBox related
    protected GameObject canvas;
    protected CanvasGroup cg;
    protected bool messageOn;
    private bool haveSeenMsg;
    private float hitTimeLength;
    private int lastSeenRoom; // used for faceToPlayer, stores which room was the player watching the canvas at last time

    // TextField related
    public TextAsset popUpTextFile;
    protected Text textField;
    protected string[] textLines;
    protected int currentPage;


    // Constants
    private const double GAZING_FIRST_TIME_DURATION = 2.0; 
    private const double GAZING_AGAIN_DURATION = 1.0; 

    protected GameMaster gm;

    public virtual void Start()
    {
        gm = GameObject.Find("Player").GetComponent<GameMaster>();

		//for hintBox
		canvas = this.transform.Find("HintCanvas").gameObject;
        cg = canvas.GetComponent<CanvasGroup>();
		messageOn = false;
        haveSeenMsg = false;
        lastSeenRoom = -1;

         //for textField
         GameObject textBox = canvas.transform.Find("Panel").gameObject;//.getGetComponent<GameObject>();
		textField = textBox.transform.Find("Text").gameObject.GetComponent<Text>();
        textLines = popUpTextFile.text.Split('@'); //split to each page

        resetAll();
    }
    
    public virtual void Update()
    {
        //update nothing if the hintbox is not shown
        if (!canvas.activeInHierarchy) 
            return;

        //otherwise, if hintbox is shown
        if (messageOn && cg.alpha < 1) //appearing
            cg.alpha += Time.deltaTime * 1.5f;
        else if (!messageOn && cg.alpha > 0) //disappearing
        {
            if (cg.alpha > 0.9)
                cg.alpha -= Time.deltaTime * 0.075f;
            else
                cg.alpha -= Time.deltaTime * 1.5f;
        }

        if (cg.alpha <= 0) //disappeared
        {
            resetAll();
        }
    }

    /* Raycast related */

    public void OnRaycastEnter(RaycastHit hitInfo)
    {
		hitTimeLength = 0;
    }
    
    public void OnRayCast(RaycastHit hitInfo)
    {   
		if (canvas.activeInHierarchy) //if the hintbox is disappearing
			TurnOnMessage();
		else {
			hitTimeLength += Time.deltaTime;
			if ((!haveSeenMsg && hitTimeLength >= GAZING_FIRST_TIME_DURATION)
			   || (haveSeenMsg && hitTimeLength >= GAZING_AGAIN_DURATION)) {
				/*		if (this is InteractiveRaycastObject
				&& ((InteractiveRaycastObject)this).getInInteraction()) 
			{
				isGazing = false;      
			}
			else
	*/
				TurnOnMessage();
			}
		}
    }

    public void OnRaycastExit()
    {
        TurnOffMessage();
    }

    /* End of raycast related */

    /* Helpers */

    public void resetAll()
    {
        //reset text
        currentPage = 0;
        textField.text = textLines[0]; //set hintbox's text to first page

        //reset hintbox
        cg.alpha = 0f;
        hitTimeLength = 0;

        canvas.SetActive(false);
        Debug.LogFormat("canvas is really set to disable" + canvas.activeInHierarchy);
    }

    public void TurnOnMessage()
    {
        messageOn = true;
        haveSeenMsg = true;
		canvas.SetActive(true);

        faceToPlayer(canvas);
    }

    public void TurnOffMessage()
    {
        messageOn = false;
    }

    public virtual void buttonClicked()
    {
        if (messageOn)
        {
            if (currentPage < textLines.Length - 1)
            {
                currentPage++;
                textField.text = textLines[currentPage];
            }
            else //reached to the last page and nothing left
            {//TODOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO   need to decide whether go back to the first page or stay at the last page, also  whether resetAll should set the page back to the first page or not (depending on whether the first page's info is important or not)
                textField.text = textLines[0];
                currentPage = 0;
            }
        }
    }

    // bypass should be set only when it is guaranteed that getCurRoom() is equal to lastSeenRoom
    //      so that it won't break the normal usage of the method
    // bypass's usage is e.g. KeypadObject can bypass the first if condition to make the keypadCanvas 
    //      face to the camera along with the keypadObject
    protected void faceToPlayer(GameObject canvas, bool bypass = false) {
        if (lastSeenRoom == gm.getCurRoom() && !bypass) { return; }

        Vector3 from = canvas.transform.position - this.transform.position;
        from.y = 0;
        Vector3 to = gm.transform.position - this.transform.position;
        to.y = 0;
        float delta = Vector3.SignedAngle(from, to, Vector3.up);
        canvas.transform.RotateAround(this.transform.position, Vector3.up, delta);

        lastSeenRoom = gm.getCurRoom();
    }
}
