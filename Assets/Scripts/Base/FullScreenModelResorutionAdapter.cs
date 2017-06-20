using UnityEngine;
using System.Collections;

public class FullScreenModelResorutionAdapter : MonoBehaviour
{
    public Vector3 _scaleof_960_640;
    public Vector3 _scaleof_1136_640;
    public Vector3 _scaleof_1024_768;

    void Start()
    {
        setScale();
    }


    public void setScale()
    {
        if (UIWindowManager.getInstance() != null)
        {
            if (UIWindowManager.getInstance().getUIRoot().scalingStyle == UIRoot.Scaling.Constrained ||
                UIWindowManager.getInstance().getUIRoot().scalingStyle == UIRoot.Scaling.ConstrainedOnMobiles)
            {
                Vector3 newVec;
                float myRatio = (float)Screen.width / (float)Screen.height;
                float maxRatio = 1708.0f / 960.0f;
                float minRatio = 1280.0f / 960.0f;
                newVec.x = Mathf.Lerp(_scaleof_960_640.x, _scaleof_1136_640.x, (myRatio - minRatio) / (maxRatio - minRatio));
                newVec.y = _scaleof_960_640.y;
                newVec.z = _scaleof_960_640.z;
                transform.localScale = newVec;
            }
            else if ((float)Screen.width / (float)Screen.height < 1.4f)
            {
                transform.localScale = _scaleof_1024_768;
            }
        }
    }
}
