using ET;
using MemoryPack;
using System.Collections.Generic;
namespace ET
{
	[ResponseType(nameof(G2C_Match))]
	[Message(LockStepOuter.C2G_Match)]
	[MemoryPackable]
	public partial class C2G_Match: MessageObject, ISessionRequest
	{
		public static C2G_Match Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(C2G_Match), isFromPool) as C2G_Match; 
		}

		[MemoryPackOrder(0)]
		public int RpcId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.G2C_Match)]
	[MemoryPackable]
	public partial class G2C_Match: MessageObject, ISessionResponse
	{
		public static G2C_Match Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(G2C_Match), isFromPool) as G2C_Match; 
		}

		[MemoryPackOrder(0)]
		public int RpcId { get; set; }

		[MemoryPackOrder(1)]
		public int Error { get; set; }

		[MemoryPackOrder(2)]
		public string Message { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[ResponseType(nameof(G2C_MatchWithAi))]
	[Message(LockStepOuter.C2G_MatchWithAi)]
	[MemoryPackable]
	public partial class C2G_MatchWithAi: MessageObject, ISessionRequest
	{
		public static C2G_MatchWithAi Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(C2G_MatchWithAi), isFromPool) as C2G_MatchWithAi; 
		}

		[MemoryPackOrder(0)]
		public int RpcId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.G2C_MatchWithAi)]
	[MemoryPackable]
	public partial class G2C_MatchWithAi: MessageObject, ISessionResponse
	{
		public static G2C_MatchWithAi Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(G2C_MatchWithAi), isFromPool) as G2C_MatchWithAi; 
		}

		[MemoryPackOrder(0)]
		public int RpcId { get; set; }

		[MemoryPackOrder(1)]
		public int Error { get; set; }

		[MemoryPackOrder(2)]
		public string Message { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

// 匹配成功，通知客户端切换场景
	[Message(LockStepOuter.Match2C_NotifyGameMatchSuccess)]
	[MemoryPackable]
	public partial class Match2C_NotifyGameMatchSuccess: MessageObject, IMessage
	{
		public static Match2C_NotifyGameMatchSuccess Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Match2C_NotifyGameMatchSuccess), isFromPool) as Match2C_NotifyGameMatchSuccess; 
		}

		[MemoryPackOrder(0)]
		public int RpcId { get; set; }

// 房间的ActorId
		[MemoryPackOrder(1)]
		public ActorId ActorId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.ActorId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

// 匹配成功，通知客户端切换场景
	[Message(LockStepOuter.Match2G_NotifyMatchSuccess)]
	[MemoryPackable]
	public partial class Match2G_NotifyMatchSuccess: MessageObject, IMessage
	{
		public static Match2G_NotifyMatchSuccess Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Match2G_NotifyMatchSuccess), isFromPool) as Match2G_NotifyMatchSuccess; 
		}

		[MemoryPackOrder(0)]
		public int RpcId { get; set; }

// 房间的ActorId
		[MemoryPackOrder(1)]
		public ActorId ActorId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.ActorId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

