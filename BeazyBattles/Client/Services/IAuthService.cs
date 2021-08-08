using BeazyBattles.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeazyBattles.Client.Services
{
    public interface IAuthService
    {
        Task <ServiceResponse<int>>Register(UserRegister request);
    }
}
