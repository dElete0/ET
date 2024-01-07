namespace ET
{
    public static class CGConstValue
    {
        public const int MatchCount = 1;
        public const int UpdateInterval = 50;
        public const int FrameCountPerSecond = 1000 / UpdateInterval;
        public const int SaveCGWorldFrameCount = 60 * FrameCountPerSecond;
    }
}