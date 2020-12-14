using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public GameObject MainCamera;
    public CanvasScaler canvasScaler;
    public float windowaspect;
    // Start is called before the first frame update
    public void ScaleFullScreenCamera()
    {

            // set the desired aspect ratio (the values in this example are
            // hard-coded for 16:9, but you could make them into public
            // variables instead so you can set them at design time)
            float targetaspect = 16.0f / 9.0f;

            // determine the game window's current aspect ratio
        if(UserInfo.Instance.GameMode != "debug")
        {
          windowaspect = (float.Parse(UserInfo.Instance.widthPx)*float.Parse(UserInfo.Instance.pxRatio)) / (float.Parse(UserInfo.Instance.heightPx)*float.Parse(UserInfo.Instance.pxRatio));
        }
        else
        {
            windowaspect = (float)Screen.width/(float)Screen.height;
        }

            // current viewport height should be scaled by this amount
            float scaleheight = windowaspect / targetaspect;

            // obtain camera component so we can modify its viewport
            Camera camera = MainCamera.GetComponent<Camera>();

            // if scaled height is less than current height, add letterbox
            if (scaleheight < 1.0f)
            {  
                Rect rect = camera.rect;

                rect.width = 1.0f;
                rect.height = scaleheight;
                rect.x = 0;
                rect.y = (1.0f - scaleheight) / 2.0f;
                
                camera.rect = rect;

                //and scale UI to match width of reference
                canvasScaler.matchWidthOrHeight = 0f;
            }
            else // add pillarbox
            {
                float scalewidth = 1.0f / scaleheight;

                Rect rect = camera.rect;

                rect.width = scalewidth;
                rect.height = 1.0f;
                rect.x = (1.0f - scalewidth) / 2.0f;
                rect.y = 0;

                camera.rect = rect;

                //and scale UI to match height of reference
                canvasScaler.matchWidthOrHeight = 1f;
            }
    }

    public void ExitFullScreenCamera()
    {
        //if(UserInfo.Instance.GameMode != "debug")
        //{
        // obtain camera component so we can modify its viewport
            Camera camera = MainCamera.GetComponent<Camera>();

                Rect rect = camera.rect;

                rect.width = 1.0f;
                rect.height = 1.0f;
                rect.x = 0;
                rect.y = 0;;
                
                camera.rect = rect;

                canvasScaler.matchWidthOrHeight = 0.5f;
        //}
    }
}
