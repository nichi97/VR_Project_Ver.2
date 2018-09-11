using UnityEngine;
using UnityEngine.UI;

public class RaycastObject : MonoBehaviour {

    //for hintBox
    protected GameObject canvas;
    protected CanvasGroup cg;
    protected bool messageOn;
    public bool haveSeenMsg;
    public float hitTimeLength;

    //for textField
    public GameObject textBox;
    public Text textField;
    public string[] textLines;
    public int currentPage;
    [SerializeField]
    public TextAsset textFile;

    //for raycast
    public void OnRaycastEnter(RaycastHit hitInfo)
    {
        Debug.LogFormat("Raycast entered on {0}", gameObject.name);
		hitTimeLength = 0;
    }
    //for raycast
    public void OnRayCast(RaycastHit hitInfo)
    {   
        Debug.LogFormat("Raycast stayed on {0}", gameObject.name);
		if (canvas.activeInHierarchy) //if the hintbox is disappearing
			TurnOnMessage();
		else {
			hitTimeLength += Time.deltaTime;
			if ((!haveSeenMsg && hitTimeLength >= 2.5)
			   || (haveSeenMsg && hitTimeLength >= 1)) {
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
    //for raycast
    public void OnRaycastExit()
    {
        Debug.LogFormat("Raycast exited on {0}", gameObject.name);
        TurnOffMessage();
    }


    protected void Start()
    {
        Debug.LogFormat("Hello {0}", this.name);

		//for hintBox
		canvas = this.transform.Find("HintCanvas").gameObject;
        cg = canvas.GetComponent<CanvasGroup>();
		messageOn = false;
        haveSeenMsg = false;

		//for textField
		textBox = canvas.transform.Find("Panel").gameObject;//.getGetComponent<GameObject>();
		textField = textBox.transform.Find("Text").gameObject.GetComponent<Text>();
        textLines = textFile.text.Split('@'); //split to each page

        resetAll();
    }

    //helper
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
    }

    public void TurnOffMessage()
    {
        messageOn = false;
    }

    protected void Update()
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
}
