using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;

struct GameObjectInfo
{
    public Type _type;
    public object _obj;
    public bool _isInit;
    public bool _isUpdate;
    public bool _isFixUpdate;
    public bool _isLateUpdate;
}

public class Game
{
    static Game game_ = null;
    Dictionary<Type, object> gameSubSystemList_ = new Dictionary<Type, object>();
    Dictionary<Type, GameSubSystem> gameSubSystemUpdateList_ = new Dictionary<Type, GameSubSystem>();
    Dictionary<Type, GameSubSystem> gameSubSystemFixUpdateList_ = new Dictionary<Type, GameSubSystem>();
    Dictionary<Type, GameSubSystem> gameSubSystemLateUpdateList_ = new Dictionary<Type, GameSubSystem>();
    List<GameObjectInfo> gameSubSystemAddList_ = new List<GameObjectInfo>();
    List<object> gameSubSystemRemoveList = new List<object>();
    virtual public void awake()
    {

    }

    virtual public void init()
    {
        addObjects();
    }

    virtual public void update()
    {
        for (int i = 0; i < gameSubSystemRemoveList.Count; i++)
        {
            Type t = gameSubSystemRemoveList[i].GetType();
            gameSubSystemList_.Remove(t);
            gameSubSystemUpdateList_.Remove(t);
            gameSubSystemFixUpdateList_.Remove(t);
            gameSubSystemLateUpdateList_.Remove(t);
        }
        for (int i = 0; i < gameSubSystemAddList_.Count;i++ ){
            GameObjectInfo goi = gameSubSystemAddList_[i];
            createObject(goi._obj, goi._type, goi._isUpdate, goi._isFixUpdate,goi._isLateUpdate);
        }
        gameSubSystemRemoveList.Clear();
        gameSubSystemAddList_.Clear();
        Dictionary<Type, GameSubSystem>.Enumerator enumrator = gameSubSystemUpdateList_.GetEnumerator();
        while (enumrator.MoveNext())
        {
            enumrator.Current.Value.update(Time.deltaTime);
        }
    }

    virtual public void fixedUpdate()
    {
        Dictionary<Type, GameSubSystem>.Enumerator enumrator = gameSubSystemFixUpdateList_.GetEnumerator();
        while (enumrator.MoveNext())
        {
            enumrator.Current.Value.fixUpdate(Time.fixedDeltaTime);
        }
    }

    virtual public void LateUpdate()
    {
        Dictionary<Type, GameSubSystem>.Enumerator enumrator = gameSubSystemLateUpdateList_.GetEnumerator();
        while (enumrator.MoveNext())
        {
            enumrator.Current.Value.lateUpdate(Time.deltaTime);
        }
    }

    static public Game getInstance()
    {
        return game_;
    }

    public Game()
    {
        game_ = this;
    }

