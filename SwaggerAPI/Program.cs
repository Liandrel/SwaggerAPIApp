using Microsoft.OpenApi.Models;
using System.Reflection;

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
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    opts.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));





    var securityScheme = new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Description = "JWT auth header info using bearer tokens",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    };

    var securityRequirement = new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "bearerAuth"
                }
            },
            new string[] {}
        }
    };

    opts.AddSecurityDefinition("bearerAuth", securityScheme);
    opts.AddSecurityRequirement(securityRequirement);


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
