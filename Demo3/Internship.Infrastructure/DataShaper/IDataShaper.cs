using System.Collections.Generic;
using System.Dynamic;

namespace Internship.Infrastructure
{
    public interface IDataShaper<T>
    {
        IList<ExpandoObject> ShapeDatas(IList<T> entities, string fieldsString);

        ExpandoObject ShapeData(T entity, string fieldsString);
    }
}
