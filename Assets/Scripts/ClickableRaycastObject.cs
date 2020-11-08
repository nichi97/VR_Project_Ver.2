using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ClickableRaycastObject : RaycastObject {

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

                processSpecialAction();
            }
        }
    }

    public abstract void processSpecialAction();
}
