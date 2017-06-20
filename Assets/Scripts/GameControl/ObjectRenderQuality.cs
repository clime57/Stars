using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class ParticleConfig
{
    public float _emissionRate = 0;
    public int _maxParticles = 0;
}

public class ObjectRenderQuality : MonoBehaviour
{
    public bool _RenderInLowMode = false;
    public bool _RenderInNormalMode = false;
    public bool _RenderInHighMode = true;
    public bool _RenderInVeryHighMode = true;

    public ParticleConfig _particleConfigInLowMode = new ParticleConfig();
    public ParticleConfig _particleConfigInNormalMode = new ParticleConfig();
    public ParticleConfig _particleConfigInHighMode = new ParticleConfig();
    public ParticleConfig _particleConfigInVeryHighMode = new ParticleConfig();

    ParticleConfig _orgParticleConfig = new ParticleConfig();
    void Awake()
    {
        setRender();
    }

    void setScriptEnable(bool enable)
    {
        Behaviour[] _script = gameObject.GetComponents<Behaviour>();
        for (int i = 0; i < _script.Length; i++)
        {
            if (_script[i] != null)
            {
                _script[i].enabled = enable;
            }
        }
        if (GetComponent<ParticleSystem>() != null)
        {
            GetComponent<ParticleSystem>().enableEmission = enable;
        }
        if (GetComponent<Renderer>() != null)
        {
            GetComponent<Renderer>().enabled = enable;
        }
        
    }

    void setParticle(ParticleConfig config)
    {
        if (config._emissionRate != 0 && config._maxParticles != 0)
        {
            GetComponent<ParticleSystem>().emissionRate = config._emissionRate;
            GetComponent<ParticleSystem>().maxParticles = config._maxParticles;
        }
        else
        {
            GetComponent<ParticleSystem>().emissionRate = _orgParticleConfig._emissionRate;
            GetComponent<ParticleSystem>().maxParticles = _orgParticleConfig._maxParticles;
        }
    }

    public void setRender()
    {
        Renderquality rq = Device.getRenderQualityLevle();


        if (rq == Renderquality.Low && _RenderInLowMode == false)
        {
            setScriptEnable(false);
            return;
        }
        if (rq == Renderquality.Normal && _RenderInNormalMode == false)
        {
            setScriptEnable(false);
            return;
        }
        if (rq == Renderquality.High && _RenderInHighMode == false)
        {
            setScriptEnable(false);
            return;
        }
        if (rq == Renderquality.VeryHigh && _RenderInVeryHighMode == false)
        {
            setScriptEnable(false);
            return;
        }


        if (GetComponent<ParticleSystem>() != null)
        {
            _orgParticleConfig._emissionRate = GetComponent<ParticleSystem>().emissionRate;
            _orgParticleConfig._maxParticles = GetComponent<ParticleSystem>().maxParticles;

            switch (rq)
            {
                case Renderquality.Low:
                    setParticle(_particleConfigInLowMode);
                    break;
                case Renderquality.Normal:
                    setParticle(_particleConfigInNormalMode);
                    break;
                case Renderquality.High:
                    setParticle(_particleConfigInHighMode);
                    break;
                case Renderquality.VeryHigh:
                    setParticle(_particleConfigInVeryHighMode);
                    break;

            }
        }


    }
}
