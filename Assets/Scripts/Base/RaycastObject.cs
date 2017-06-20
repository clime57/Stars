using UnityEngine;
using System.Collections;

public class RaycastObject : GameSubSystem
{
    Camera _camera;
    int enableCount_ = 0;
    RaycastHit hit;
    Ray ray;
    private static RaycastObject _instance;
    public static RaycastObject GetInstance()
    {
        if (_instance == null)
        {
            _instance = new RaycastObject();
        }
        return _instance;
    }

    public static bool _checkMove = false;   //ÅÐ¶ÏÆÁÄ»ÊÇ·ñÔÚ»¬¶¯

    override public void init()
    {

    }

    override public void update(float time)
    {
        if (!_checkMove)
        {
            _camera = Camera.main;
            if (_camera != null && Input.GetMouseButtonUp(0) && !UIRG_Utils.isMouseOverNGUI())
            {
                ray = _camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    string onClickName = hit.transform.tag;

                    if (onClickName.CompareTo("NPC") == 0)
                    {
                        
                    }
                }
            }
        }
    }
    
}