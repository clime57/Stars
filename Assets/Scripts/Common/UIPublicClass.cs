using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class UIRG_Utils 
{
    public static float greyRedColor = 0.0f;
    public static Color GreyColor = new Color(0.0f, 1.0f, 1.0f);

    /// <summary>
    /// 设置UIButton是否有效，并且自动设置无效图片为灰色
    /// </summary>
    /// <param name="btn"></param>
    /// <param name="grey"></param>
    /// <param name="enable"></param>
    static public void setButtonEnable(UIButton btn,bool enable)
    {
        UIWidget[] widgets = btn.GetComponentsInChildren<UIWidget>(true);
        float r = 1;
        if (!enable)
        {
            r = greyRedColor;
        }
        Color defaultColor = btn.defaultColor;
        defaultColor.r = r;
        btn.defaultColor = defaultColor;
        btn.hover.r = r;
        btn.pressed.r = r;
        btn.disabledColor.r = r;
        foreach (UIWidget widget in widgets)
        {
            if (widget.GetType() != typeof(UILabel))
            {
                Color col = widget.color;
                col.r = r;
                widget.color = col;
            }
        }
        if (btn.GetComponent<Collider>() != null)
        {
            btn.GetComponent<Collider>().enabled = enable;
        }
    }

    /// <summary>
    /// 判断鼠标是否在有包围盒的UI上
    /// </summary>
    public static bool isMouseOverNGUI()
    {
        Vector3 mousePostion = Input.mousePosition;
        if (UICamera.Raycast(mousePostion))
        {
            if (UICamera.lastHit.collider != null)
                return true;
        }
        return false;
    }
    /// <summary>
    /// 得到UI系统的分辨率
    /// </summary>
    /// <returns></returns>
    public static Vector2 getUIResolution()
    {
        Vector2 newPos = new Vector2();
        newPos.x = Screen.width;
        newPos.y = Screen.height;
        UIRoot.Scaling scalingStyle = UIRoot.Scaling.Constrained;
        if (UIWindowManager.getInstance() != null)
        {
            scalingStyle = UIWindowManager.getInstance().getUIRoot().scalingStyle;
        }
        else
        {
            scalingStyle = NGUITools.FindInParents<UIRoot>(UICamera.mainCamera.gameObject).scalingStyle;
        }

        if (scalingStyle == UIRoot.Scaling.Constrained || scalingStyle == UIRoot.Scaling.ConstrainedOnMobiles)
        {
            float curScreenHeight = Screen.height * UICamera.mainCamera.rect.height;
            newPos.x *= (640.0f / (float)curScreenHeight);
            newPos.y *= (640.0f / (float)curScreenHeight);
        }
        return newPos;
    }

    /// <summary>
    /// 加载初始化界面
    /// </summary>
    public static void loadInitWindow()
    {

    }
    /// <summary>
    /// 销毁初始化界面
    /// </summary>
    public static void destroyInitWindow()
    {

    }
    /// <summary>
    /// 数据表加载完后要加载的必要界面
    /// </summary>
    public static void loadWindows()
    {

    }
    
    /// <summary>
    /// 删除副本需要的界面
    /// </summary>
    public static void destroyWindowInInstance()
    {

    }
}

