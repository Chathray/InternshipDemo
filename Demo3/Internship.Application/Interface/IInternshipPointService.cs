using System.Collections.Generic;

namespace Internship.Application
{
    public interface IInternshipPointService
    {
        IList<InternshipPointModel> GetAll();
        int GetCount();
        public bool EvaluateIntern(InternshipPointModel mark);
        InternshipPointModel GetPoint(int id);
        bool UpdatePoint(InternshipPointModel model);
        bool DeletePoint(int id);
    }
}