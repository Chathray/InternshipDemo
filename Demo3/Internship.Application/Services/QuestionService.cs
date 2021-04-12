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

        public bool Delete(int id)
        {
            return _questionRespository.Delete(id);
        }

        public QuestionModel Get(int id)
        {
            var obj = _questionRespository.Get(id);
            return ObjectMapper.Mapper.Map<QuestionModel>(obj);
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

        public bool Update(QuestionModel model)
        {
            var qa_obj = ObjectMapper.Mapper.Map<Question>(model);
            return _questionRespository.Update(qa_obj);
        }
    }
}
