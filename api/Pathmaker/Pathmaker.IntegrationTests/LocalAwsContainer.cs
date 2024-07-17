using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;

namespace Pathmaker.IntegrationTests;

public class FileStorageContainer {
    const int LocalStackPort = 4566;
    const string LocalStackImage = "localstack/localstack:1.3.1";

    public static IContainer GetContainer() {
        var localStackTestContainer = new ContainerBuilder()
            .WithImage(LocalStackImage)
            .WithExposedPort(LocalStackPort)
            .WithPortBinding(LocalStackPort, true)
            .WithWaitStrategy(Wait.ForUnixContainer()
                .UntilHttpRequestIsSucceeded(request => request
                    .ForPath("/_localstack/health")
                    .ForPort(LocalStackPort)))
            .Build();
        return localStackTestContainer;
        // await localStackTestContainer.StartAsync();
        //
        // var localstackUrl = $"http://{localStackTestContainer.Hostname}:{localStackTestContainer.GetMappedPublicPort(4566)}";
        // return localstackUrl;
    }
}
