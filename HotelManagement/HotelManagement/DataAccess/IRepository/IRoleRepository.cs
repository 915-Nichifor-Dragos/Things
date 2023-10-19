namespace HotelManagement.DataAccess.IRepository;

public interface IRoleRepository
{
    public Task<Models.DataModels.Role> GetRoleByName(Models.Constants.Role name);
    IEnumerable<Models.DataModels.Role> GetAssignableRoles(List<Models.Constants.Role> roles);
}
