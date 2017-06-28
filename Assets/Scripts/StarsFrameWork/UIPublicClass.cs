using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Stars
{
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
        static public void setButtonEnable(UIButton btn, bool enable)
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
    }

}