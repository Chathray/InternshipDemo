using Internship.Infrastructure;

namespace Internship.Application
{
    public class ActivityService : ServiceBase<ActivityModel, Activity>, IActivityService
    {
        private readonly IActivityRepository _activityRespository;
        public ActivityService(IActivityRepository activityRespository) : base(activityRespository)
        {
            _activityRespository = activityRespository;
        }
    }
}
