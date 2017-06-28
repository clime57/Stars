using System;
using Stars;
[Serializable]
public class SampleExcelFile :IConfig{
   private uint _id;
   private int _intField;
   private string _stringField;
   private bool _boolField;
   private float _floatField;
   public SampleExcelFile (uint type_id,int type_intField,string type_stringField,bool type_boolField,float type_floatField){
     _id =  (uint)type_id;
     _intField =  type_intField;
     _stringField =  type_stringField;
     _boolField =  type_boolField;
     _floatField =  type_floatField;
   }
  /// <summary></summary>
  public uint id{ get { return _id; }}
  /// <summary></summary>
  public int intField{ get { return _intField; }}
  /// <summary></summary>
  public string stringField{ get { return _stringField; }}
  /// <summary></summary>
  public bool boolField{ get { return _boolField; }}
  /// <summary></summary>
  public float floatField{ get { return _floatField; }}
}
