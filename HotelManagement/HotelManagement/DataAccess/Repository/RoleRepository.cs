﻿using HotelManagement.DataAccess.Contexts;
using HotelManagement.DataAccess.IRepositories;
using HotelManagement.DataAccess.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.DataAccess.Repository;

public class RoleRepository : AbstractRepository<Models.DataModels.Role>, IRoleRepository
{
    private readonly HotelManagementContext _context;

    public RoleRepository(HotelManagementContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }

    public async Task<Models.DataModels.Role> GetRoleByName(Models.Constants.Role name)
    {
        return await _context.Roles
            .FirstOrDefaultAsync(role => role.Name.Equals(name));
    }

    public IEnumerable<Models.DataModels.Role> GetAssignableRoles(List<Models.Constants.Role> roles)
    {
        return _dbSet
            .Where(r => roles.Contains(r.Name))
            .ToList();
    }
}
