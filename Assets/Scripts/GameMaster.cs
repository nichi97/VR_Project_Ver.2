using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

    public GameObject interactiveCanvas;
    
    private int currentRoom;

    //gazer related
    private Gazer gazer;
    private MeshRenderer reticle;

    // Use this for initialization
    void Start () {
        //gazer
        gazer = GameObject.Find("Main Camera").GetComponent<Gazer>();
        reticle = GameObject.Find("GvrReticlePointer").GetComponent<MeshRenderer>();

        //if (GameObject.Find("InteractiveCanvas") == null)
        if (interactiveCanvas == null)
            Debug.LogError("Make interactive canvas active, or check its checkbox!!!!!");
        //interactiveCanvas = GameObject.Find("InteractiveCanvas").gameObject;
        disableInteractiveCanvas();

        currentRoom = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void enableInteractiveCanvas(TextAsset interactiveCanvasContent)
    {
        //disable gazer
        gazer.enabled = false;
        reticle.enabled = false;

        interactiveCanvas.SetActive(true);

        //set text content of interactiveCanvas
        InteractiveCanvas ic = interactiveCanvas.GetComponent<InteractiveCanvas>();
        ic.setTextContent(interactiveCanvasContent);
    }

    //this function solely is not sufficient to disable IC, also need to reset currentPage in InteractiveCanvas
    public void disableInteractiveCanvas()
    {
        //enable gazer
        gazer.enabled = true;
        reticle.enabled = true;

        interactiveCanvas.SetActive(false);
    }

    public void enableKeypadCanvas(GameObject keypadCanvas)
    {
        //disable gazer only (excluding reticle)
        gazer.enabled = false;

        keypadCanvas.SetActive(true);
    }

    public void disableKeypadCanvas(GameObject keypadCanvas)
    {
        //enable gazer
        gazer.enabled = true;

        keypadCanvas.SetActive(false);
    }

    public void changeRoom()
    {
        GetComponent<AudioSource>().Play();

        currentRoom++;
        if (currentRoom == 1)
        {
            this.transform.position = new Vector3(10, 11, 0); //Vector3(x, ,y, z)
        }

    }

}
