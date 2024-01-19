using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.ComponentModel;

namespace ET
{
    [Config]
    public partial class CardConfigCategory : Singleton<CardConfigCategory>, IMerge
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private Dictionary<int, CardConfig> dict = new();
		
        public void Merge(object o)
        {
            CardConfigCategory s = o as CardConfigCategory;
            foreach (var kv in s.dict)
            {
                this.dict.Add(kv.Key, kv.Value);
            }
        }
		
        public CardConfig Get(int id)
        {
            this.dict.TryGetValue(id, out CardConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (CardConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, CardConfig> GetAll()
        {
            return this.dict;
        }

        public CardConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

	public partial class CardConfig: ProtoObject, IConfig
	{
		/// <summary>Id</summary>
		public int Id { get; set; }
		/// <summary>Type</summary>
		public int Type { get; set; }
		/// <summary>衍生</summary>
		public int Derive { get; set; }
		/// <summary>名字</summary>
		public string Name { get; set; }
		/// <summary>描述</summary>
		public string Desc { get; set; }
		/// <summary></summary>
		public int Target { get; set; }
		/// <summary></summary>
		public int Cost { get; set; }
		/// <summary></summary>
		public int Attack { get; set; }
		/// <summary></summary>
		public int HP { get; set; }
		/// <summary></summary>
		public int Red { get; set; }
		/// <summary></summary>
		public int Blue { get; set; }
		/// <summary></summary>
		public int White { get; set; }
		/// <summary></summary>
		public int Green { get; set; }
		/// <summary></summary>
		public int Black { get; set; }
		/// <summary></summary>
		public int Grey { get; set; }
		/// <summary>效果编号</summary>
		public int Effect1Type { get; set; }
		/// <summary></summary>
		public int Effect1From { get; set; }
		/// <summary>魔法伤害值//召唤单位BaseId</summary>
		public int Effect1Num1 { get; set; }
		/// <summary>召唤单位数量</summary>
		public int Effect1Num2 { get; set; }
		/// <summary></summary>
		public int Effect1Num3 { get; set; }
		/// <summary>效果编号</summary>
		public int Effect2Type { get; set; }
		/// <summary></summary>
		public int Effect2From { get; set; }
		/// <summary></summary>
		public int Effect2Num1 { get; set; }
		/// <summary></summary>
		public int Effect2Num2 { get; set; }
		/// <summary></summary>
		public int Effect2Num3 { get; set; }
		/// <summary>效果编号</summary>
		public int Effect3Type { get; set; }
		/// <summary></summary>
		public int Effect3From { get; set; }
		/// <summary></summary>
		public int Effect3Num1 { get; set; }
		/// <summary></summary>
		public int Effect3Num2 { get; set; }
		/// <summary></summary>
		public int Effect3Num3 { get; set; }

	}
}
