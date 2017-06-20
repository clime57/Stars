using UnityEngine;
using System.Collections;



public delegate void callbackSystemConfig();


public class SystemConfig : GameData
{
	static bool soundSwich_ = true;
	static bool musicSwith_ = true;
    static bool netPlayerShowSwith_ = true;
    static public float soundVsolume = 1.0f;
    static public float musicVsolume = 1.0f;
    static Renderquality renderquality_ = Renderquality.Normal;

    static public callbackSystemConfig _callbackSound;
    static public callbackSystemConfig _callbackMusic;
    static public callbackSystemConfig _callbackNetPlayer;
	
	static public bool soundSwich {
		set {
			soundSwich_ = value;
            if (_callbackSound != null)
            {
                _callbackSound();
            }
			PlayerPrefs.SetInt ("SoundSwich",value?1:0);
            //AudioListener.volume = value ? 1 : 0;
		}
		get {
			return soundSwich_;
		}
	}
	
	static public bool musicSwich {
		set {
			musicSwith_ = value;
            if (_callbackMusic != null)
            {
                _callbackMusic();
            }
			PlayerPrefs.SetInt ("MusicSwich",value?1:0);
		}
		get {
			return musicSwith_;
		}
	}
	
    //static public bool netPlayerShowSwich {
    //    set {
    //        netPlayerShowSwith_ = value;
    //        if (_callbackNetPlayer != null)
    //        {
    //            _callbackNetPlayer();
    //        }
    //        PlayerPrefs.SetInt ("NetPlayerShowSwich",value?1:0);
    //    }
    //    get {
    //        return netPlayerShowSwith_;
    //    }
    //}

    static public Renderquality renderquality
    {
        set
        {
            renderquality_ = value;
            PlayerPrefs.SetInt("Renderquality", (int)value);
            setAnisotropicFilter();
        }
        get
        {
            return renderquality_;
        }
    }
    /// <summary>
    /// 是否要省内存
    /// </summary>
    static public bool IsNeedSaveMemory
    {
        get
        {
            return Device._isMemoryNotMuch;
        }
    }

    static public void setAnisotropicFilter()
    {
        if (renderquality_ == Renderquality.High || renderquality_ == Renderquality.VeryHigh)
        {
            QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
        }
        else
        {
            QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
        }
    }

    /// <summary>
    /// 测试是否设置过渲染级别
    /// </summary>
    /// <returns></returns>
    static public bool isUserSetRenderQuality()
    {
        return PlayerPrefs.HasKey("Renderquality");
    }
	public SystemConfig ()
	{
		if (PlayerPrefs.HasKey ("SoundSwich")) {
			soundSwich_ = PlayerPrefs.GetInt ("SoundSwich") == 1 ? true : false;
            //AudioListener.volume = soundSwich_ ? 1 : 0;
		} else {
			PlayerPrefs.SetInt ("SoundSwich", 1);
			soundSwich_ = true;
		}
		
		if (PlayerPrefs.HasKey ("MusicSwich")) {
			musicSwith_ = PlayerPrefs.GetInt ("MusicSwich") == 1 ? true : false;	
		} else {
			PlayerPrefs.SetInt ("MusicSwich", 1);
			musicSwith_ = true;
		}
		
		if (PlayerPrefs.HasKey ("NetPlayerShowSwich")) {
			netPlayerShowSwith_ = PlayerPrefs.GetInt ("NetPlayerShowSwich") == 1 ? true : false;	
		} else {
			PlayerPrefs.SetInt ("NetPlayerShowSwich", 1);
			netPlayerShowSwith_ = true;
		}

        if (PlayerPrefs.HasKey("Renderquality"))
        {
            renderquality_ = (Renderquality)PlayerPrefs.GetInt("Renderquality");
        }
	}
	
}
