namespace ET.Client
{
    public struct SceneChangeStart
    {
    }

    public struct RoomChangeStart {
        
    }
    
    public struct SceneChangeFinish
    {
    }

    public struct RoomChangeFinish {
        
    }
    
    public struct AfterCreateClientScene
    {
    }
    
    public struct AfterCreateCurrentScene
    {
    }

    public struct AppStartInitFinish
    {
    }

    public struct LoginFinish
    {
    }

    public struct FindEnemy {
        
    }
    
    public struct FightWithAi {}

    public struct EnterMapFinish
    {
    }

    public struct AfterUnitCreate
    {
        public Unit Unit;
    }
}