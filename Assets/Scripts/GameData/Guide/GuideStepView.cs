using UnityEngine;
using System.Collections;
namespace Stars
{
    public class GuideStepView
    {
        virtual public void begin(string para)
        {

        }

        virtual public bool update()
        {
            return false;
        }

        virtual public void end()
        {

        }

        virtual public void showArrow(string text)
        {

        }

        virtual public bool processEvent(string triggerType, string para)
        {
            return false;
        }
    }

}