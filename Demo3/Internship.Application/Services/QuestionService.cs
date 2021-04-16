using Internship.Infrastructure;

namespace Internship.Application
{
    public class QuestionService : ServiceBase<QuestionModel, Question>, IQuestionService
    {
        private readonly IQuestionRepository _questionRespository;
        public QuestionService(IQuestionRepository questionRespository) : base(questionRespository)
        {
            _questionRespository = questionRespository;
        }

    }
}
