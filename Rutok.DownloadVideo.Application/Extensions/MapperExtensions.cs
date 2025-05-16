using System.Reflection;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Rutok.DownloadVideo.Application.MapperRegisters;

namespace Rutok.DownloadVideo.Application.Extensions;

public static class MapperExtensions
{
    public static IServiceCollection RegisterMapster(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        var registers = config.Scan(Assembly.GetAssembly(typeof(RequestModelsRegister)));
        config.Apply(registers);
        services.AddSingleton(config);
        services.AddScoped<IMapper, Mapper>();
        return services;
    }
}