// 客户端通知房间切换场景完成
	[Message(LockStepOuter.C2Room_ChangeSceneFinish)]
	[MemoryPackable]
	public partial class C2Room_ChangeSceneFinish: MessageObject, IRoomMessage
	{
		public static C2Room_ChangeSceneFinish Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(C2Room_ChangeSceneFinish), isFromPool) as C2Room_ChangeSceneFinish; 
		}

		[MemoryPackOrder(0)]
		public long PlayerId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.PlayerId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.C2Room_ChangeGameSceneFinish)]
	[MemoryPackable]
	public partial class C2Room_ChangeGameSceneFinish: MessageObject, IRoomMessage
	{
		public static C2Room_ChangeGameSceneFinish Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(C2Room_ChangeGameSceneFinish), isFromPool) as C2Room_ChangeGameSceneFinish; 
		}

		[MemoryPackOrder(0)]
		public long PlayerId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.PlayerId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.C2Room_SelectCard)]
	[MemoryPackable]
	public partial class C2Room_SelectCard: MessageObject, IRoomMessage
	{
		public static C2Room_SelectCard Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(C2Room_SelectCard), isFromPool) as C2Room_SelectCard; 
		}

		[MemoryPackOrder(0)]
		public long PlayerId { get; set; }

		[MemoryPackOrder(1)]
		public long CardId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.PlayerId = default;
			this.CardId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.CardGameUnitInfo)]
	[MemoryPackable]
	public partial class CardGameUnitInfo: MessageObject
	{
		public static CardGameUnitInfo Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(CardGameUnitInfo), isFromPool) as CardGameUnitInfo; 
		}

		[MemoryPackOrder(0)]
		public long PlayerId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.PlayerId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.LockStepUnitInfo)]
	[MemoryPackable]
	public partial class LockStepUnitInfo: MessageObject
	{
		public static LockStepUnitInfo Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(LockStepUnitInfo), isFromPool) as LockStepUnitInfo; 
		}

		[MemoryPackOrder(0)]
		public long PlayerId { get; set; }

		[MemoryPackOrder(1)]
		public TrueSync.TSVector Position { get; set; }

		[MemoryPackOrder(2)]
		public TrueSync.TSQuaternion Rotation { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.PlayerId = default;
			this.Position = default;
			this.Rotation = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

// 房间通知客户端进入战斗
	[Message(LockStepOuter.Room2C_Start)]
	[MemoryPackable]
	public partial class Room2C_Start: MessageObject, IMessage
	{
		public static Room2C_Start Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Room2C_Start), isFromPool) as Room2C_Start; 
		}

		[MemoryPackOrder(0)]
		public long StartTime { get; set; }

		[MemoryPackOrder(1)]
		public List<LockStepUnitInfo> UnitInfo { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.StartTime = default;
			this.UnitInfo.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.Room2C_CGStart)]
	[MemoryPackable]
	public partial class Room2C_CGStart: MessageObject, IMessage
	{
		public static Room2C_CGStart Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Room2C_CGStart), isFromPool) as Room2C_CGStart; 
		}

		[MemoryPackOrder(0)]
		public long StartTime { get; set; }

		[MemoryPackOrder(1)]
		public List<CardGameUnitInfo> UnitInfo { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.StartTime = default;
			this.UnitInfo.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.FrameMessage)]
	[MemoryPackable]
	public partial class FrameMessage: MessageObject, IMessage
	{
		public static FrameMessage Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(FrameMessage), isFromPool) as FrameMessage; 
		}

		[MemoryPackOrder(0)]
		public int Frame { get; set; }

		[MemoryPackOrder(1)]
		public long PlayerId { get; set; }

		[MemoryPackOrder(2)]
		public LSInput Input { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.Frame = default;
			this.PlayerId = default;
			this.Input = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.OneFrameInputs)]
	[MemoryPackable]
	public partial class OneFrameInputs: MessageObject, IMessage
	{
		public static OneFrameInputs Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(OneFrameInputs), isFromPool) as OneFrameInputs; 
		}

		[MongoDB.Bson.Serialization.Attributes.BsonDictionaryOptions(MongoDB.Bson.Serialization.Options.DictionaryRepresentation.ArrayOfArrays)]
		[MemoryPackOrder(1)]
		public Dictionary<long, LSInput> Inputs { get; set; } = new();
		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.Inputs.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.Room2C_AdjustUpdateTime)]
	[MemoryPackable]
	public partial class Room2C_AdjustUpdateTime: MessageObject, IMessage
	{
		public static Room2C_AdjustUpdateTime Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Room2C_AdjustUpdateTime), isFromPool) as Room2C_AdjustUpdateTime; 
		}

		[MemoryPackOrder(0)]
		public int DiffTime { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.DiffTime = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.C2Room_CheckHash)]
	[MemoryPackable]
	public partial class C2Room_CheckHash: MessageObject, IRoomMessage
	{
		public static C2Room_CheckHash Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(C2Room_CheckHash), isFromPool) as C2Room_CheckHash; 
		}

		[MemoryPackOrder(0)]
		public long PlayerId { get; set; }

		[MemoryPackOrder(1)]
		public int Frame { get; set; }

		[MemoryPackOrder(2)]
		public long Hash { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.PlayerId = default;
			this.Frame = default;
			this.Hash = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.Room2C_CheckHashFail)]
	[MemoryPackable]
	public partial class Room2C_CheckHashFail: MessageObject, IMessage
	{
		public static Room2C_CheckHashFail Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Room2C_CheckHashFail), isFromPool) as Room2C_CheckHashFail; 
		}

		[MemoryPackOrder(0)]
		public int Frame { get; set; }

		[MemoryPackOrder(1)]
		public byte[] LSWorldBytes { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.Frame = default;
			this.LSWorldBytes = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.G2C_Reconnect)]
	[MemoryPackable]
	public partial class G2C_Reconnect: MessageObject, IMessage
	{
		public static G2C_Reconnect Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(G2C_Reconnect), isFromPool) as G2C_Reconnect; 
		}

		[MemoryPackOrder(0)]
		public long StartTime { get; set; }

		[MemoryPackOrder(1)]
		public List<LockStepUnitInfo> UnitInfos { get; set; } = new();

		[MemoryPackOrder(2)]
		public int Frame { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.StartTime = default;
			this.UnitInfos.Clear();
			this.Frame = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.RoomCardInfo)]
	[MemoryPackable]
	public partial class RoomCardInfo: MessageObject
	{
		public static RoomCardInfo Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(RoomCardInfo), isFromPool) as RoomCardInfo; 
		}

		[MemoryPackOrder(0)]
		public long CardId { get; set; }

		[MemoryPackOrder(1)]
		public int BaseId { get; set; }

		[MemoryPackOrder(2)]
		public int Type { get; set; }

		[MemoryPackOrder(3)]
		public int Attack { get; set; }

		[MemoryPackOrder(4)]
		public int HP { get; set; }

		[MemoryPackOrder(5)]
		public int Cost { get; set; }

		[MemoryPackOrder(6)]
		public int Red { get; set; }

		[MemoryPackOrder(7)]
		public int Blue { get; set; }

		[MemoryPackOrder(8)]
		public int White { get; set; }

		[MemoryPackOrder(9)]
		public int Green { get; set; }

		[MemoryPackOrder(10)]
		public int Black { get; set; }

		[MemoryPackOrder(11)]
		public int Grey { get; set; }

		[MemoryPackOrder(12)]
		public bool CantBeAttackTarget { get; set; }

		[MemoryPackOrder(13)]
		public bool CantBeMagicTarget { get; set; }

		[MemoryPackOrder(14)]
		public int UseCardType { get; set; }

		[MemoryPackOrder(15)]
		public int CardType { get; set; }

		[MemoryPackOrder(16)]
		public int AttackCount { get; set; }

		[MemoryPackOrder(17)]
		public List<int> CardPowers { get; set; } = new();

		[MemoryPackOrder(18)]
		public int Order { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.CardId = default;
			this.BaseId = default;
			this.Type = default;
			this.Attack = default;
			this.HP = default;
			this.Cost = default;
			this.Red = default;
			this.Blue = default;
			this.White = default;
			this.Green = default;
			this.Black = default;
			this.Grey = default;
			this.CantBeAttackTarget = default;
			this.CantBeMagicTarget = default;
			this.UseCardType = default;
			this.CardType = default;
			this.AttackCount = default;
			this.CardPowers.Clear();
			this.Order = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.Room2C_GetHandCardFromGroup)]
	[MemoryPackable]
	public partial class Room2C_GetHandCardFromGroup: MessageObject, IMessage
	{
		public static Room2C_GetHandCardFromGroup Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Room2C_GetHandCardFromGroup), isFromPool) as Room2C_GetHandCardFromGroup; 
		}

		[MemoryPackOrder(0)]
		public RoomCardInfo CardInfo { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.CardInfo = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.Room2C_EnemyGetHandCardFromGroup)]
	[MemoryPackable]
	public partial class Room2C_EnemyGetHandCardFromGroup: MessageObject, IMessage
	{
		public static Room2C_EnemyGetHandCardFromGroup Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Room2C_EnemyGetHandCardFromGroup), isFromPool) as Room2C_EnemyGetHandCardFromGroup; 
		}

		[MemoryPackOrder(0)]
		public RoomCardInfo CardInfo { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.CardInfo = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.Room2C_GetCard)]
	[MemoryPackable]
	public partial class Room2C_GetCard: MessageObject, IMessage
	{
		public static Room2C_GetCard Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Room2C_GetCard), isFromPool) as Room2C_GetCard; 
		}

		[MemoryPackOrder(0)]
		public RoomCardInfo CardInfo { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.CardInfo = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.Room2C_EnemyGetCard)]
	[MemoryPackable]
	public partial class Room2C_EnemyGetCard: MessageObject, IMessage
	{
		public static Room2C_EnemyGetCard Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Room2C_EnemyGetCard), isFromPool) as Room2C_EnemyGetCard; 
		}

		[MemoryPackOrder(0)]
		public RoomCardInfo CardInfo { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.CardInfo = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.Room2C_MyGroupCount)]
	[MemoryPackable]
	public partial class Room2C_MyGroupCount: MessageObject, IMessage
	{
		public static Room2C_MyGroupCount Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Room2C_MyGroupCount), isFromPool) as Room2C_MyGroupCount; 
		}

		[MemoryPackOrder(0)]
		public int Count { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.Count = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.Room2C_EnemyGroupCount)]
	[MemoryPackable]
	public partial class Room2C_EnemyGroupCount: MessageObject, IMessage
	{
		public static Room2C_EnemyGroupCount Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Room2C_EnemyGroupCount), isFromPool) as Room2C_EnemyGroupCount; 
		}

		[MemoryPackOrder(0)]
		public int Count { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.Count = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.C2Room_TurnOver)]
	[MemoryPackable]
	public partial class C2Room_TurnOver: MessageObject, IRoomMessage
	{
		public static C2Room_TurnOver Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(C2Room_TurnOver), isFromPool) as C2Room_TurnOver; 
		}

		[MemoryPackOrder(0)]
		public long PlayerId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.PlayerId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.Room2C_TurnStart)]
	[MemoryPackable]
	public partial class Room2C_TurnStart: MessageObject, IMessage
	{
		public static Room2C_TurnStart Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Room2C_TurnStart), isFromPool) as Room2C_TurnStart; 
		}

		[MemoryPackOrder(0)]
		public bool IsThisClient { get; set; }

		[MemoryPackOrder(1)]
		public int Cost { get; set; }

		[MemoryPackOrder(2)]
		public int CostD { get; set; }

		[MemoryPackOrder(3)]
		public int Red { get; set; }

		[MemoryPackOrder(4)]
		public int Blue { get; set; }

		[MemoryPackOrder(5)]
		public int Green { get; set; }

		[MemoryPackOrder(6)]
		public int White { get; set; }

		[MemoryPackOrder(7)]
		public int Black { get; set; }

		[MemoryPackOrder(8)]
		public int Grey { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.IsThisClient = default;
			this.Cost = default;
			this.CostD = default;
			this.Red = default;
			this.Blue = default;
			this.Green = default;
			this.White = default;
			this.Black = default;
			this.Grey = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.Room2C_NewHero)]
	[MemoryPackable]
	public partial class Room2C_NewHero: MessageObject, IMessage
	{
		public static Room2C_NewHero Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Room2C_NewHero), isFromPool) as Room2C_NewHero; 
		}

		[MemoryPackOrder(0)]
		public RoomCardInfo Hero { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.Hero = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.Room2C_NewAgent)]
	[MemoryPackable]
	public partial class Room2C_NewAgent: MessageObject, IMessage
	{
		public static Room2C_NewAgent Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Room2C_NewAgent), isFromPool) as Room2C_NewAgent; 
		}

		[MemoryPackOrder(0)]
		public RoomCardInfo Agent1 { get; set; }

		[MemoryPackOrder(1)]
		public RoomCardInfo Agent2 { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.Agent1 = default;
			this.Agent2 = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.Room2C_EnemyNewHero)]
	[MemoryPackable]
	public partial class Room2C_EnemyNewHero: MessageObject, IMessage
	{
		public static Room2C_EnemyNewHero Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Room2C_EnemyNewHero), isFromPool) as Room2C_EnemyNewHero; 
		}

		[MemoryPackOrder(0)]
		public RoomCardInfo Hero { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.Hero = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.Room2C_EnemyNewAgent)]
	[MemoryPackable]
	public partial class Room2C_EnemyNewAgent: MessageObject, IMessage
	{
		public static Room2C_EnemyNewAgent Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Room2C_EnemyNewAgent), isFromPool) as Room2C_EnemyNewAgent; 
		}

		[MemoryPackOrder(0)]
		public RoomCardInfo Agent1 { get; set; }

		[MemoryPackOrder(1)]
		public RoomCardInfo Agent2 { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.Agent1 = default;
			this.Agent2 = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.Room2C_GroupCount)]
	[MemoryPackable]
	public partial class Room2C_GroupCount: MessageObject, IMessage
	{
		public static Room2C_GroupCount Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Room2C_GroupCount), isFromPool) as Room2C_GroupCount; 
		}

		[MemoryPackOrder(0)]
		public int Count { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.Count = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.Room2C_CardBoom)]
	[MemoryPackable]
	public partial class Room2C_CardBoom: MessageObject, IMessage
	{
		public static Room2C_CardBoom Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Room2C_CardBoom), isFromPool) as Room2C_CardBoom; 
		}

		[MemoryPackOrder(0)]
		public RoomCardInfo Card { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.Card = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.Room2C_EnemyCardBoom)]
	[MemoryPackable]
	public partial class Room2C_EnemyCardBoom: MessageObject, IMessage
	{
		public static Room2C_EnemyCardBoom Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Room2C_EnemyCardBoom), isFromPool) as Room2C_EnemyCardBoom; 
		}

		[MemoryPackOrder(0)]
		public RoomCardInfo Card { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.Card = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.C2Room_Attack)]
	[MemoryPackable]
	public partial class C2Room_Attack: MessageObject, IRoomMessage
	{
		public static C2Room_Attack Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(C2Room_Attack), isFromPool) as C2Room_Attack; 
		}

		[MemoryPackOrder(0)]
		public long PlayerId { get; set; }

		[MemoryPackOrder(1)]
		public long Actor { get; set; }

		[MemoryPackOrder(2)]
		public long Target { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.PlayerId = default;
			this.Actor = default;
			this.Target = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.Room2C_Attack)]
	[MemoryPackable]
	public partial class Room2C_Attack: MessageObject, IMessage
	{
		public static Room2C_Attack Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Room2C_Attack), isFromPool) as Room2C_Attack; 
		}

		[MemoryPackOrder(0)]
		public long Actor { get; set; }

		[MemoryPackOrder(1)]
		public long Target { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.Actor = default;
			this.Target = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.C2Room_UseCard)]
	[MemoryPackable]
	public partial class C2Room_UseCard: MessageObject, IRoomMessage
	{
		public static C2Room_UseCard Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(C2Room_UseCard), isFromPool) as C2Room_UseCard; 
		}

		[MemoryPackOrder(0)]
		public long PlayerId { get; set; }

		[MemoryPackOrder(1)]
		public long Card { get; set; }

		[MemoryPackOrder(2)]
		public long Target { get; set; }

		[MemoryPackOrder(3)]
		public int Pos { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.PlayerId = default;
			this.Card = default;
			this.Target = default;
			this.Pos = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.Room2C_UseCard)]
	[MemoryPackable]
	public partial class Room2C_UseCard: MessageObject, IMessage
	{
		public static Room2C_UseCard Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Room2C_UseCard), isFromPool) as Room2C_UseCard; 
		}

		[MemoryPackOrder(0)]
		public RoomCardInfo Card { get; set; }

		[MemoryPackOrder(1)]
		public bool IsThisClient { get; set; }

		[MemoryPackOrder(2)]
		public RoomCardInfo Target { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.Card = default;
			this.IsThisClient = default;
			this.Target = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.Room2C_OperateFail)]
	[MemoryPackable]
	public partial class Room2C_OperateFail: MessageObject, IMessage
	{
		public static Room2C_OperateFail Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Room2C_OperateFail), isFromPool) as Room2C_OperateFail; 
		}

		[MemoryPackOrder(0)]
		public int FailId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.FailId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.C2Room_Effect)]
	[MemoryPackable]
	public partial class C2Room_Effect: MessageObject, IRoomMessage
	{
		public static C2Room_Effect Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(C2Room_Effect), isFromPool) as C2Room_Effect; 
		}

		[MemoryPackOrder(0)]
		public long PlayerId { get; set; }

		[MemoryPackOrder(1)]
		public int EffectType { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.PlayerId = default;
			this.EffectType = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.Room2C_Effect)]
	[MemoryPackable]
	public partial class Room2C_Effect: MessageObject, IMessage
	{
		public static Room2C_Effect Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Room2C_Effect), isFromPool) as Room2C_Effect; 
		}

		[MemoryPackOrder(0)]
		public int EffectType { get; set; }

		[MemoryPackOrder(1)]
		public bool IsThisClient { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.EffectType = default;
			this.IsThisClient = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.C2Room_EffectToCard)]
	[MemoryPackable]
	public partial class C2Room_EffectToCard: MessageObject, IRoomMessage
	{
		public static C2Room_EffectToCard Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(C2Room_EffectToCard), isFromPool) as C2Room_EffectToCard; 
		}

		[MemoryPackOrder(0)]
		public long PlayerId { get; set; }

		[MemoryPackOrder(1)]
		public int EffectType { get; set; }

		[MemoryPackOrder(2)]
		public RoomCardInfo Card { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.PlayerId = default;
			this.EffectType = default;
			this.Card = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.Room2C_EffectToCard)]
	[MemoryPackable]
	public partial class Room2C_EffectToCard: MessageObject, IMessage
	{
		public static Room2C_EffectToCard Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Room2C_EffectToCard), isFromPool) as Room2C_EffectToCard; 
		}

		[MemoryPackOrder(0)]
		public int EffectType { get; set; }

		[MemoryPackOrder(1)]
		public RoomCardInfo Card { get; set; }

		[MemoryPackOrder(2)]
		public bool IsThisClient { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.EffectType = default;
			this.Card = default;
			this.IsThisClient = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.C2Room_EffectToCards)]
	[MemoryPackable]
	public partial class C2Room_EffectToCards: MessageObject, IRoomMessage
	{
		public static C2Room_EffectToCards Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(C2Room_EffectToCards), isFromPool) as C2Room_EffectToCards; 
		}

		[MemoryPackOrder(0)]
		public long PlayerId { get; set; }

		[MemoryPackOrder(1)]
		public int EffectType { get; set; }

		[MemoryPackOrder(2)]
		public List<RoomCardInfo> Card { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.PlayerId = default;
			this.EffectType = default;
			this.Card.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.Room2C_EffectToCards)]
	[MemoryPackable]
	public partial class Room2C_EffectToCards: MessageObject, IMessage
	{
		public static Room2C_EffectToCards Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Room2C_EffectToCards), isFromPool) as Room2C_EffectToCards; 
		}

		[MemoryPackOrder(0)]
		public int EffectType { get; set; }

		[MemoryPackOrder(1)]
		public List<RoomCardInfo> Card { get; set; } = new();

		[MemoryPackOrder(2)]
		public bool IsThisClient { get; set; }

		[MemoryPackOrder(3)]
		public int Num1 { get; set; }

		[MemoryPackOrder(4)]
		public int Num2 { get; set; }

		[MemoryPackOrder(5)]
		public int Num3 { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.EffectType = default;
			this.Card.Clear();
			this.IsThisClient = default;
			this.Num1 = default;
			this.Num2 = default;
			this.Num3 = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.C2Room_EffectCardToCard)]
	[MemoryPackable]
	public partial class C2Room_EffectCardToCard: MessageObject, IRoomMessage
	{
		public static C2Room_EffectCardToCard Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(C2Room_EffectCardToCard), isFromPool) as C2Room_EffectCardToCard; 
		}

		[MemoryPackOrder(0)]
		public long PlayerId { get; set; }

		[MemoryPackOrder(1)]
		public int EffectType { get; set; }

		[MemoryPackOrder(2)]
		public RoomCardInfo Actor { get; set; }

		[MemoryPackOrder(3)]
		public RoomCardInfo Target { get; set; }

		[MemoryPackOrder(3)]
		public int Num1 { get; set; }

		[MemoryPackOrder(4)]
		public int Num2 { get; set; }

		[MemoryPackOrder(5)]
		public int Num3 { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.PlayerId = default;
			this.EffectType = default;
			this.Actor = default;
			this.Target = default;
			this.Num1 = default;
			this.Num2 = default;
			this.Num3 = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.Room2C_GetHandCardFromShowCard)]
	[MemoryPackable]
	public partial class Room2C_GetHandCardFromShowCard: MessageObject, IMessage
	{
		public static Room2C_GetHandCardFromShowCard Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Room2C_GetHandCardFromShowCard), isFromPool) as Room2C_GetHandCardFromShowCard; 
		}

		[MemoryPackOrder(0)]
		public long CardId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.CardId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.Room2C_EnemyGetHandCardFromShowCard)]
	[MemoryPackable]
	public partial class Room2C_EnemyGetHandCardFromShowCard: MessageObject, IMessage
	{
		public static Room2C_EnemyGetHandCardFromShowCard Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Room2C_EnemyGetHandCardFromShowCard), isFromPool) as Room2C_EnemyGetHandCardFromShowCard; 
		}

		[MemoryPackOrder(0)]
		public long CardId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.CardId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.Room2C_EffectCardToCard)]
	[MemoryPackable]
	public partial class Room2C_EffectCardToCard: MessageObject, IMessage
	{
		public static Room2C_EffectCardToCard Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Room2C_EffectCardToCard), isFromPool) as Room2C_EffectCardToCard; 
		}

		[MemoryPackOrder(0)]
		public int EffectType { get; set; }

		[MemoryPackOrder(1)]
		public RoomCardInfo Actor { get; set; }

		[MemoryPackOrder(2)]
		public RoomCardInfo Target { get; set; }

		[MemoryPackOrder(3)]
		public int Num1 { get; set; }

		[MemoryPackOrder(4)]
		public int Num2 { get; set; }

		[MemoryPackOrder(5)]
		public int Num3 { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.EffectType = default;
			this.Actor = default;
			this.Target = default;
			this.Num1 = default;
			this.Num2 = default;
			this.Num3 = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.Room2C_CallUnit)]
	[MemoryPackable]
	public partial class Room2C_CallUnit: MessageObject, IMessage
	{
		public static Room2C_CallUnit Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Room2C_CallUnit), isFromPool) as Room2C_CallUnit; 
		}

		[MemoryPackOrder(0)]
		public RoomCardInfo Card { get; set; }

		[MemoryPackOrder(1)]
		public List<long> UnitOrder { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.Card = default;
			this.UnitOrder.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.Room2C_EnemyCallUnit)]
	[MemoryPackable]
	public partial class Room2C_EnemyCallUnit: MessageObject, IMessage
	{
		public static Room2C_EnemyCallUnit Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Room2C_EnemyCallUnit), isFromPool) as Room2C_EnemyCallUnit; 
		}

		[MemoryPackOrder(0)]
		public RoomCardInfo Card { get; set; }

		[MemoryPackOrder(1)]
		public List<long> UnitOrder { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.Card = default;
			this.UnitOrder.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.Room2C_CardGetDamage)]
	[MemoryPackable]
	public partial class Room2C_CardGetDamage: MessageObject, IMessage
	{
		public static Room2C_CardGetDamage Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Room2C_CardGetDamage), isFromPool) as Room2C_CardGetDamage; 
		}

		[MemoryPackOrder(0)]
		public RoomCardInfo Card { get; set; }

		[MemoryPackOrder(1)]
		public int hurt { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.Card = default;
			this.hurt = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.Room2C_Cost)]
	[MemoryPackable]
	public partial class Room2C_Cost: MessageObject, IMessage
	{
		public static Room2C_Cost Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Room2C_Cost), isFromPool) as Room2C_Cost; 
		}

		[MemoryPackOrder(0)]
		public int Now { get; set; }

		[MemoryPackOrder(1)]
		public int Max { get; set; }

		[MemoryPackOrder(2)]
		public bool IsMy { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.Now = default;
			this.Max = default;
			this.IsMy = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.Room2C_GetColor)]
	[MemoryPackable]
	public partial class Room2C_GetColor: MessageObject, IMessage
	{
		public static Room2C_GetColor Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Room2C_GetColor), isFromPool) as Room2C_GetColor; 
		}

		[MemoryPackOrder(0)]
		public int Color { get; set; }

		[MemoryPackOrder(1)]
		public int Num { get; set; }

		[MemoryPackOrder(2)]
		public bool IsMy { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.Color = default;
			this.Num = default;
			this.IsMy = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.Room2C_LoseHandCard)]
	[MemoryPackable]
	public partial class Room2C_LoseHandCard: MessageObject, IMessage
	{
		public static Room2C_LoseHandCard Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Room2C_LoseHandCard), isFromPool) as Room2C_LoseHandCard; 
		}

		[MemoryPackOrder(0)]
		public long CardId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.CardId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.Room2C_UnitDead)]
	[MemoryPackable]
	public partial class Room2C_UnitDead: MessageObject, IMessage
	{
		public static Room2C_UnitDead Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Room2C_UnitDead), isFromPool) as Room2C_UnitDead; 
		}

		[MemoryPackOrder(0)]
		public List<long> CardIds { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.CardIds.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.Room2C_AttackCountEnough)]
	[MemoryPackable]
	public partial class Room2C_AttackCountEnough: MessageObject, IMessage
	{
		public static Room2C_AttackCountEnough Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Room2C_AttackCountEnough), isFromPool) as Room2C_AttackCountEnough; 
		}

		[MemoryPackOrder(0)]
		public long CardId { get; set; }

		[MemoryPackOrder(1)]
		public bool AttackCountEnough { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.CardId = default;
			this.AttackCountEnough = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.Room2C_CardsGetDamage)]
	[MemoryPackable]
	public partial class Room2C_CardsGetDamage: MessageObject, IMessage
	{
		public static Room2C_CardsGetDamage Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Room2C_CardsGetDamage), isFromPool) as Room2C_CardsGetDamage; 
		}

		[MemoryPackOrder(0)]
		public List<RoomCardInfo> Card { get; set; } = new();

		[MemoryPackOrder(1)]
		public List<int> hurt { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.Card.Clear();
			this.hurt.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.Room2C_CallUnits)]
	[MemoryPackable]
	public partial class Room2C_CallUnits: MessageObject, IMessage
	{
		public static Room2C_CallUnits Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Room2C_CallUnits), isFromPool) as Room2C_CallUnits; 
		}

		[MemoryPackOrder(0)]
		public List<RoomCardInfo> Units { get; set; } = new();

		[MemoryPackOrder(1)]
		public List<long> Order { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.Units.Clear();
			this.Order.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.Room2C_EnemyCallUnits)]
	[MemoryPackable]
	public partial class Room2C_EnemyCallUnits: MessageObject, IMessage
	{
		public static Room2C_EnemyCallUnits Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Room2C_EnemyCallUnits), isFromPool) as Room2C_EnemyCallUnits; 
		}

		[MemoryPackOrder(0)]
		public List<RoomCardInfo> Units { get; set; } = new();

		[MemoryPackOrder(1)]
		public List<long> Order { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.Units.Clear();
			this.Order.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.Room2C_FlashMyUnit)]
	[MemoryPackable]
	public partial class Room2C_FlashMyUnit: MessageObject, IMessage
	{
		public static Room2C_FlashMyUnit Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Room2C_FlashMyUnit), isFromPool) as Room2C_FlashMyUnit; 
		}

		[MemoryPackOrder(0)]
		public List<RoomCardInfo> Units { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.Units.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.Room2C_FlashEnemyUnit)]
	[MemoryPackable]
	public partial class Room2C_FlashEnemyUnit: MessageObject, IMessage
	{
		public static Room2C_FlashEnemyUnit Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Room2C_FlashEnemyUnit), isFromPool) as Room2C_FlashEnemyUnit; 
		}

		[MemoryPackOrder(0)]
		public List<RoomCardInfo> Units { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.Units.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.Room2C_FlashUnit)]
	[MemoryPackable]
	public partial class Room2C_FlashUnit: MessageObject, IMessage
	{
		public static Room2C_FlashUnit Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Room2C_FlashUnit), isFromPool) as Room2C_FlashUnit; 
		}

		[MemoryPackOrder(0)]
		public RoomCardInfo Units { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.Units = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.Room2C_FindCardsToShow)]
	[MemoryPackable]
	public partial class Room2C_FindCardsToShow: MessageObject, IMessage
	{
		public static Room2C_FindCardsToShow Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Room2C_FindCardsToShow), isFromPool) as Room2C_FindCardsToShow; 
		}

		[MemoryPackOrder(0)]
		public List<RoomCardInfo> Cards { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.Cards.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.Room2C_GetHandCards)]
	[MemoryPackable]
	public partial class Room2C_GetHandCards: MessageObject, IMessage
	{
		public static Room2C_GetHandCards Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Room2C_GetHandCards), isFromPool) as Room2C_GetHandCards; 
		}

		[MemoryPackOrder(0)]
		public List<RoomCardInfo> Cards { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.Cards.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(LockStepOuter.Room2C_EnemyGetHandCards)]
	[MemoryPackable]
	public partial class Room2C_EnemyGetHandCards: MessageObject, IMessage
	{
		public static Room2C_EnemyGetHandCards Create(bool isFromPool = true) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Room2C_EnemyGetHandCards), isFromPool) as Room2C_EnemyGetHandCards; 
		}

		[MemoryPackOrder(0)]
		public List<RoomCardInfo> Cards { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.Cards.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	public static class LockStepOuter
	{
		 public const ushort C2G_Match = 11002;
		 public const ushort G2C_Match = 11003;
		 public const ushort C2G_MatchWithAi = 11004;
		 public const ushort G2C_MatchWithAi = 11005;
		 public const ushort Match2C_NotifyGameMatchSuccess = 11006;
		 public const ushort Match2G_NotifyMatchSuccess = 11007;
		 public const ushort C2Room_ChangeSceneFinish = 11008;
		 public const ushort C2Room_ChangeGameSceneFinish = 11009;
		 public const ushort C2Room_SelectCard = 11010;
		 public const ushort CardGameUnitInfo = 11011;
		 public const ushort LockStepUnitInfo = 11012;
		 public const ushort Room2C_Start = 11013;
		 public const ushort Room2C_CGStart = 11014;
		 public const ushort FrameMessage = 11015;
		 public const ushort OneFrameInputs = 11016;
		 public const ushort Room2C_AdjustUpdateTime = 11017;
		 public const ushort C2Room_CheckHash = 11018;
		 public const ushort Room2C_CheckHashFail = 11019;
		 public const ushort G2C_Reconnect = 11020;
		 public const ushort RoomCardInfo = 11021;
		 public const ushort Room2C_GetHandCardFromGroup = 11022;
		 public const ushort Room2C_EnemyGetHandCardFromGroup = 11023;
		 public const ushort Room2C_GetCard = 11024;
		 public const ushort Room2C_EnemyGetCard = 11025;
		 public const ushort Room2C_MyGroupCount = 11026;
		 public const ushort Room2C_EnemyGroupCount = 11027;
		 public const ushort C2Room_TurnOver = 11028;
		 public const ushort Room2C_TurnStart = 11029;
		 public const ushort Room2C_NewHero = 11030;
		 public const ushort Room2C_NewAgent = 11031;
		 public const ushort Room2C_EnemyNewHero = 11032;
		 public const ushort Room2C_EnemyNewAgent = 11033;
		 public const ushort Room2C_GroupCount = 11034;
		 public const ushort Room2C_CardBoom = 11035;
		 public const ushort Room2C_EnemyCardBoom = 11036;
		 public const ushort C2Room_Attack = 11037;
		 public const ushort Room2C_Attack = 11038;
		 public const ushort C2Room_UseCard = 11039;
		 public const ushort Room2C_UseCard = 11040;
		 public const ushort Room2C_OperateFail = 11041;
		 public const ushort C2Room_Effect = 11042;
		 public const ushort Room2C_Effect = 11043;
		 public const ushort C2Room_EffectToCard = 11044;
		 public const ushort Room2C_EffectToCard = 11045;
		 public const ushort C2Room_EffectToCards = 11046;
		 public const ushort Room2C_EffectToCards = 11047;
		 public const ushort C2Room_EffectCardToCard = 11048;
		 public const ushort Room2C_GetHandCardFromShowCard = 11049;
		 public const ushort Room2C_EnemyGetHandCardFromShowCard = 11050;
		 public const ushort Room2C_EffectCardToCard = 11051;
		 public const ushort Room2C_CallUnit = 11052;
		 public const ushort Room2C_EnemyCallUnit = 11053;
		 public const ushort Room2C_CardGetDamage = 11054;
		 public const ushort Room2C_Cost = 11055;
		 public const ushort Room2C_GetColor = 11056;
		 public const ushort Room2C_LoseHandCard = 11057;
		 public const ushort Room2C_UnitDead = 11058;
		 public const ushort Room2C_AttackCountEnough = 11059;
		 public const ushort Room2C_CardsGetDamage = 11060;
		 public const ushort Room2C_CallUnits = 11061;
		 public const ushort Room2C_EnemyCallUnits = 11062;
		 public const ushort Room2C_FlashMyUnit = 11063;
		 public const ushort Room2C_FlashEnemyUnit = 11064;
		 public const ushort Room2C_FlashUnit = 11065;
		 public const ushort Room2C_FindCardsToShow = 11066;
		 public const ushort Room2C_GetHandCards = 11067;
		 public const ushort Room2C_EnemyGetHandCards = 11068;
	}
}
