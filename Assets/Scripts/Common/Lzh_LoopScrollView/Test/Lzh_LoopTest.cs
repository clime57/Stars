using UnityEngine;
using System.Collections.Generic;

public class Lzh_LoopTest : MonoBehaviour
{
	public Lzh_LoopScrollView loopSV1;
	public Lzh_LoopScrollView loopSV2;
	public List<string> iconSpriteNames ;


	// Use this for initialization
	void Start ()
    {
		iconSpriteNames = new List<string> ();
		iconSpriteNames.Add ("Emoticon - Angry");
		iconSpriteNames.Add ("Emoticon - Annoyed");
		iconSpriteNames.Add ("Emoticon - Dead");
		iconSpriteNames.Add ("Emoticon - Frown");
		iconSpriteNames.Add ("Emoticon - Laugh");
		iconSpriteNames.Add ("Emoticon - Rambo");
		iconSpriteNames.Add ("Emoticon - Smile");
		iconSpriteNames.Add ("Emoticon - Smirk");
		iconSpriteNames.Add ("Button A");
		iconSpriteNames.Add ("Button B");
		iconSpriteNames.Add ("Button X");
		iconSpriteNames.Add ("Button Y");
		iconSpriteNames.Add ("Flag-FR");
		iconSpriteNames.Add ("Flag-US");
		iconSpriteNames.Add ("Checkmark");
		iconSpriteNames.Add ("X Mark");


		InitScrollview1 ();
		InitScrollview2 ();
	}


	void InitScrollview1()
	{
		List<Lzh_LoopItemData> datas = new List<Lzh_LoopItemData>();
		for(int i=0; i<100; i++)
		{
			Lzh_LoopItemData data = new Lzh_LoopItemData1(i.ToString());
			datas.Add(data);
		}
		loopSV1.Init(datas, OnItemInit1);
	}

	void OnItemInit1(Lzh_LoopItemObject item, Lzh_LoopItemData data)
	{
		Lzh_LoopItemData1 myData = data as Lzh_LoopItemData1;
		lzh_LoopItemTest1 itemComp = item.widget.GetComponent<lzh_LoopItemTest1> ();
		string front = "item:";
		if(item.dataIndex%3==1)
		{
			front += "\n";
		}
		else if(item.dataIndex%3==2)
		{
			front += "\n\n";
		}
		itemComp.lblName.text = front + myData.name.ToString ();
		item.widget.height = itemComp.lblName.height + 18;
	}


	void InitScrollview2()
	{
		List<Lzh_LoopItemData> datas = new List<Lzh_LoopItemData>();
		for(int i=0; i<100; i++)
		{
			Lzh_LoopItemData data = new Lzh_LoopItemData2(i.ToString(),"");
			datas.Add(data);
		}
		loopSV2.Init(datas, OnItemInit2);
	}

	void OnItemInit2(Lzh_LoopItemObject item, Lzh_LoopItemData data)
	{
		Lzh_LoopItemData2 myData = data as Lzh_LoopItemData2;
		lzh_LoopItemTest2 itemComp = item.widget.GetComponent<lzh_LoopItemTest2> ();
		string font = "item:";

		itemComp.lblName.text = font + myData.name;
		itemComp.icon.spriteName = iconSpriteNames[Random.Range(0,iconSpriteNames.Count)];
	}
}


