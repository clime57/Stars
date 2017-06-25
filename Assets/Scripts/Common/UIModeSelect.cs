using UnityEngine;
using System.Collections;

public class UIModeSelect : MonoBehaviour
{
    int _width = 0;
    int _height = 0;
    void Start()
    {
        _width = Screen.width;
        _height = Screen.height;
        if (Screen.height == 768 && Screen.width == 1024)
        {
            UIRoot uiroot = NGUITools.FindInParents<UIRoot>(this.gameObject);
            uiroot.scalingStyle = UIRoot.Scaling.Flexible;
        }
        else
        {
            UIRoot uiroot = NGUITools.FindInParents<UIRoot>(this.gameObject);
            uiroot.scalingStyle = UIRoot.Scaling.Constrained;
            uiroot.manualHeight = 960;

            if ((float)Screen.width / (float)Screen.height < 1.4f)
            {
                float height = Screen.width / 1280.0f * 960 / Screen.height;
                UICamera.mainCamera.rect = new Rect(0, (1 - height) / 2, 1, height);
            }
			if(Screen.height > 960){
				int h = 960;
				if(Screen.height / 2 >= 960){
					h = Screen.height / 2;
				}
				int width = (int)((float)Screen.width * h / (float)Screen.height);
                string channelID = "";
                channelID = RG_Utils.callAndroidJava<string>("getChannelID");
                if (channelID != null)
                {
                    TyLogger.Log("ChannelID = " + channelID.ToString());
                    Screen.SetResolution(width, h, true);
                    Debug.Log("SetReolution " + width + " " + h);
                    _width = width;
                    _height = h;
                }
			}
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            if (_width != 0 && _height != 0)
            {
                Screen.SetResolution(_width, _height, true);
                Debug.Log("Screen.SetResolution Resume");
            }
        }
    }

    void OnApplicationPause(bool paused)
    {
        if (!paused)
        {
            if (_width != 0 && _height != 0)
            {
                Screen.SetResolution(_width, _height, true);
                Debug.Log("Screen.SetResolution Resume");
            }
        }
    }

}
