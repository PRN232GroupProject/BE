using BusinessObjects.DTO;
using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Mapper
{
    public interface IMapperlyMapper
    {
        UserDTO UserToUserDto(User user);
        LoginResponse UserToLoginResponse(User user);
        User RegisterRequestToUser(RegisterRequest request);
    }
}