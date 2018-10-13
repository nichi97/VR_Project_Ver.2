using UnityEngine;

public class Gazer : MonoBehaviour {

    private RaycastObject lastRaycastObject;
    private RaycastObject lastRaycastObjectForFinger;
    float hitTimeLength;
    bool isGazing;
    bool fingerMode;
    string inputPw;
    int inputCount;
    bool locked;
    GameObject cursorInstance;

	//new
	bool enable;

    // Use this for initialization
    void Start()
    {
        hitTimeLength = 0;
        isGazing = true;
        fingerMode = false; //finger raycasting mode
        inputPw = "";
        inputCount = 0;
        locked = true;
        
		//new
		enable = true;
    }

    // Update is called once per frame
    void Update()
    {
		/*

        //when solving the keypad
        if (locked && !isGazing && lastRaycastObject is KeypadObject)
        {
            if (!fingerMode)
            {
                fingerMode = true;
                ((KeypadObject)lastRaycastObject).setPwMode(true);
                //instantiate cursor for finger pointer
                cursorInstance = Instantiate(Resources.Load("cursor")) as GameObject;
                cursorInstance.SetActive(false);//cursorInstance.GetComponent<Renderer>().enabled = false;
            }
            if (inputPw.CompareTo("662") == 0)
            {
                locked = false;
                ((KeypadObject)lastRaycastObject).setLocked(false);
            }
            else if (inputPw.Length > 2)
                inputPw = "";
        }
        
        if (!locked)
        {
            isGazing = true;
            fingerMode = false;
            inputPw = "";
            inputCount = 0;
        }

        //when the gazed object is interactive raycast object
        if (!isGazing && lastRaycastObject is InteractiveRaycastObject)
        {
            //Debug.LogFormat("hi");
            //lastRaycastObject.TurnOffMessage();

            //reset back to the gazing mode with no interaction
            if (!((InteractiveRaycastObject)lastRaycastObject).getInInteraction())
            {
                isGazing = true;
                fingerMode = false;
                inputPw = "";
                inputCount = 0;
            }
            else
            {
                if (fingerMode && !OVRInput.Get(OVRInput.NearTouch.SecondaryIndexTrigger))
                    fingerRaycast();
                return;
            }
        }

		*/


        //Debug.LogFormat("checkpoint");
        Ray myRay = new Ray(this.transform.position, this.transform.forward);
        Debug.DrawRay(transform.position, transform.forward * 1000.0f);
        RaycastHit hitObject;
        if (Physics.Raycast(myRay, out hitObject, Mathf.Infinity))
        {
            //Debug.LogFormat("checkpoint");
			RaycastObject rayCastHitObject = hitObject.collider.GetComponentInParent<RaycastObject>();
            if (rayCastHitObject != null)
            {
                if (rayCastHitObject != lastRaycastObject)
                {
                    if (lastRaycastObject != null)
                        lastRaycastObject.OnRaycastExit();
                    rayCastHitObject.OnRaycastEnter(hitObject);
                    lastRaycastObject = rayCastHitObject;
                }
                else
                {
                    rayCastHitObject.OnRayCast(hitObject);
                }
            }

            else if (lastRaycastObject != null)
            {
                lastRaycastObject.OnRaycastExit();
                lastRaycastObject = null;
            }
        }
        else if (lastRaycastObject != null)
        {
            lastRaycastObject.OnRaycastExit();
            lastRaycastObject = null;
        }
        //Debug.LogFormat("{0}", hitObject);

    }


    private void fingerRaycast()
    {/*
        Debug.LogFormat("finger time");

        OvrAvatar avatar = GameObject.Find("LocalAvatar").GetComponent<OvrAvatar>();//  this.gameObjectGetComponent<OvrAvatar>();
        //OvrAvatar avatar = FindObjectOfType(OvrAvatar);
        Debug.LogFormat("avatar is {0}", avatar.name);
        Ray myRay = new Ray(avatar.GetHandTransform(OvrAvatar.HandType.Right, OvrAvatar.HandJoint.IndexTip).transform.position, this.transform.forward);
        //Debug.DrawRay(myRay.origin, myRay.direction * 1000.0f);
        //Ray myRay = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hitObject;
        if (Physics.Raycast(myRay, out hitObject, Mathf.Infinity))
        {
            //cursor
            cursorInstance.SetActive(true);//cursorInstance.GetComponent<Renderer>().enabled = true;
            cursorInstance.transform.position = new Vector3(hitObject.point.x + 10f, hitObject.point.y, hitObject.point.z);
            cursorInstance.transform.rotation = hitObject.transform.rotation;//.FromToRotation(Vector3.up, hitObject.normal);

            NumberKey rayCastHitObject = hitObject.collider.GetComponent<NumberKey>();
            if (rayCastHitObject != null
                && OVRInput.Get(OVRInput.Button.One)
                || OVRInput.Get(OVRInput.Button.Two))
            {
                switch (rayCastHitObject.getName())
                {
                    case "Button 1":
                        inputPw += "1";
                        break;
                    case "Button 2":
                        inputPw += "2";
                        break;
                    case "Button 3":
                        inputPw += "3";
                        break;
                    case "Button 4":
                        inputPw += "4";
                        break;
                    case "Button 5":
                        inputPw += "5";
                        break;
                    case "Button 6":
                        inputPw += "6";
                        break;
                    case "Button 7":
                        inputPw += "7";
                        break;
                    case "Button 8":
                        inputPw += "8";
                        break;
                    case "Button 9":
                        inputPw += "9";
                        break;
                    case "Button 0":
                        inputPw += "0";
                        break;
                    case "Button Red":
                        inputPw = "";
                        break;
                }
            }
        }
        else
        {
            cursorInstance.SetActive(false);//cursorInstance.GetComponent<Renderer>().enabled = false;

        }

	*/



            /*
            if (rayCastHitObject != null)
            {
                if (rayCastHitObject != lastRaycastObjectForFinger)
                {
                    if (lastRaycastObjectForFinger != null)
                        lastRaycastObjectForFinger.OnRaycastExit();
                    rayCastHitObject.OnRaycastEnter(hitObject);
                    lastRaycastObjectForFinger = rayCastHitObject;
                    hitTimeLength = 0;
                    rayCastHitObject.TurnOffMessage();
                }
                else
                {
                    rayCastHitObject.OnRayCast(hitObject);
                    hitTimeLength += Time.deltaTime;

                    //show internal mind
                    if ((!rayCastHitObject.haveSeenMsg && hitTimeLength >= 3)
                            || (rayCastHitObject.haveSeenMsg && hitTimeLength >= 1))
                    {
                        if (rayCastHitObject is InteractiveRaycastObject
                            && ((InteractiveRaycastObject)rayCastHitObject).getInInteraction())
                        {
                            isGazing = false;
                        }
                        else
                            rayCastHitObject.TurnOnMessage();
                    }
                }
            }

            else if (lastRaycastObjectForFinger != null)
            {
                lastRaycastObjectForFinger.OnRaycastExit();
                lastRaycastObjectForFinger = null;
                hitTimeLength = 0;
            }
        }
        else if (lastRaycastObjectForFinger != null)
        {
            lastRaycastObjectForFinger.OnRaycastExit();
            lastRaycastObjectForFinger = null;
            hitTimeLength = 0;
        }*/
    }

}
