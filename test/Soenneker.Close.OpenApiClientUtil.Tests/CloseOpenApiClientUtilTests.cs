using Soenneker.Close.OpenApiClientUtil.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Close.OpenApiClientUtil.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class CloseOpenApiClientUtilTests : HostedUnitTest
{
    private readonly ICloseOpenApiClientUtil _openapiclientutil;

    public CloseOpenApiClientUtilTests(Host host) : base(host)
    {
        _openapiclientutil = Resolve<ICloseOpenApiClientUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
