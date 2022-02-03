using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenResolution : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Camera camera = GetComponent<Camera>();
        Rect rect = camera.rect;
        float scaleHeight = ((float)Screen.width / Screen.height) / ((float)(16 / 9));
        float scaleWidth = 1f / scaleHeight;

        if(scaleHeight < 1)
        {
            rect.height = scaleHeight;
            rect.y = (1f - scaleHeight) / 2f;
        }
        else
        {
            rect.width = scaleWidth;
            rect.x = (1f - scaleWidth);
        }

        camera.rect = rect;
    }

    private void OnPreCull() => GL.Clear(true, true, Color.black);
}
