using System;
using System.Collections.Generic;

namespace ET
{
    public class CGUpdater
    {
        private List<long> updateIds = new();
        private List<long> newUpdateIds = new();

        private readonly Dictionary<long, EntityRef<CGEntity>> cgEntities = new();

        public void Update()
        {
            if (this.newUpdateIds.Count > 0)
            {
                foreach (long id in this.newUpdateIds)
                {
                    this.updateIds.Add(id);
                }
                this.updateIds.Sort();
                this.newUpdateIds.Clear();
            }

            foreach (long id in this.updateIds)
            {
                CGEntity entity = cgEntities[id];
                if (entity == null)
                {
                    this.cgEntities.Remove(id);
                    continue;
                }
                this.newUpdateIds.Add(id);
                CGEntitySystemSingleton.Instance.CGUpdate(entity);
            }
            this.updateIds.Clear();
            ObjectHelper.Swap(ref this.updateIds, ref this.newUpdateIds);
        }
        
        public void Add(CGEntity entity)
        {
            this.newUpdateIds.Add(entity.Id);
            this.cgEntities.Add(entity.Id, entity);
        }
    }
}