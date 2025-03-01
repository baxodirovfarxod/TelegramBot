using Microsoft.EntityFrameworkCore;
using RamazonBot.Dal;
using RamazonBot.Dal.Entities;

namespace RamazonBot.Bll.Service;

public class RamazonTimeService : IRamazonTimeService
{
    private readonly MainContext mainContext;

    public RamazonTimeService(MainContext mainContext)
    {
        this.mainContext = mainContext;
    }

    public async Task<RamazonTimes> GetRamazonTimes(int day)
    {
        return await mainContext.RamazonTimes.FirstOrDefaultAsync(r => r.Day == day);
    }
}
