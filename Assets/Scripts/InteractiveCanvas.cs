using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveCanvas : MonoBehaviour {

    public GameObject textBox;
    public Text textField; //will be initialized in GameMaster
    public string[] textLines; //will be set in GameMaster
    public int currentPage;
    public bool closing; //indicator of whether the animation should be opening or closing

    private RectTransform rtPanel, rtText;
    private float textHeight;

    //game master
    public GameMaster gm;

    // Use this for initialization
    void Start()
    {
        gm = GameObject.Find("Player").GetComponent<GameMaster>();
    }

    //Awake is called before any Start()
    void Awake() {
        //squeeze the panel for the animation
        rtPanel = this.transform.Find("Panel").GetComponent<RectTransform>();
        rtPanel.offsetMin = new Vector2(rtPanel.offsetMin.x, 150); //new Vector2(left, bottom)
        rtPanel.offsetMax = new Vector2(rtPanel.offsetMax.x, -150); //new Vector2(-right, -top)
        //squeeze the text for the animation
        rtText = rtPanel.transform.Find("Text").GetComponent<RectTransform>();
        textHeight = rtText.sizeDelta.y;
        rtText.sizeDelta = new Vector2(rtText.sizeDelta.x, 0); //new Vector2(width, height)

        //initialization
        closing = false;
        currentPage = -1;

        //for textfield
        textBox = this.transform.Find("Panel").gameObject;
        textField = textBox.transform.Find("Text").gameObject.GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!closing) //opening
        {
            if (rtPanel.offsetMin.y > 50)
            {
                rtPanel.offsetMin = new Vector2(rtPanel.offsetMin.x, rtPanel.offsetMin.y - 10);
                rtPanel.offsetMax = new Vector2(rtPanel.offsetMax.x, rtPanel.offsetMax.y + 10);

                rtText.sizeDelta = new Vector2(rtText.sizeDelta.x, rtText.sizeDelta.y + textHeight * 0.1f); 
            }
        }
        else //closing
        {
            if (rtPanel.offsetMin.y < 150)
            {
                rtPanel.offsetMin = new Vector2(rtPanel.offsetMin.x, rtPanel.offsetMin.y + 20);
                rtPanel.offsetMax = new Vector2(rtPanel.offsetMax.x, rtPanel.offsetMax.y - 20);

                rtText.sizeDelta = new Vector2(rtText.sizeDelta.x, rtText.sizeDelta.y - textHeight * 0.2f);
            }
            else
            {
                closing = false; //no need to close anymore
                gm.disableInteractiveCanvas();
            }
        }
    }

    public void setTextContent(TextAsset interactiveCanvasContent)
    {
        textLines = interactiveCanvasContent.text.Split('@');
        textField.text = textLines[0];
        currentPage++; //equivalent to currentPage = 0
    }

    public void buttonClicked()
    {
        //if text content hasn't been set yet
        if (currentPage < 0)
            return;

        if (currentPage < textLines.Length - 1)
        {
            GetComponent<AudioSource>().Play();
            currentPage++;
            textField.text = textLines[currentPage];
        }
        else
        {
            //reset everything related to textfield as this InteractiveCanvas will be disabled
            currentPage = -1;

            //close IC
            closing = true;
        }
       
    }
}
