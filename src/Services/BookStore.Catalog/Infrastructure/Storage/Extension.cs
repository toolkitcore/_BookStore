using BookStore.Catalog.Infrastructure.Storage.Cloudinary;
using BookStore.Catalog.Infrastructure.Storage.Cloudinary.Internal;
using BookStore.Infrastructure.Validator;
using CloudinaryDotNet;
using Microsoft.Extensions.Options;

namespace BookStore.Catalog.Infrastructure.Storage;

public static class Extension
{
    public static IServiceCollection AddCloudinary(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<CloudinarySetting>()
            .Bind(configuration.GetSection(nameof(CloudinarySetting)))
            .ValidateFluentValidation()
            .ValidateOnStart();

        services.AddScoped<ICloudinaryUploadApi, CloudinaryDotNet.Cloudinary>(provider =>
        {
            var cloudinary = provider.GetRequiredService<IOptions<CloudinarySetting>>().Value;
            return new(new Account(cloudinary.CloudName, cloudinary.ApiKey, cloudinary.ApiSecret));
        });

        services.AddScoped<ICloudinaryService, CloudinaryService>();

        return services;
    }
}
