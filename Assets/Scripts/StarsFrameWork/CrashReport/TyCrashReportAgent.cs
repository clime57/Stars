
using UnityEngine;
using System.Collections;
namespace Stars
{
    /// <summary>
    /// 崩溃异常捕获代理接口
    /// </summary>
    public interface TyCrashReportAgent
    {
        /// <summary>
        /// 对崩溃异常捕获进行初始化
        /// </summary>
        /// <param name="info">初始化信息</param>
        void init(TyCrashReport.initInfo info);
        /// <summary>
        /// 上报异常
        /// </summary>
        /// <param name="e">异常</param>
        /// <param name="desc">描述</param>
        void reportException(System.Exception e, string desc);
        /// <summary>
        /// 设置用户ID
        /// </summary>
        /// <param name="userId">用户ID</param>
        void setUserId(string userId);
    }

}