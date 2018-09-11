using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : RaycastObject {

    public TextAsset interactiveTextFile;

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
                

                GameObject.Find("Player").GetComponent<GameMaster>().enableInteractiveCanvas(interactiveTextFile);
               
            }
        }
    }
}
