using UnityEngine;
using UnityEngine.UI;

public class ReadableObject : ClickableRaycastObject {

    public TextAsset readingCanvasContentFile;

    public override void processSpecialAction()
    {
        gm.getReadingCanvas().enableReadingCanvas(readingCanvasContentFile);
    }
}
