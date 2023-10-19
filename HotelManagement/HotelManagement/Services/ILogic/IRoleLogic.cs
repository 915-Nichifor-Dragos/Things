namespace HotelManagement.BusinessLogic.ILogic;

public interface IRoleLogic
{
    public Task<Models.DataModels.Role> GetRoleByName(Models.Constants.Role name);
    public IEnumerable<Models.DataModels.Role> GetAssignableRoles(bool isOwner);
}
