using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
namespace Stars
{
    public class SoundPlayer : MonoBehaviour
    {
        static SoundPlayer _instance;
        AudioClip _clip = null;
        List<AudioSource> _audioSources = new List<AudioSource>();
        Dictionary<string, AudioClip> _soundClips = new Dictionary<string, AudioClip>();
        Dictionary<string, ChnnelInfo> _channelAudioSouces = new Dictionary<string, ChnnelInfo>();

        class ChnnelInfo
        {
            public AudioSource _as;
            public Dictionary<string, AudioClip> _clips = new Dictionary<string, AudioClip>();
        }

        public enum SoundEnum
        {
            /// <summary>
            /// 最大值，必须放到最下方
            /// </summary>
            Max
        }
        /// <summary>
        /// 存放一些放在AssetBundle中的音效
        /// </summary>
        AudioClip[] _audioClips = new AudioClip[(int)SoundEnum.Max + 1];
        /// <summary>
        /// 播放在AssetBundle中的音效
        /// </summary>
        /// <param name="soundEnum"></param>
        public void playAssetBundleSound(SoundEnum soundEnum)
        {
            if (_audioClips != null && _audioClips.Length > (int)soundEnum)
            {
                playNoCacheSound(_audioClips[(int)soundEnum]);
            }
        }
        /// <summary>
        /// 设置一些AssetBundle中的音效
        /// </summary>
        /// <param name="audioClips">存放音效的数组</param>
        public void setAssetBundleSound(AudioClip[] audioClips)
        {
            _audioClips = audioClips;
        }

        void Awake()
        {
            _instance = this;
            if (GetComponent<AudioSource>() != null)
            {
                _audioSources.Add(GetComponent<AudioSource>());
            }
        }

        public static SoundPlayer getInstance()
        {
            return _instance;
        }
        /// <summary>
        /// 播放音效,如果找不到声音，将返回假
        /// </summary>
        /// <param name="soundPath">Sound/以下的路径</param>
        public bool playNoCacheSound(string soundPath)
        {
            unload();
            if (!_soundClips.TryGetValue(soundPath, out _clip))
            {
                _clip = Resources.Load("Sound/" + soundPath) as AudioClip;
                if (_clip == null)
                {
                    //loadAsset
                }
                if (_clip != null)
                {
                    _soundClips.Add(soundPath, _clip);
                }
            }
            return playNoCacheSound(_clip);
        }

        /// <summary>
        /// 播放音效,如果找不到声音，将返回假
        /// </summary>
        /// <param name="clip">AudioClip</param>
        /// <returns></returns>
        public bool playNoCacheSound(AudioClip clip)
        {
            _clip = clip;
            //ruby 20150907 声音判断
            if (clip != null && SystemConfig.soundSwich == true)
            {
                for (int i = 0; i < _audioSources.Count; i++)
                {
                    if (!_audioSources[i].isPlaying)
                    {
                        _audioSources[i].PlayOneShot(_clip);
                        return true;
                    }
                }
                AudioSource audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.PlayOneShot(_clip);
                _audioSources.Add(audioSource);
                return true;
            }
            else
            {
                //Debug.Log(soundPath + "can't be find!");
                return false;
            }
        }
        /// <summary>
        /// 在某个通道播放声音，在这个通道下声音是独占的，如果要播放新的声音，将会使以前的声音停止
        /// </summary>
        /// <param name="soundPath">Resource/Sound下的路径</param>
        /// <param name="channel">通道名称，自定义即可，如果之前没有此通道，将新建</param>
        /// <returns>播放成功返回真，否则假,系统开关关闭也会返回假</returns>
        public bool playInChannel(string soundPath, string channel)
        {
            ChnnelInfo ci = null;
            AudioClip ac = null;
            if (!SystemConfig.soundSwich) return false;
            if (!_channelAudioSouces.TryGetValue(channel, out ci))
            {
                ci = new ChnnelInfo();
                ci._as = gameObject.AddComponent<AudioSource>();
                _channelAudioSouces.Add(channel, ci);
            }
            if (ci._clips.TryGetValue(soundPath, out ac))
            {
                ci._as.clip = ac;
            }
            else
            {
                ci._as.clip = Resources.Load("Sound/" + soundPath, typeof(AudioClip)) as AudioClip;
                if (ci._as.clip == null)
                {
                    //loadAsset
                }
                if (ci._as.clip != null)
                {
                    ci._clips.Add(soundPath, ci._as.clip);
                }
            }
            if (ci._as.clip != null)
            {
                ci._as.Stop();
                ci._as.Play();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void OnLevelWasLoaded(int level)
        {
            unload();
            unloadAllChnnel();
        }


        /// <summary>
        /// 释放所有通道
        /// </summary>
        public void unloadAllChnnel()
        {
            foreach (ChnnelInfo ci in _channelAudioSouces.Values)
            {
                Object.Destroy(ci._as);
                foreach (AudioClip ac in ci._clips.Values)
                {
                    Resources.UnloadAsset(ac);
                }
            }
            _channelAudioSouces.Clear();
        }
        /// <summary>
        /// 释放某通道
        /// </summary>
        /// <param name="channel">通道名称</param>
        public void unloadChannel(string channel)
        {
            ChnnelInfo ci = null;
            if (_channelAudioSouces.TryGetValue(channel, out ci))
            {
                Object.Destroy(ci._as);
                foreach (AudioClip ac in ci._clips.Values)
                {
                    Resources.UnloadAsset(ac);
                }
                _channelAudioSouces.Remove(channel);
            }
        }

        public void unload()
        {
            if (_soundClips.Count > 10)
            {
                for (int i = 0; i < _audioSources.Count; i++)
                {
                    if (!_audioSources[i].isPlaying)
                    {
                        Object.Destroy(_audioSources[i]);
                        _audioSources.Remove(_audioSources[i]);
                        i--;
                    }
                }
                foreach (AudioClip ac in _soundClips.Values)
                {
                    Resources.UnloadAsset(ac);
                }
                _soundClips.Clear();
            }
        }
    }

}