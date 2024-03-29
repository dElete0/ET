namespace ET {
    public static class RoomMessageMsg {
        
    }

    public static class CardGameMsg {
        public const int UnitMax = 7;
        public const int GroupMax = 50;
        public const int HandCardsCountMax = 10;
        public const int CostMax = 12;
    }

    public enum Room2C_OperateFailType {
        None = 0,
        //当前无法行动
        CantOperateNow = 1,
        //费用不足
        NotEnoughCost = 2,
        CantGetMoreUnit = 3,
        MustAttackTaunt = 4,
    }
}