using System.ComponentModel;

namespace Common.Enums
{
    public enum UserTypeEnum
    {
        [Description("Employee")]
        Employee = 1 ,
        [Description("Chef")]
        Chef,
        [Description("Admin")]
        Admin
    }
}
