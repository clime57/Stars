using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Mono.Xml;
public class Language :GameData{
	Dictionary<string ,string> list_ = new Dictionary<string,string>();
 
	public void setLanguage(string Language){
		
	}
	
	public string getText(string str){
		if(list_.ContainsKey(str)){
			return list_[str];
		}
        return str;
	}
}