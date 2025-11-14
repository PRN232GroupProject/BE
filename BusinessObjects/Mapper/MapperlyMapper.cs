using BusinessObjects.DTO;
using BusinessObjects.Entities;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Mapper
{
    [Mapper]
    public partial class MapperlyMapper : IMapperlyMapper
    {
        // User to UserDto mapping
        [MapProperty(nameof(User.Role.Name), nameof(UserDTO.Role))]
        public partial UserDTO UserToUserDto(User user);

        // User to LoginResponse mapping - only Role name
        [MapProperty(nameof(User.Role.Name), nameof(LoginResponse.Role))]
        public partial LoginResponse UserToLoginResponse(User user);

        // RegisterRequest to User mapping
        public partial User RegisterRequestToUser(RegisterRequest request);
    }
}