using UnityEngine;
using System.Collections;

public enum GuideObjIndex
{
	
}
//主界面     1	背包1，强化装备2，升级技能3，奖励4，活动5，背包6，精灵7，星座8，宝石9
//背包内部   2	顺序位置0-50（先横向数），关闭按钮99
//物品操作UI  3	装备1-针对装备，合成2-针对图纸，镶嵌3-针对宝石
//精灵UI   4	培育切页1，喂养切页2，培育切页的培育按钮3，培育切页的确认按钮4
//星座UI   5	点亮按钮1，关闭按钮2
//装备强化UI  6	装备选取槽位0-50（先横向数），强化按钮51，关闭按钮99
//技能升级UI 7	技能槽位0-50（先向下数），升级51，装备52，关闭按钮99
//宝石UI   8	宝石槽位（0-50），合成按钮51，制作按钮52，镶嵌按钮53，关闭按钮99，装备选择槽位（100-150）
//NPC 对话框 9	任务列表槽位（0-10），完成时的交付按钮11,对话任务的对话部分确定扭12
//副本选择界面10	副本序号（0-50）
//副本描述界面11	进入副本按钮1
//活动UI 12	竞技场按钮11，对手选框（0-10），关闭战斗结算12，返回主界面按钮13
//奖励UI 13	竞技场奖励切页1，关闭按钮2

public enum GuideWindow{
	Main = 1,
	Inventory,
	ToolTip,
	Spirit,
	Star,
	Strenthen,
	Skill,
	Gem,
	NpcDialog,
	InstanceSelect,
	InstanceDescription,
	Activity,
	Reward
}
