using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GuideGroup 
{
	public string _name = "";//引导组名称
	public int  _priority = 0;//优先级，先执行优先级高的满足相同条件的引导
	public string _begin_trigger_type = "";//触发引导类型
	public string _begin_trigger_para = "";//触发引导参数
	public int _minLevel = 0;//引导开始最小等级
	public int _maxLevel = 0;//引导开始最高等级
    public int _gameStartCheck = -1;//-1不允許，1允許
	bool _isActive = false;
	GuideStep _curStep = null;
	
	public Dictionary<uint,GuideStep> _guideSteps = new Dictionary<uint, GuideStep>();


    virtual public bool parse(string triggerType,string para)
    {
        bool result = true;        
        //        Debug.Log("分析結果 = " + result.ToString());

        return result;
    }

	virtual public bool process(string trigger_type,string para){
		if(_curStep != null){
			if(_curStep.process(trigger_type,para) == false){
				_curStep.end();
				_curStep = nextStep	();
				if(_curStep == null){
					end ();
					return false;
				}else{
					_curStep.begin();
					return true;
				}
			}else{
				return true;	
			}
		}
		_curStep = null;
		return false;
	}
	
	virtual public bool update(){
		if(_curStep != null){
			if(_curStep.update() == false){
                if(_curStep != null)//clime
				    _curStep.end();
				_curStep = nextStep	();
				if(_curStep == null){
					end ();
				}else{
					_curStep.begin();	
				}
			}
		}
		return _isActive;
	}
	
	virtual public bool begin(string para){
		//UIWindowManager.getInstance().hideOtherWindow();
		_curStep = nextStep	();
		if(_curStep != null)_curStep.begin();
		_isActive = true;
		return true;
	}
	

	
	virtual public void end(){
		if(_curStep != null){
			_curStep.end();
			_curStep = null;
		}
		_isActive = false;
	}
	
	GuideStep nextStep(){
		uint id = 0;
		if(_curStep != null)
		{
			id = _curStep._id;	
		}
		if(_guideSteps.ContainsKey(id + 1)){
			return _guideSteps[id + 1];	
		}else{
			return null;
		}
	}

    public bool endCurStep()
    {
        if (_curStep != null)
        {
            //if (_curStep.update() == false)
            {
                _curStep.end();
                _curStep = nextStep();
                if (_curStep == null)
                {
                    end();
                }
                else
                {
                    _curStep.begin();
                }
            }
        }
        return _isActive;
    }
}

