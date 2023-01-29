using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opts =>
{
    opts.SwaggerDoc("v1", new OpenApiInfo 
    { 
        //put from appsettings/hard code
        Version = "v1",
        Title = "User API (title for swagger)",
        Description = "This is description about API",
        TermsOfService = new Uri("https://www.termsofservicesite.com"),
        Contact = new OpenApiContact
        {
            Name= "ContactName",
            Url = new Uri("https://www.contactsite.com")
        },
        License = new OpenApiLicense
        {
            Name = "LicenseName",
            Url = new Uri("https://www.licenseurl.com")
        }
    });

});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opts =>
    {
        opts.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        opts.RoutePrefix= string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
