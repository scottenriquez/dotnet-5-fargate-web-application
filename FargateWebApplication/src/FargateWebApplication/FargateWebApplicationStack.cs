using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.Ecr.Assets;
using Amazon.CDK.AWS.ECS;
using Amazon.CDK.AWS.ECS.Patterns;

namespace FargateWebApplication
{
    public class FargateWebApplicationStack : Stack
    {
        internal FargateWebApplicationStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            Vpc vpc = new Vpc(this, "MyVpc", new VpcProps
            {
                MaxAzs = 3
            });
            Cluster cluster = new Cluster(this, "MyCluster", new ClusterProps
            {
                Vpc = vpc
            });
            ApplicationLoadBalancedFargateService applicationLoadBalancedFargateService = new ApplicationLoadBalancedFargateService(this, "MyFargateService",
                new ApplicationLoadBalancedFargateServiceProps
                {
                    Cluster = cluster,
                    DesiredCount = 1,
                    TaskImageOptions = new ApplicationLoadBalancedTaskImageOptions
                    {
                        Image = ContainerImage.FromRegistry("mcr.microsoft.com/dotnet/samples:dotnetapp")
                        // Image = ContainerImage.FromDockerImageAsset(new DockerImageAsset(this, "DockerImageAsset", new DockerImageAssetProps()
                        // {
                        //     Directory = "src/WebApplication"
                        // }))
                    },
                    MemoryLimitMiB = 2048,
                    PublicLoadBalancer = true
                }
            );
            CfnOutput loadBalancerUrl = new CfnOutput(this, "LoadBalancerURL", new CfnOutputProps()
            {
                ExportName = "LoadBalancerURL",
                Value = applicationLoadBalancedFargateService.LoadBalancer.LoadBalancerDnsName
            });
        }
    }
}
