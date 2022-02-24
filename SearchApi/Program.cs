//??#1 diff Core 6 with project, startup, missing methods, how come defining services and pipeline isn't separated ...

using SearchApi;

var app = WebApplication
    .CreateBuilder(args)//;//#3 a word or two on asp core 6 builder top usages and responsibilities
    .RegisterServices()//custom services registration method for clear code
    .Build();//??#3

// pipeline
app.SetupMiddleware();//custom middleware pipeline setup method for clear code
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

