using UnityEngine;
using System.Collections;
using System;
using System.Reflection;
namespace Stars
{
    public class GuideStep
    {
        public uint _id;
        public string _guideShowName;
        public GuideStepView _guideView = null;
        public string _step_para;
        public string _end_trigger_type;
        public string _end_trigger_para;
        public int _begin_reset = 0;
        public int _end_reset = 0;
        public string _guideArrowPos = "";

        public string _arrow_tip_text = "请点击";
        bool _isActive = true;
        virtual public void begin()
        {
            _guideView = Activator.CreateInstance(Type.GetType(_guideShowName)) as GuideStepView;
            if (_guideView != null)
            {
                _guideView.begin(_step_para);
            }
            _isActive = true;
        }

        virtual public bool update()
        {
            if (_guideView != null)
            {
                if (_guideView.update() == false)
                {
                    _guideView.end();
                    return false;
                }
            }
            return _isActive;
        }

        virtual public void end()
        {
            if (_guideView != null)
            {
                _guideView.end();
                _guideView = null;
            }
            _isActive = false;
        }

        virtual public bool process(string triggerType, string para)
        {
            if (_guideView != null)
            {
                if (_guideView.processEvent(triggerType, para))
                {
                    end();
                    return false;
                }
            }
            if (_end_trigger_type == triggerType && (_end_trigger_para == para || para == "*"))
            {
                end();
                return false;
            }
            return true;
        }
    }

}