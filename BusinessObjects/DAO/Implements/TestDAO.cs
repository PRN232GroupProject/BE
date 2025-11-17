using BusinessObjects.Context;
using BusinessObjects.DAO.Interfaces;
using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DAO.Implements
{
    public class TestDAO : ITestDAO
    {
        private readonly ChemProjectDbContext _context;
        public TestDAO(ChemProjectDbContext context)  {
            _context = context;
        }

        public async Task<Test?> GetTestWithQuestionsAsync(int testId)
        {
            return await _context.Tests
                .Include(t => t.TestQuestions)
                    .ThenInclude(tq => tq.Question)
                .FirstOrDefaultAsync(t => t.Id == testId);
        }
    }
}
