using Dapper;
using System;
using System.Data;

namespace Api.Gear.Middleware;

//https://stackoverflow.com/questions/5898988/map-string-to-guid-with-dapper
//https://medium.com/dapper-net/custom-type-handling-4b447b97c620
public class CustomGuidTypeHandler : SqlMapper.TypeHandler<Guid>
{
    public override void SetValue(IDbDataParameter parameter, Guid guid)
    {
        parameter.Value = guid.ToString();
    }

    public override Guid Parse(object value)
    {
        // note: commenting this b/c sqlite does not support guid, so dapper
        // should pass back string only
        // Dapper may pass a Guid instead of a string
        // if (value is Guid)
        //     return (Guid)value;

        return new Guid((string)value);
    }
}