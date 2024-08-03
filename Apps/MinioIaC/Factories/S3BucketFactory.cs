namespace MinioIaC.Factories;

public static class S3BucketFactory
{
    public static Pulumi.Minio.S3Bucket Build(string name, Pulumi.Minio.S3BucketArgs args)
    {
        return new Pulumi.Minio.S3Bucket(
            name,
            new()
            {
                Acl = args.Acl,
                Bucket = args.Bucket,
                BucketPrefix = args.BucketPrefix,
                ForceDestroy = args.ForceDestroy,
                ObjectLocking = args.ObjectLocking,
                Quota = args.Quota,
            }
        );
    }
}
