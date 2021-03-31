using Internship.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Internship.Application
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRespository;
        public QuestionService(IQuestionRepository questionRespository)
        {
            _questionRespository = questionRespository;
        }

        public Task<IReadOnlyList<QuestionModel>> GetAllAsync()
        {
            var ques = _questionRespository.GetAllAsync();
            return ObjectMapper.Mapper.Map<Task<IReadOnlyList<QuestionModel>>>(ques);
        }

        public Task<int> GetCountAsync()
        {
            return _questionRespository.GetCountAsync();
        }
    }
}
