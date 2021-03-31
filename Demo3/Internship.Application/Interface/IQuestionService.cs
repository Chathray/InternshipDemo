using Internship.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Internship.Application
{
    public interface IQuestionService
    {
        Task<IReadOnlyList<QuestionModel>> GetAllAsync();
        Task<int> GetCountAsync();
    }
}