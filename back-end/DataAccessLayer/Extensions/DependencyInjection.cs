using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using DataAccessLayer.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccess(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var options = configuration
            .GetSection("Filebase")
            .Get<FilebaseOptions>()
            ?? throw new InvalidOperationException(
                "Filebase configuration is missing.");

        var credentials = new BasicAWSCredentials(
            options.AccessKey,
            options.SecretKey);

        var s3Config = new AmazonS3Config
        {
            ServiceURL = options.Endpoint,
            AuthenticationRegion = options.Region,
            ForcePathStyle = true
        };

        services.AddSingleton<IAmazonS3>(
            new AmazonS3Client(credentials, s3Config));

        services.AddSingleton(options);

        return services;
    }
}
