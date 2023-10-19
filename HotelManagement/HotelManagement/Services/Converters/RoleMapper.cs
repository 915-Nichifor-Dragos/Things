using HotelManagement.Models.Constants;

namespace HotelManagement.BusinessLogic.Converters;

public static class RoleMapper
{
    public static DBRoles MapToDBRole(this Role role)
    {
        switch (role)
        {
            case Role.Client:
                return DBRoles.Client;
            case Role.Employee: 
                return DBRoles.Employee;
            case Role.Manager:
                return DBRoles.Manager;
            case Role.Owner: 
                return DBRoles.Owner;
            default:
                throw new NotImplementedException();
        }
    }
}
