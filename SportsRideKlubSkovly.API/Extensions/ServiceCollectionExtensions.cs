﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace SportsRideKlubSkovly.API.Extensions;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddSwaggerGenWithAuth(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSwaggerGen(o =>
        {
            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "JWT Authentication",
                Description = "Enter your JWT token in this field",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                BearerFormat = "JWT"
            };
            
            o.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securityScheme);

            var securityRequirement = new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        }
                    },
                    []
                }
            };
            
            o.AddSecurityRequirement(securityRequirement);
        });
        
        return serviceCollection;
    }
}