/*
 * 描术：
 * 
 * 作者：AnYuanLzh
 * 公司：lexun
 * 时间：2014-xx-xx
 */
using UnityEngine;
using System.Collections.Generic;

public class lzh_LoopItemTest2:MonoBehaviour
{
	public UISprite icon;
	public UILabel lblName;


	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}

public class Lzh_LoopItemData2 : Lzh_LoopItemData
{
	public string name = "-1";
	public string iconSpriteName = "";

	public Lzh_LoopItemData2(string name, string iconSpriteName)
	{
		this.name = name;
		this.iconSpriteName = iconSpriteName;
	}


}
