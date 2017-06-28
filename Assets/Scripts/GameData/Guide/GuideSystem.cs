using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Stars
{
    /// <summary>
    /// 
    /// </summary>
    public enum Enum_GuideRecord
    {

    }

    public class GuideSystem : GameSubSystem
    {
        public const bool UseGuide = true;
        public GuideGroup _curGuideGroup = null;
        Dictionary<string, GuideGroup> _guideGroupList = new Dictionary<string, GuideGroup>();
        string _nextGuideGroupName = "";
        bool _isResume = false;

        public delegate void RefreshTownTip();
        public RefreshTownTip _listRefreshTownTip = null;
        public ulong _guideRecord = 0xFFFFFFFFFFFFFFFF;

        public void setGuideRecord(ulong v)
        {
            _guideRecord = v;
        }

        public bool isGuideComplete(Enum_GuideRecord record)
        {
            int iRecord = (int)record;
            if (iRecord < 64)
            {
                ulong a = 1 & (_guideRecord >> iRecord);
                return a == 1;
            }
            return true;
        }

        public void setGuideComplete(Enum_GuideRecord record)
        {
            int iRecord = (int)record;
            if (iRecord < 64)
            {
                ulong a = (ulong)(((ulong)1) << iRecord);
                _guideRecord = a | _guideRecord;
            }
        }

        override public void init()
        {
            //GameMsgProcesser msgProcess = Game.getInstance().findObject<GameMsgProcesser>();
            //if (msgProcess != null)
            //{
            //    msgProcess.add("GuideObject", processGuideObject);
            //}
        }

        override public void update(float time)
        {
            if (_isResume == true)
            {
                _isResume = false;
                return;
            }
            bool canGuide = false;
            if (canGuide == false)
                return;
            if (_curGuideGroup != null)
            {
                if (_curGuideGroup.update() == false)
                {
                    _curGuideGroup.end();
                    _curGuideGroup = null;
                }
            }
            else
            {
                //processGuideEvent ("normal_guide", "task_shortcut_btn");
            }

            if (_nextGuideGroupName != "")
            {
                if (_guideGroupList.ContainsKey(_nextGuideGroupName))
                {
                    if (_curGuideGroup != null)
                    {
                        _curGuideGroup.end();
                    }
                    GuideGroup processGroup = _guideGroupList[_nextGuideGroupName];
                    _curGuideGroup = processGroup;
                    _curGuideGroup.begin("");

                }

                _nextGuideGroupName = "";
            }
        }

        public void addTownTipFun(RefreshTownTip fun)
        {
            _listRefreshTownTip += fun;
        }
        //红点提示
        public void refreshTownTip()
        {
            if (_listRefreshTownTip != null)
            {
                _listRefreshTownTip();
            }
        }
        //恢复，继续执行
        public void resume()
        {
            _isResume = true;
        }

        public void endCurLuaStep()
        {
            if (_curGuideGroup != null)
            {//如果當前正在進行一個組
                _curGuideGroup.process("endLuaStep", "");
            }
        }

        public void reset()
        {
            if (_curGuideGroup != null)
            {
                _curGuideGroup.end();
            }
            _curGuideGroup = null;
            _nextGuideGroupName = "";
        }

        public void beginNewGuide(string guideGroupName)
        {
            _nextGuideGroupName = guideGroupName;
        }

        void processGuideObject(GameObject obj)
        {
            GuideObject go = obj.GetComponent<GuideObject>();
            if (go != null)
            {
                if (go._name == "")
                    return;

                processGuideEvent("ui_click", go._name);
                //resumeScript();
            }

        }

        public void processGuideEvent(string triggerType, string trigger_para)
        {
            if (UseGuide == false) return;

            //Debug.Log("processGuideEvent " + triggerType + " " + trigger_para);
            GuideGroup processGroup = null;
            //查找或獲取一個Group
            foreach (KeyValuePair<string, GuideGroup> kv in _guideGroupList)
            {
                //Debug.Log("分析觸發器" + kv.Value._name + " ");
                if (kv.Value._begin_trigger_type.Contains(triggerType) && (kv.Value._begin_trigger_para == trigger_para || trigger_para == "*" || kv.Value._begin_trigger_para == "*")
                    && kv.Value.parse(triggerType, trigger_para))
                {

                    if (processGroup == null)
                    {
                        processGroup = kv.Value;
                    }
                    else if (processGroup._priority > kv.Value._priority)//比较优先级，最后确定符合条件的最高优先级的组
                    {
                        processGroup = kv.Value;
                    }

                }
            }
            //如果找到一個組
            if (processGroup != null)
            {
                if (_curGuideGroup != null)
                {
                    if (_curGuideGroup._name == processGroup._name)
                    {//找到的组就是当前组
                        _curGuideGroup.process(triggerType, trigger_para);
                    }
                    else if (_curGuideGroup._priority > processGroup._priority)
                    {//找到的组不是当前组，判断优先级后，如果优先级够就要结束当前组，并开始找到的组
                        if (processGroup.parse(triggerType, trigger_para) == true)
                        {
                            _curGuideGroup.end();
                            _curGuideGroup = processGroup;
                            _curGuideGroup.begin(trigger_para);
                        }
                    }
                }
                else
                {//把之前找到的组作为当前组，并开始
                    if (processGroup.parse(triggerType, trigger_para) == true)
                    {
                        _curGuideGroup = processGroup;
                        _curGuideGroup.begin(trigger_para);

                    }
                }
            }
            else if (_curGuideGroup != null)
            {//如果當前正在進行一個組
                if (_curGuideGroup.process(triggerType, trigger_para) == false)
                {
                    _curGuideGroup.end();
                    _curGuideGroup = null;
                }
            }
            //#endif
        }



        void readGuideStep(System.Security.SecurityElement se, GuideGroup guideGroup)
        {
            foreach (System.Security.SecurityElement child in se.Children)
            {
                Hashtable hash = child.Attributes;
                DictionaryEntry[] array = new DictionaryEntry[hash.Count];
                hash.CopyTo(array, 0);
                GuideStep guideStep = new GuideStep();
                for (int i = 0; i < array.Length; i++)
                {
                    DictionaryEntry attr = array[i];
                    string strKey = (string)attr.Key;
                    string strValue = (string)attr.Value;
                    if (strKey == ("id"))
                    {
                        guideStep._id = uint.Parse(strValue);
                    }
                    else if (strKey == "step_name")
                    {
                        guideStep._guideShowName = strValue;
                    }
                    else if (strKey == "step_para")
                    {
                        guideStep._step_para = strValue;
                    }
                    else if (strKey == "end_trigger_type")
                    {
                        guideStep._end_trigger_type = strValue;
                    }
                    else if (strKey == "end_trigger_para")
                    {
                        guideStep._end_trigger_para = strValue;
                    }
                    else if (strKey == "begin_reset")
                    {
                        guideStep._begin_reset = int.Parse(strValue);
                    }
                    else if (strKey == "end_reset")
                    {
                        guideStep._end_reset = int.Parse(strValue);
                    }
                    else if (strKey == "guide_arrow_pos")
                    {
                        guideStep._guideArrowPos = strValue;
                    }
                }
                guideGroup._guideSteps.Add(guideStep._id, guideStep);
            }
        }

        public override void resetWhenBackToLogin()
        {
            reset();
        }

    }
}