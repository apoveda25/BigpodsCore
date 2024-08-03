using System.Collections.Generic;
using System.Linq;
using MinioIaC.Data.S3Buckets;
using MinioIaC.Factories;
using Pulumi;

return await Deployment.RunAsync(() =>
{
    // Create buckets
    var bucketsCreated = S3BucketsData
        .GetS3Buckets()
        .AsParallel()
        .ToDictionary(item => item.Key, item => S3BucketFactory.Build(item.Key, item.Value.Config));

    // Export outputs here
    return new Dictionary<string, object?>
    {
        ["buckets"] = Output.All(
            bucketsCreated.Select(bucket =>
                Output
                    .All(bucket.Value.Id, bucket.Value.Bucket, bucket.Value.BucketDomainName)
                    .Apply(bucket => bucket)
            )
        )
    };
});
