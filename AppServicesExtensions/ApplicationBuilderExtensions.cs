namespace ApiCatalogo2.AppServicesExtensions;

public static class ApplicationBuilderExtensions
    
    //métodos de extenção
{
    public static IApplicationBuilder UseExcepetionHandling(this IApplicationBuilder app, 
         IWebHostEnvironment environment)
    {
        if(environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        return app;
    }
    public static IApplicationBuilder UseeAppCors(this IApplicationBuilder app)
    {
        app.UseCors(p =>
        {
            p.AllowAnyOrigin();
            p.WithMethods("GET");
            p.AllowAnyHeader();
        });
        return app;
    }
    public static IApplicationBuilder UseSwaggerMiddleware(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => { });
        return app;
    }
}
