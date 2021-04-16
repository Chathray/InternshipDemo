using Internship.Infrastructure;

namespace Internship.Application
{
    public interface IQuestionService : IServiceBase<QuestionModel, Question>
    {
    }
}