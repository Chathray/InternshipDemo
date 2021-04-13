using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Internship.Infrastructure.Test
{
    public class InternTests
    {        
        [Fact]
        public void InternInfo_CanGet()
        {
            //Setup DbContext and DbSet mock  
            var dbContextMock = new Mock<DataContext>();
            var dbSetMock = new Mock<DbSet<Intern>>();

            dbContextMock.Setup(s => s.Set<Intern>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(s => s.FindAsync(It.IsAny<Guid>())).Returns(new ValueTask<Intern>());

            InternRespository _internRespository = new(dbContextMock.Object);
            _internRespository.Count(typeof(Intern));
        }
    }
}
