namespace ET
{
    public static partial class CGUnitFactory
    {
        public static CGUnit Init(CGWorld cgWorld, CardGameUnitInfo unitInfo)
        {
            CGUnitComponent cgUnitComponent = cgWorld.GetComponent<CGUnitComponent>();
            CGUnit cgUnit = cgUnitComponent.AddChildWithId<CGUnit>(unitInfo.PlayerId);

            cgUnit.AddComponent<CGInputComponent>();
            return cgUnit;
        }
    }
}