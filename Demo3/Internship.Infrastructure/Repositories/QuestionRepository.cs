namespace Internship.Infrastructure
{
    public class QuestionRepository : Repository<Question>, IQuestionRepository
    {
        private readonly DataContext _context;
        private readonly DataProvider _provider;

        public QuestionRepository(DataContext context, DataProvider provider) : base(context)
        {
            _context = context;
            _provider = provider;
        }
    }
}
