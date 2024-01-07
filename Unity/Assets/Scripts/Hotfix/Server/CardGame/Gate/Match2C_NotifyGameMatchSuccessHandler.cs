using System;


namespace ET.Server
{
    [MessageHandler(SceneType.Gate)]
    public class Match2C_NotifyGameMatchSuccessHandler : MessageHandler<Player, Match2C_NotifyGameMatchSuccess>
    {
        protected override async ETTask Run(Player player, Match2C_NotifyGameMatchSuccess message)
        {
            Log.Warning("服务器发送匹配成功的消息");
            player.AddComponent<PlayerRoomComponent>().RoomActorId = message.ActorId;
			
            player.GetComponent<PlayerSessionComponent>().Session.Send(message);
            await ETTask.CompletedTask;
        }
    }
}