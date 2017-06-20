#region HeadComments
/* ========================================================================
* Copyright (C) 2015 SinceMe
* 
* 作    者：Clime
* 文件名称：CBGameGlobalEventType
* 功    能：存放全局事件类型的常量字符串
* 创建时间：2015/8/12 23:47:16
* 版    本：V1.0.0
*
* 修改日志：修改者： 时间： 修改内容：
* 
* =========================================================================
*/
#endregion
/// <summary>
/// 存放全局事件类型的常量字符串
/// </summary>
public class CBGameGlobalEventType
{
    /// <summary>
    /// 示例
    /// </summary>
    public const string GlobalExampleEventType = "GlobalExampleEventType";

    public const string EVENT_CARD_SELECT = "EVENT_CARD_SELECT";//卡牌选中

    public const string EVENT_CHAPTER_POINT_SELECT = "EVENT_CHAPTER_POINT_SELECT";//副本章节选择

    public const string EVENT_CHAPTER_VIEW_SHOW = "EVENT_CHAPTER_VIEW_SHOW";//打开章节选择面板

    public const string EVENT_CHAPTER_VIEW_CLOSE = "EVENT_CHAPTER_VIEW_CLOSE";//关闭章节选择面板

    public const string EVENT_GO_WAR = "EVENT_GO_WAR";//去战斗

    public const string EVENT_ULTIMATE_SKILL_COUNT_FULL = "EVENT_ULTIMATE_SKILL_COUNT_FULL";//终极技能蓄满

    public const string EVENT_ULTIMATE_SKILL_USE = "EVENT_ULTIMATE_SKILL_USE";//终极技能使用

    public const string EVENT_CHAPTER_HEROITEM_SELECT = "EVENT_CHAPTER_HEROITEM_SELECT";

    public const string EVENT_WAR_START = "EVENT_WAR_START";//开始战斗

    public const string EVENT_COMPTITIVE_HERO_SELECT = "EVENT_COMPTITIVE_HERO_SELECT";//竞技场选择挑战的英雄

    public const string EVENT_COMPTITIVE_WAR_START = "EVENT_COMPTITIVE_WAR_START";//竞技场战斗开始

    public const string EVENT_MAP_TOUCH_SWITCH = "EVENT_MAP_TOUCH_SWITCH";//大地图滑动是否开启

    public const string EVENT_MAP_INIT_FINISH = "EVENT_MAP_INIT_FINISH";//大地图初始完成

    public const string EVENT_CHAPTE_GIFT_GET = "EVENT_CHAPTE_GIFT_GET";//章节宝箱领取

	public const string EVENT_SHOP_SEVER_BACK = "EVENT_SHOP_SEVER_BACK";//获取商店服务器数据

    public const string EVENT_COMBA_ARENA_MATCH = "EVENT_COMBA_ARENA_MATCH";//格斗竞技场准备就绪

    public const string EVENT_SELECT_CHAPTER_AWARD_ICON = "EVENT_SELECT_CHAPTER_AWARD_ICON";//选中章节奖励Icon

    public const string EVENT_CLOSE_CHAPTER_AWARD_ICON = "EVENT_CLOSE_CHAPTER_AWARD_ICON";//关闭章节奖励Icon
}
