using RamazonBot.Dal.Entities;

namespace RamazonBot.Bll.Service;

public interface IRamazonTimeService
{
    Task<RamazonTimes> GetRamazonTimes(int day);
}