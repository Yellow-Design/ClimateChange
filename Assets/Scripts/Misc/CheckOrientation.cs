using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOrientation : MonoBehaviour
{
    public GameObject VerticalUi;
    public GameObject HorizontalUi;
    static DeviceOrientation orientation;


    // Update is called once per frame
    void Update()
    {
        switch (Input.deviceOrientation)
        {
            case DeviceOrientation.Unknown:            // Ignore
            case DeviceOrientation.FaceUp:            // Ignore
            case DeviceOrientation.FaceDown:        // Ignore
                break;
            default:
                if (orientation != Input.deviceOrientation)
                {
                    orientation = Input.deviceOrientation;
                    if(orientation == DeviceOrientation.LandscapeLeft || orientation == DeviceOrientation.LandscapeRight)
                    {
                        VerticalUi.SetActive(false);
                        HorizontalUi.SetActive(true);
                    }
                    if (orientation == DeviceOrientation.Portrait || orientation == DeviceOrientation.PortraitUpsideDown)
                    {
                        VerticalUi.SetActive(true);
                        HorizontalUi.SetActive(false);
                    }

                }
                break;
        }
    }
}
