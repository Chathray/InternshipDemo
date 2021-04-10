using Internship.Infrastructure;
using System.Collections.Generic;

namespace Internship.Application
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRespository;
        public QuestionService(IQuestionRepository questionRespository)
        {
            _questionRespository = questionRespository;
        }

        public IList<QuestionModel> GetAll()
        {
            var ques = _questionRespository.GetAll();
            return ObjectMapper.Mapper.Map<IList<Question>, IList<QuestionModel>>(ques);
        }

        public bool Insert(QuestionModel qa)
        {
            var qa_obj = ObjectMapper.Mapper.Map<Question>(qa);
            return _questionRespository.Insert(qa_obj);
        }
    }
}
