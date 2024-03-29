﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;


namespace ET.Server
{
    [MessageSessionHandler(SceneType.Realm)]
    [FriendOfAttribute(typeof(ET.Server.AccountInfo))]
    public class C2R_LoginHandler : MessageSessionHandler<C2R_Login, R2C_Login>
    {

        protected override async ETTask Run(Session session, C2R_Login request, R2C_Login response)
        {
            if (string.IsNullOrEmpty(request.Account) || string.IsNullOrEmpty(request.Password))
            {
                response.Error = ErrorCode.ERR_LoginInfoEmpty;
                CloseSession(session).Coroutine();
                return;
            }

            //Account加锁
            using (await session.Root().GetComponent<CoroutineLockComponent>().Wait(CoroutineLockType.LoginAccount, request.Account.GetLongHashCode())) 
            {
                DBComponent dbComponent = session.Root().GetComponent<DBManagerComponent>().GetZoneDB(session.Zone());
                List<AccountInfo> accountInfos = await dbComponent.Query<AccountInfo>(accountInfo => accountInfo.Account == request.Account);

                if (accountInfos.Count < 1) {
                    AccountInfoComponent accountInfoComponent =
                            session.GetComponent<AccountInfoComponent>() ?? session.AddComponent<AccountInfoComponent>();
                    AccountInfo accountInfo = accountInfoComponent.AddChild<AccountInfo>();
                    accountInfo.Account = request.Account;
                    accountInfo.Password = request.Password;
                    await dbComponent.Save(accountInfo);
                } else {
                    AccountInfo accountInfo = accountInfos[0];
                    if (accountInfo.Password != request.Password) {
                        response.Error = ErrorCode.ERR_LoginPasswordError;
                        CloseSession(session).Coroutine();
                        return;
                    }
                }
            }

            // 随机分配一个Gate
            StartSceneConfig config = RealmGateAddressHelper.GetGate(session.Zone(), request.Account);
            Log.Debug($"gate address: {config}");

            // 向gate请求一个key,客户端可以拿着这个key连接gate
            G2R_GetLoginKey g2RGetLoginKey = (G2R_GetLoginKey)await session.Fiber().Root.GetComponent<MessageSender>().Call(
                config.ActorId, new R2G_GetLoginKey() { Account = request.Account });

            response.Address = config.InnerIPPort.ToString();
            response.Key = g2RGetLoginKey.Key;
            response.GateId = g2RGetLoginKey.GateId;

            CloseSession(session).Coroutine();
        }

        private async ETTask CloseSession(Session session)
        {
            await session.Root().GetComponent<TimerComponent>().WaitAsync(1000);
            session.Dispose();
        }
    }
}
