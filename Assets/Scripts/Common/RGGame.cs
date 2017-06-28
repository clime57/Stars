using UnityEngine;
using System.Collections;
using System;
using System.Reflection;
namespace Stars
{
    //游戏功能管理器
    public class RGGame : Game
    {
        override protected void addObjects()
        {
            addObject<UIWindowManager>(true, false);
            addObject<GuideSystem>(true, true);
            addObject<RaycastObject>(true, true);
            addObject<TaskManager>(false, true);
            addObject<RGGameLogic>(true, true);
        }

        public override void update()
        {
            base.update();
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                quitcallback();
            }
        }

        void quitcallback()
        {
            Application.Quit();
        }

    }

}