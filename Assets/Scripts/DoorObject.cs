using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorObject : ClickableRaycastObject {
    public int doorNum; // e.g. door#0 connects room#0 & room#1 

    //public AudioSource lockedSound;
    public AudioSource audioUnlock;
    public AudioSource audioOpen;

    private GameObject doorWing;

    private bool locked;
    private bool opened;

    /* parameters */
    private const float OPENED_ANGLE = 30.0f;
    private const float OPENING_DECELERATION = -2.0f;
    private float openSpeed = 10.0f;


    public override void Start() {
        base.Start();

        if (audioUnlock == null || audioOpen == null) { Debug.LogError("Door audio source is not assigned!!"); }

        doorWing = this.transform.Find("Door/doorWing").gameObject;

        locked = true;
        opened = false;
    }

    public override void Update() {
        base.Update();

        // update door opening animation if unlocked 
        if (!locked && !opened) {
            if (doorWing.transform.localEulerAngles.y - OPENED_ANGLE < 0 && openSpeed > 0) {
                doorWing.transform.Rotate(0.0f, Time.deltaTime * openSpeed, 0.0f, Space.Self);
                openSpeed += Time.deltaTime * OPENING_DECELERATION;
            }
            else {
                //doorWing.transform.eulerAngles = to;
                opened = true;
            }
        }
    }

    /* Inheritance */

    public override void processSpecialAction() {
        passDoor();
    }

    /* Door-specific methods */

    public void passDoor() {
        if (!locked) {
            audioOpen.Play();
            gm.changeRoom(doorNum);
        }
        else {
            //audioSource.PlayOneShot(lockedSound); // TODO tobetested
        }
    }

    public void unlockDoor() {
        // TODO play sound
        //asUnlock.Play();
        locked = false;
    }

    public bool isLocked() { return locked; }
}
