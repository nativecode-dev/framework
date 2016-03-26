namespace NativeCode.Core.Authorization
{
    using System.Collections.Concurrent;

    public static class AuthorizationHandlers
    {
        private static readonly ConcurrentDictionary<string, IAuthorizationHandler> GlobalHandlers = new ConcurrentDictionary<string, IAuthorizationHandler>();

        public static IAuthorizationHandler GetHandler(string name)
        {
            if (GlobalHandlers.ContainsKey(name))
            {
                return GlobalHandlers[name];
            }

            return default(IAuthorizationHandler);
        }

        public static void Register(string name, IAuthorizationHandler handler)
        {
            GlobalHandlers.AddOrUpdate(name, k => handler, (k, v) => handler);
        }
    }
}