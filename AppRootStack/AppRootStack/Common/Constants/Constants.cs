using System;

namespace AppRootStack.Common.Constants
{
    public static class Constants
    {
        public const string EndPoint = "https://randomuser.me/api";

        public static readonly string Seed = Guid.NewGuid().ToString();
    }
}
