using System;
[Serializable]
public class zhCN :IConfig{
   private uint _id;
   private string _index;
   private string _Content;
   public zhCN (uint type_id,string type_index,string type_Content){
     _id =  (uint)type_id;
     _index =  type_index;
     _Content =  type_Content;
   }
  /// <summary></summary>
  public uint id{ get { return _id; }}
  /// <summary></summary>
  public string index{ get { return _index; }}
  /// <summary></summary>
  public string Content{ get { return _Content; }}
}
