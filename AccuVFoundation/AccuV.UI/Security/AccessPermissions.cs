using System;

namespace AccuV.UI.Security
{
    [Flags]
    public enum AccessPermissions
    {
        None = 0,
        Create = 1,
        Read = 2,
        Update = 4,
        Delete = 8,
        Approve = 16,
        Admin = 32
    }
}