    public T findObject<T>()
    {
        Type t = typeof(T);
        if(gameSubSystemList_.ContainsKey(t)){
            return (T)gameSubSystemList_[t];
        }else{
            foreach(KeyValuePair<Type,object> kvp in gameSubSystemList_){
                if (kvp.Key.IsSubclassOf(t))
                {
                    return (T)kvp.Value;
                }
            }
        }
        return default(T);
    }
    /// <summary>
    /// 供子类调用来添加游戏子系统
    /// </summary>
    /// <typeparam name="T">游戏子系统类型</typeparam>
    /// <param name="isInit">是否执行初始化</param>
    /// <param name="isUpdate">是否更新</param>
    /// <param name="isFixUpdate">是否固定帧率更新</param>
    /// <param name="isLateUpdte">是否延迟更新</param>
    protected void addObject<T>(bool isInit, bool isUpdate,bool isFixUpdate = false,bool isLateUpdte = false)
    {
        createObject(Type.GetType(typeof(T).FullName), isInit, isUpdate, isFixUpdate, isLateUpdte);
    }
    /// <summary>
    /// 将某个游戏子系统加入到创建列表中，初始化（如果有）将立即执行，如果有更新将于下一帧执行
    /// 游戏子系统如需更新需要重写基类相关函数
    /// </summary>
    /// <typeparam name="T">游戏子系统类型</typeparam>
    /// <param name="isInit">是否执行初始化</param>
    /// <param name="isUpdate">是否更新</param>
    /// <param name="isFixUpdate">是否固定帧率更新</param>
    /// <param name="isLateUpdte">是否延迟更新</param>
    public void addObjToCreateList<T>(bool isInit, bool isUpdate, bool isFixUpdate = false, bool isLateUpdte = false)
    {
        GameObjectInfo info = new GameObjectInfo();
        info._type = Type.GetType(typeof(T).FullName);
        object t = Activator.CreateInstance(info._type);
        info._obj = t;
        info._isInit = isInit;
        info._isUpdate = isUpdate;
        info._isFixUpdate = isFixUpdate;
        info._isLateUpdate = isLateUpdte;
        gameSubSystemAddList_.Add(info);
        if (isInit)
        {
            ((GameSubSystem)t).init();
        }
    }
    /// <summary>
    /// 创建已经初始化好的游戏对象
    /// </summary>
    /// <param name="t">游戏对象实例</param>
    /// <param name="typeOfObj">类型</param>
    /// <param name="isUpdate">是否需要更新</param>
    void createObject(object t, Type typeOfObj, bool isUpdate, bool isFixUpdate = false, bool isLateUpdte = false)
    {
        if (gameSubSystemList_.ContainsKey(typeOfObj))
        {
            TyLogger.LogError("Already exist an Object " + typeOfObj.ToString());
        }
        else
        {
            gameSubSystemList_.Add(typeOfObj, t);
            if (isUpdate) gameSubSystemUpdateList_.Add(typeOfObj, (GameSubSystem)t);
            if (isFixUpdate) gameSubSystemFixUpdateList_.Add(typeOfObj, (GameSubSystem)t);
            if (isLateUpdte) gameSubSystemLateUpdateList_.Add(typeOfObj, (GameSubSystem)t);
        }

    }

    void createObject(Type typeOfObj, bool isInit, bool isUpdate, bool isFixUpdate = false, bool isLateUpdte = false)
    {
        if (gameSubSystemList_.ContainsKey(typeOfObj))
        {
            TyLogger.LogError("already exist a Object " + typeOfObj.ToString());
        }
        else
        {
            object t = Activator.CreateInstance(typeOfObj);
            gameSubSystemList_.Add(typeOfObj, t);
            if (isInit) ((GameSubSystem)t).init();
            if (isUpdate) gameSubSystemUpdateList_.Add(typeOfObj, (GameSubSystem)t);
            if (isFixUpdate) gameSubSystemFixUpdateList_.Add(typeOfObj, (GameSubSystem)t);
            if (isLateUpdte) gameSubSystemLateUpdateList_.Add(typeOfObj, (GameSubSystem)t);
        }
    }
    /// <summary>
    /// 移除某游戏子系统
    /// </summary>
    /// <typeparam name="T">游戏子系统类型</typeparam>
    public void removeObject<T>()
    {
        object gss;
        if (gameSubSystemList_.TryGetValue(typeof(T), out gss))
        {
            if (!gameSubSystemRemoveList.Contains(gss))
            {
                gameSubSystemRemoveList.Add(gss);
                ((GameSubSystem)gss).shutdown();
            }
        }
    }
    /// <summary>
    /// 初始化时执行添加游戏子系统
    /// </summary>
    virtual protected void addObjects()
    {

    }

    virtual public void processMsg(GameObject obj)
    {

    }

    virtual public void resetWhenBackToLogin()
    {
        Dictionary<Type, object>.Enumerator gameSubSystemEnum = gameSubSystemList_.GetEnumerator();
        while (gameSubSystemEnum.MoveNext())
        {
            ((GameSubSystem)(gameSubSystemEnum.Current.Value)).resetWhenBackToLogin();
        }
    }
}
