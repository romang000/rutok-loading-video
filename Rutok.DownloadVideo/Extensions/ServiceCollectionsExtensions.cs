using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Rutok.DownloadVideo.Application.Abstractions.IRepositories;
using Rutok.DownloadVideo.Application.Abstractions.IServices;
using Rutok.DownloadVideo.Application.Services;
using Rutok.DownloadVideo.Domain.Options;
using Rutok.DownloadVideo.Infrastructure.BackgroundServices;
using Rutok.DownloadVideo.Infrastructure.Context;
using Rutok.DownloadVideo.Infrastructure.Repositories;

namespace Rutok.DownloadVideo.Extensions;

public static class ServiceCollectionsExtensions
{
    public static WebApplicationBuilder AddSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Rutok Download Video",
                Version = "v1",
            });
            
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme.",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer",
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
        return builder;
    }
    
    public static WebApplicationBuilder AddData(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<DownloadVideoDbContext>(option =>
        {
            option.UseNpgsql(builder.Configuration.GetConnectionString(nameof(DownloadVideoDbContext)));
        });
        
        return builder;
    }
    
    public static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ITagService, TagService>();
        builder.Services.AddScoped<ITagRepository, TagRepository>();
        
        builder.Services.AddScoped<IVideoService, VideoService>();
        builder.Services.AddScoped<IVideoRepository, VideoRepository>();
        
        builder.Services.AddScoped<ICommentRepository, CommentRepository>();
        builder.Services.AddScoped<ICommentService, CommentService>();

        builder.Services.AddScoped<IBaseRepository, BaseRepository>();
        return builder;
    }

    public static WebApplicationBuilder AddOptions(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection("RabbitMQ"));
        return builder;
    }

    public static WebApplicationBuilder AddBackgroundService(this WebApplicationBuilder builder)
    {
        builder.Services.AddHostedService<CreateVideoConsumer>();
        
        return builder;
    }

    public static WebApplicationBuilder AddIntegrationServices(this WebApplicationBuilder builder)
    {
        return builder;
    }
}