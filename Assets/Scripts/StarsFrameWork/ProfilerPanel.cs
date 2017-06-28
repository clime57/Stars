using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
namespace Stars
{
    public class ProfilerPanel : MonoBehaviour
    {
        const float m_KBSize = 1024.0f * 1024.0f;
        const string notTableDataFlag = "**";
        private string memory, framerate;

        private float updateInterval = 0.2F;
        private float accum = 0; // FPS accumulated over the interval
        private int frames = 0; // Frames drawn over the interval
        private float timeleft; // Left time for current interval

        float timeSave = 3;

        float resAll = 0;
        //各类型的消耗
        Dictionary<Type, float> typeCount = null;
        //RSS内存
        float _rssMemory = 0;

        float _time = 0;

        bool _isShowInfo = false;
        void Start()
        {
            timeleft = updateInterval;
            //onCreatePath();
        }

        public void onCreatePath()
        {
            //if (Directory.Exists(Application.persistentDataPath + "/11" ))
            //{

            //}
            //else
            //{
            //    Directory.CreateDirectory(Application.persistentDataPath + "/11");
            //}
            //Application.CaptureScreenshot( "122.png");
        }

        void Update()
        {
            if (_time > 0.3f)
            {
                RG_Utils.callAndroidJava("getMemory");
                _time = 0;
            }
            _time += Time.deltaTime;

            UpdateGameInfo();
        }

        void OnGUI()
        {
            GUILayout.Label("fps:" + framerate);
            if (_isShowInfo)
            {
                if (GUILayout.Button("HideInfo", GUILayout.Width(70), GUILayout.Height(30)))
                {
                    _isShowInfo = false;
                }
                GUILayout.Label("" + memory);
                if (GUILayout.Button("save", GUILayout.Width(100), GUILayout.Height(50)))
                {
                    save();
                    timeSave = 0;
                }
                if (timeSave < 3)
                {
                    GUILayout.Label("save to " + Application.persistentDataPath + "/memory.txt");
                }
            }
            else
            {
                if (GUILayout.Button("ShowInfo", GUILayout.Width(70), GUILayout.Height(30)))
                {
                    _isShowInfo = true;
                }
            }
        }
        /// <summary>
        /// 更新游戏信息
        /// </summary>
        void UpdateGameInfo()
        {
            float totalMemory = (float)(UnityEngine.Profiling.Profiler.GetTotalAllocatedMemory() / m_KBSize);
            float totalReservedMemory = (float)(UnityEngine.Profiling.Profiler.GetTotalReservedMemory() / m_KBSize);
            float totalUnusedReservedMemory = (float)(UnityEngine.Profiling.Profiler.GetTotalUnusedReservedMemory() / m_KBSize);
            float monoHeapSize = (float)(UnityEngine.Profiling.Profiler.GetMonoHeapSize() / m_KBSize);
            float monoUsedSize = (float)(UnityEngine.Profiling.Profiler.GetMonoUsedSize() / m_KBSize);

            float processMemory = Device.ProcessMem / 1024.0f;

            timeleft -= Time.deltaTime;
            accum += Time.timeScale / Time.deltaTime;
            ++frames;
            timeSave += Time.deltaTime;
            // Interval ended - update GUI text and start new interval
            if (timeleft <= 0.0)
            {
                // display two fractional digits (f2 format)
                float fps = accum / frames;
                framerate = fps.ToString();// ioo.f("fps:{0:F2}", fps);

                //if (fps < 30) {
                //    framerate.color = Color.yellow;
                //} else {
                //    if (fps < 10) {
                //        framerate.color = Color.red;
                //    } else {
                //        framerate.color = Color.green;
                //    }
                //}
                timeleft = updateInterval;
                accum = 0.0F;
                frames = 0;
            }
            if (_isShowInfo)
            {
                memory = string.Format("**TotalAllocatedMemory：{0}MB\n" +
                                            "**TotalReservedMemory：{1}MB\n" +
                                            "**TotalUnusedReservedMemory:{2}MB\n" +
                                            "**MonoHeapSize:{3}MB\n**MonoUsedSize:{4}MB\n" +
                                            "**ProcessMemory:{5}MB\n" +
                                            "**ResAll:{6}MB\n",
                                            totalMemory, totalReservedMemory,
                                            totalUnusedReservedMemory, monoHeapSize, monoUsedSize, processMemory, resAll
                                           );
            }
        }
        void save()
        {

            resAll = 0;
            string ss = "";
            if (typeCount == null)
                typeCount = new Dictionary<Type, float>();
            typeCount.Clear();
            ss += saveType(typeof(Texture));
            ss += saveType(typeof(Mesh));
            ss += saveType(typeof(AudioClip));
            ss += saveType(typeof(AnimationClip));
            ss += saveType(typeof(Material));
            resAll = resAll / m_KBSize;
            UpdateGameInfo();
            string title = notTableDataFlag + "内存消耗记录,时间" + System.DateTime.Now + "\n";
            title += memory;

            foreach (KeyValuePair<Type, float> kv in typeCount)
            {
                title += notTableDataFlag + kv.Key.Name + ":" + kv.Value.ToString() + "MB\n";
            }

            title += notTableDataFlag + "名称,大小,模块类别,文件路径\n";
            ss = title + ss + "*******************************************************";
            CreateFile(Application.persistentDataPath, "/memory.txt", ss);

        }
        //Mesh, Texture, Audio, Animation and Materials
        string saveType(Type tp)
        {
            string ss = "";
            UnityEngine.Object[] objs = Resources.FindObjectsOfTypeAll(tp);
            ss += (notTableDataFlag + tp.Name + "部分,数量" + objs.Length.ToString());
            string sss = "";
            float typeSize = 0;
            foreach (UnityEngine.Object t in objs)
            {
                //只有在编辑器下或开发模式编译才能真正获取
                int s = UnityEngine.Profiling.Profiler.GetRuntimeMemorySize(t);
                typeSize += s;
                string newName = t.name;
                if (newName == "") newName = "empty";
                sss += (newName + "," + s.ToString() + "\n");
                resAll += s;
            }
            ss += ",消耗" + (typeSize / m_KBSize).ToString() + "MB\n";
            ss += sss;
            typeCount.Add(tp, typeSize / m_KBSize);
            for (int i = 0; i < objs.Length; i++)
            {
                objs[i] = null;
            }
            return ss;

        }

        static void CreateFile(string path, string name, string info)
        {
            //文件流信息
            StreamWriter sw = new StreamWriter(path + name, true, System.Text.Encoding.UTF8); ;

            //以行的形式写入信息
            sw.WriteLine(info);
            sw.Flush();
            //关闭流
            sw.Close();
            //销毁流
            sw.Dispose();
        }

    }
}