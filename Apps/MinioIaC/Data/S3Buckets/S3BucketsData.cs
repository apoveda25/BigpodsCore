using System.Collections.Generic;
using MinioIaC.Utils;

namespace MinioIaC.Data.S3Buckets;

public record Bucket(string Name, Pulumi.Minio.S3BucketArgs Config);

public static class S3BucketsData
{
    public static string BigpodsMonolithMediasName { get; } = "Bigpods Monolith Medias";

    public static Dictionary<string, Bucket> GetS3Buckets()
    {
        string bigpodsMonolithMediasNameKebabCase = StringUtils.ToKebabCase(
            BigpodsMonolithMediasName
        );

        return new Dictionary<string, Bucket>
        {
            [BigpodsMonolithMediasName] = new Bucket(
                $"buckets:{bigpodsMonolithMediasNameKebabCase}",
                new Pulumi.Minio.S3BucketArgs
                {
                    Acl = "public",
                    Bucket = bigpodsMonolithMediasNameKebabCase,
                    // BucketPrefix = bigpodsMonolithMediasNameKebabCase,
                    ForceDestroy = true,
                    ObjectLocking = true,
                    Quota = 1024,
                }
            ),
        };
    }
}
