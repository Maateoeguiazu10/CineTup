namespace CineTup.Presentation.Authorization
{
    public enum AuthorizationPolicy
    {
        AdminOnly,
        ClientOnly,
        SysAdminOnly
    }
    public static class Policies
    {
        public const string AdminOnly = "AdminOnly";
        public const string ClientOnly = "ClientOnly";
        public const string SysAdminOnly = "SysAdminOnly";
    }
}
