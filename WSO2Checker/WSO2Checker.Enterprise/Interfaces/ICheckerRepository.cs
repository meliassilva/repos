using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Byui.WSO2Checker.Enterprise.Interfaces
{
    public interface ICheckerRepository
    {
        Task<string> GetAccessToken();
        //Task<Response<List<Assignments>>> GetAssignmentsByEntityCode(string entityCode, bool isSectionCode = true);
    }
}
