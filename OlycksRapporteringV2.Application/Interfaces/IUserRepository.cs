using OlycksRapporteringV2.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OlycksRapporteringV2.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmployeeId(string employeeId);
        Task OnClickedCreateUser(User user);

    }
}
