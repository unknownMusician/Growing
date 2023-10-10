using System;

namespace Growing.Utils.ExternalPolymorphism
{
    public sealed class NoTypeRegisteredException : Exception
    {
        public NoTypeRegisteredException(Type type) : base($"No type delegate for {type?.FullName ?? "Null"} registered.") { }
    }
}