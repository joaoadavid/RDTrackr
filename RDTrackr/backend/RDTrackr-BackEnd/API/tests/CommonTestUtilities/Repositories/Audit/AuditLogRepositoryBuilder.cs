using Moq;
using RDTrackR.Domain.Entities;
using RDTrackR.Domain.Repositories.Audit;

namespace CommonTestUtilities.Repositories.Audit
{
    public class AuditLogRepositoryBuilder
    {
        private readonly Mock<IAuditLogRepository> _repoMock;

        public AuditLogRepositoryBuilder()
        {
            _repoMock = new Mock<IAuditLogRepository>();
        }

        public AuditLogRepositoryBuilder VerifyAddAsync(AuditLog expected)
        {
            _repoMock.Verify(r => r.AddAsync(
                It.Is<AuditLog>(log =>
                    log.UserId == expected.UserId &&
                    log.UserName == expected.UserName &&
                    log.ActionType == expected.ActionType &&
                    log.Description == expected.Description
                )
            ), Times.Once);

            return this;
        }

        public AuditLogRepositoryBuilder AddAsyncCallback(Action<AuditLog> callback)
        {
            _repoMock.Setup(r => r.AddAsync(It.IsAny<AuditLog>()))
                     .Callback(callback)
                     .Returns(Task.CompletedTask);

            return this;
        }

        public IAuditLogRepository Build() => _repoMock.Object;
    }
}
