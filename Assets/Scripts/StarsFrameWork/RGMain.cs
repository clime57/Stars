using UnityEngine;
using System.Collections;
using System;
namespace Stars
{
    public class RGMain : MonoBehaviour
    {
        static RGMain _main = null;
        Game game_ = null;
        public string gameType = "Game";

        void Awake()
        {
            if (_main != null)
            {
                GameObject.DestroyImmediate(gameObject);
                return;
            }
            Device device = GetComponent<Device>();
            if (device == null)
            {
                device = gameObject.AddComponent<Device>();
            }
            Device.collectInfo();
            ChanelSDK channelsdk = GetComponent<ChanelSDK>();
            if (channelsdk == null)
            {
                channelsdk = gameObject.AddComponent<ChanelSDK>();
            }
            DontDestroyOnLoad(this.gameObject);
            _main = this;
            if (gameType == "" || gameType == "Game")
            {
                game_ = new Game();
            }
            else
            {
                game_ = Activator.CreateInstance(Type.GetType(gameType)) as Game;
            }
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            game_.awake();
        }

        void Start()
        {
            game_.init();
        }

        void Update()
        {
            game_.update();
        }

        void FixedUpdate()
        {
            game_.fixedUpdate();
        }

        void LateUpdate()
        {
            game_.LateUpdate();
        }

        static public RGMain Get()
        {
            return _main;
        }

        void processMsg(GameObject obj)
        {
            game_.processMsg(obj);
        }

    }

}