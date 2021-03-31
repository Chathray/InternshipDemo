namespace Internship.Infrastructure
{
    public interface IInternshipPointRepository : IRepository<InternshipPoint>
    {
        bool EvaluateIntern(InternshipPoint point);
        InternshipPoint GetPoint(int id);
        bool Update(InternshipPoint obj);
        bool Delete(int id);

    }
}
