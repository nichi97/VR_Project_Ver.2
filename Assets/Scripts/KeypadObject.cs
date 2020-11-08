using UnityEngine;
using UnityEngine.UI;

public class KeypadObject : ClickableRaycastObject {

    private GameObject keypadCanvas;

    public override void Start()
    {
        base.Start();

        keypadCanvas = this.transform.Find("KeypadCanvas").gameObject; // transform is used to find inactive child
        keypadCanvas.SetActive(false);
    }

    public override void processSpecialAction()
    {
        faceToPlayer(keypadCanvas, true);
        keypadCanvas.GetComponent<KeypadCanvas>().enableKeypadCanvas();  
    }
}
