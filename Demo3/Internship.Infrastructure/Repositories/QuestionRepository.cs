namespace Internship.Infrastructure
{
    public class QuestionRepository : RepositoryBase<Question>, IQuestionRepository
    {
        private readonly DataContext _context;

        public QuestionRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
