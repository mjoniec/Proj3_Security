namespace SearchApi
{
    public static class SetupMiddlewarePipeline
    {
        public static WebApplication SetupMiddleware(this WebApplication app)
        {
            if (app.Environment.IsDevelopment()) //??#4 good practices in enabling on production
            {
                app.UseSwagger(); //??#4 diff swagger / swaggerUI 
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection(); // ??#5 read on enabling https with core 6, diff in launchSettings.json comparison
            //app.UseAuthorization(); // ??#6 if not needed & no https why added by default?

            app.MapControllers();//??#7 - read on web application in core 6 + pipeline changes, how does MapControllers() know what folder and classes to map to?

            return app;
        }
    }
}
