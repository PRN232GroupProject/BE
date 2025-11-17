using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DAO.Interfaces
{
    public interface ITestDAO
    {
        Task<Test?> GetTestWithQuestionsAsync(int testId);
    }
}
