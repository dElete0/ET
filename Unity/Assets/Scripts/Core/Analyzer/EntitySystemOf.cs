using System;

namespace ET
{
    /// <summary>
    /// 标记Entity的System静态类 用于自动生成System函数
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class EntitySystemOfAttribute: BaseAttribute
    {
        public Type type;

        /// <summary>
        /// 标记Entity的System静态类 用于自动生成System函数
        /// </summary>
        /// <param name="type">Entity类型</param>
        /// <param name="ignoreAwake">是否忽略生成AwakeSystem</param>
        public EntitySystemOfAttribute(Type type, bool ignoreAwake = false)
        {
            this.type = type;
        }
    }

    /// <summary>
    /// 标记LSEntity的System静态类 用于自动生成System函数
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class LSEntitySystemOfAttribute: BaseAttribute
    {
        public Type type;

        /// <summary>
        /// 标记LSEntity的System静态类 用于自动生成System函数
        /// </summary>
        /// <param name="type">LSEntity类型</param>
        /// <param name="ignoreAwake">是否忽略生成AwakeSystem</param>
        public LSEntitySystemOfAttribute(Type type, bool ignoreAwake = false)
        {
            this.type = type;
        }
    }
    
    [AttributeUsage(AttributeTargets.Class)]
    public class CGEntitySystemOfAttribute: BaseAttribute
    {
        public Type type;

        /// <summary>
        /// 标记CGEntity的System静态类 用于自动生成System函数
        /// </summary>
        /// <param name="type">CGEntity类型</param>
        /// <param name="ignoreAwake">是否忽略生成AwakeSystem</param>
        public CGEntitySystemOfAttribute(Type type, bool ignoreAwake = false)
        {
            this.type = type;
        }
    }
}