using System.Collections.Generic;

namespace Internship.Application
{
    public interface IQuestionService
    {
        IList<QuestionModel> GetAll();
        bool Insert(QuestionModel qa);
        bool Delete(int id);
        QuestionModel Get(int id);
        bool Update(QuestionModel model);
    }
}