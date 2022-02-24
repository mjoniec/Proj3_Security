//??#1 diff Core 6 with project, startup, missing methods, how come defining services and pipeline isn't separated ...

// services
using SearchApi.Services;//#2 custom using, some good practice on nesting & visibility, default usings not visible how...

var builder = WebApplication.CreateBuilder(args);//#3 a word or two on asp core 6 builder top usages and responsibilities

builder.Services.AddControllers();//??#3
builder.Services.AddEndpointsApiExplorer();//#4 swagger - how is AddEndpointsApiExplorer needed with swagger
builder.Services.AddSwaggerGen(); //??#4 - read on default swagger and open api in core6, how come no xml (generated?) / attributes in controllers are needed?
builder.Services.AddScoped<IGoogleSearchService, GoogleSearchService>();

// pipeline
var app = builder.Build(); //??#3

if (app.Environment.IsDevelopment()) //??#4 good practices in enabling on production
{
    app.UseSwagger(); //??#4 diff swagger / swaggerUI 
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection(); // ??#5 read on enabling https with core 6, diff in launchSettings.json comparison
//app.UseAuthorization(); // ??#6 if not needed & no https why added by default?

app.MapControllers();//??#7 - read on web application in core 6 + pipeline changes, how does MapControllers() know what folder and classes to map to?
app.Run();//??#7

//??#8 - launchsettings.json
//structure
//double iis definition on 2 separate layers
//what is profiles
//linux equvalent of IIS possible to define there?
//how to select kestrel ? from cmd/ azure deployed ...

//??#9 - DI
//what is a container (DI not docker) 
//diff to a default DI container that comes withASP
//some input on usages in IServiceCollection / IServiceProvider - anything new with core 6?

//??#10 inline (?) controllers
//any good code practice on defining controllers in project.cs ?

