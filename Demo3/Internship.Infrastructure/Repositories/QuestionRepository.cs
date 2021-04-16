namespace Internship.Infrastructure
{
    public class QuestionRepository : RepositoryBase<Question>, IQuestionRepository
    {
        private readonly RepositoryContext _context;


        public QuestionRepository(RepositoryContext context) : base(context)
        {
            _context = context;

        }
    }
}
