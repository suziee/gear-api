using System.Data;

namespace Api.Gear.Interfaces
{
    interface IDbConnectionFactory
    {
        IDbConnection Get();
    }
}