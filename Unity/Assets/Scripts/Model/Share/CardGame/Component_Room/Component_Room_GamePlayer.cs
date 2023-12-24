using System.Collections.Generic;

namespace ET {
    [ComponentOf(typeof(GameRoom))]
    public class Component_Room_GamePlayer : Entity, IAwake<int> {
        //房间最大人数
        public int PlayerMax;
        //当前人数
        public int PlayerCount; 
    }
}
