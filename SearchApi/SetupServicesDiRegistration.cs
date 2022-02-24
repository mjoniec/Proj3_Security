// services
using SearchApi.Services;//#2 custom using, some good practice on nesting & visibility, default usings not visible how...

namespace SearchApi;

public static class SetupServicesDiRegistration
{
    public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();//??#3
        builder.Services.AddEndpointsApiExplorer();//#4 swagger - how is AddEndpointsApiExplorer needed with swagger
        builder.Services.AddSwaggerGen(); //??#4 - read on default swagger and open api in core6, how come no xml (generated?) / attributes in controllers are needed?
        builder.Services.AddScoped<IGoogleSearchService, GoogleSearchService>();

        return builder;
    }
}
