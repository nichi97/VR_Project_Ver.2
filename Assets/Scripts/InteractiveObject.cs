using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : RaycastObject {

    public TextAsset interactiveTextFile;
    private GameObject keypadCanvas;

    public override void  Start()
    {
        base.Start();

        if (this.transform.Find("KeypadCanvas") != null)
        {
            keypadCanvas = this.transform.Find("KeypadCanvas").gameObject;
            keypadCanvas.SetActive(false);
        }
    }

    public override void buttonClicked()
    {
        if (messageOn)
        {
            if (currentPage < textLines.Length - 1)
            {
                currentPage++;
                textField.text = textLines[currentPage];
            }
            else 
            {
                TurnOffMessage();
                resetAll();

                if (interactiveTextFile != null) {
                    gm.enableInteractiveCanvas(interactiveTextFile);
                } else if (keypadCanvas != null) {
                    gm.enableKeypadCanvas(keypadCanvas);
                } else { 
                    Debug.LogError("Make keypad canvas active, or check its checkbox!!!!!");
                }
             }
        }
    }
}
