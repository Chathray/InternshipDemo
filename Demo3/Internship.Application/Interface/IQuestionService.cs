using System.Collections.Generic;

namespace Internship.Application
{
    public interface IQuestionService
    {
        IList<QuestionModel> GetAll();
        bool Insert(QuestionModel qa);
    }
}