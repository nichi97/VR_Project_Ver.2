using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

    public GameObject interactiveCanvas;

    //gazer related
    public Gazer gazer;
    public MeshRenderer reticle;

    // Use this for initialization
    void Start () {
        //gazer
        gazer = GameObject.Find("Main Camera").GetComponent<Gazer>();
        reticle = GameObject.Find("GvrReticlePointer").GetComponent<MeshRenderer>();

        if (GameObject.Find("InteractiveCanvas") == null)
            Debug.LogError("Make interactive canvas active, or check its checkbox!!!!!");
        interactiveCanvas = GameObject.Find("InteractiveCanvas").gameObject;
        disableInteractiveCanvas();
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
}
