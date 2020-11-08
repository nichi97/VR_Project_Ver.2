using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadingCanvas : MonoBehaviour {

    private GameObject textBox;
    private Text textField; //will be initialized in GameMaster
    private RectTransform rtPanel, rtText;
    private float textHeight;

    private string[] textLines; //will be set in GameMaster
    private int currentPage;
    private bool closing; //indicator of whether the animation should be opening or closing

    private GameMaster gm;

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

        initGMIfNeeded();
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
                disableReadingCanvas();
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
    
    /* Enable and disable can be called before Start/Awake, hence needs to initialize gm on need basis */
    public void enableReadingCanvas(TextAsset readingContent)
    {
        initGMIfNeeded();

        //disable gazer
        gm.disableGazer();
        gm.disableReticle();

        this.gameObject.SetActive(true);

        //set text content of masterCanvas
        setTextContent(readingContent);
    }

    //this function solely is not sufficient to disable IC, also need to reset currentPage in InteractiveCanvas
    public void disableReadingCanvas()
    {
        initGMIfNeeded();

        //enable gazer
        gm.enableGazer();
        gm.enableReticle();

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
