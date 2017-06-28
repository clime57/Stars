/*
 * 描术：
 * 
 * 作者：AnYuanLzh
 * 公司：lexun
 * 时间：2014-xx-xx
 */
using UnityEngine;
using System.Collections;

public class lzh_LoopItemTest1:MonoBehaviour
{
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

public class Lzh_LoopItemData1 : Lzh_LoopItemData
{
	public string name = "-1";
	public Lzh_LoopItemData1(string name)
	{
		this.name = name;
	}
}
