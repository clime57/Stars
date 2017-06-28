using UnityEngine;
using System.Collections;
namespace Stars
{
    public class UIPanelParaSetting : MonoBehaviour
    {
        public UIPanel _uiPanel;
        public UIScrollView _uiScrollView;
        const float panelSoftness_ = 6;
        void Awake()
        {
            if (_uiPanel != null && _uiScrollView != null)
            {
                float panelSoftness = panelSoftness_;
                if (_uiScrollView.movement == UIScrollView.Movement.Horizontal)
                {
                    _uiPanel.clipSoftness = new Vector2(panelSoftness, 0);
                }
                else if (_uiScrollView.movement == UIScrollView.Movement.Vertical)
                {
                    _uiPanel.clipSoftness = new Vector2(0, panelSoftness);
                }

            }
            else
            {
                TyLogger.LogError("Can't fine UIPanel or UIScrollView");
            }
            Destroy(this);
        }
    }

}