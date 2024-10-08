﻿using System.Reflection;

namespace SportsRideKlubSkovly.API.Extensions;

public static class MediatRExtensions
{
    public static IServiceCollection AddMediatRModules(this IServiceCollection services)
    {
        var binPath = AppDomain.CurrentDomain.BaseDirectory;
        var assemblies = Directory.GetFiles(binPath, "Modules.*.dll")
            .Select(Assembly.LoadFrom)
            .ToArray();
 
        services.AddMediatR(config => config.RegisterServicesFromAssemblies(assemblies));
 
        return services;
    }
}