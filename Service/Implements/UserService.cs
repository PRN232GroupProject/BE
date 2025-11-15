using BusinessObjects.DTO;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implements
{
    public class UserService : IUserService
    {
        public Task<UserDTO> GetCurrentUser()
        {
            throw new NotImplementedException();
        }
    }
}
