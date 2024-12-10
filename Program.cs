using CourtComplaintFormBackend.Data;
using CourtComplaintFormBackend.Helpers;
using CourtComplaintFormBackend.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.SetIsOriginAllowed(origin => new Uri(origin).Host.Equals("localhost", StringComparison.OrdinalIgnoreCase))
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    // JWT authentication definition.
    // Random values in Issuer, Audience and SigningKey at the moment.
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "YourIssuer",
        ValidAudience = "YourAudience",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSuperSecretKeyYourSuperSecretKey"))
    };
});

builder.Services.AddAuthorization();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("ContactFormDb"));

var app = builder.Build();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        var errorResponse = new
        {
            StatusCode = 500,
            Message = "Error: An unexpected error has occurred.\n\n\nAdditional information:\n" + context.Features.Get<IExceptionHandlerFeature>()?.Error
        };

        await context.Response.WriteAsJsonAsync(errorResponse);
    });
});

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureCreated();

    if (!db.ContactForms.Any())
    {
        db.ContactForms.AddRange(
            new ContactFormData
            {
                Id = 1,
                Name = "John Doe",
                Email = "john.doe@example.com",
                Message = "This is the first test message.",
                SubmittedAt = DateTime.UtcNow
            },
            new ContactFormData
            {
                Id = 2,
                Name = "Jane Smith",
                Email = "jane.smith@example.com",
                Message = "This is a second test message.",
                SubmittedAt = DateTime.UtcNow
            },
            new ContactFormData
            {
                Id = 3,
                Name = "Robert Johnson",
                Email = "robert.johnson@example.com",
                Message = "This is a third test message.",
                SubmittedAt = DateTime.UtcNow
            }
        );
        db.SaveChanges();
    }
}

app.MapGet("/", () =>
{
    return Results.Ok(new { Message = "Yo" });
});

app.MapGet("/authenticate", async (HttpContext httpContext, ApplicationDbContext db) =>
{
    var response = new Dictionary<string, object>
    {
        { "Message", "" },
        { "Data", new Dictionary<string, object>() }
    };

    // Perform authentication check here
    // If successful, generate and send token

    bool authenticationSuccess = true;

    if(!authenticationSuccess)
    {
        response["Message"] = "Error: Could not authenticate user credentials.";
        response.Remove("Data");

        return Results.Json(response, statusCode: 500);
    }

    var tokenHandler = new TokenHelper();

    var tokenString = tokenHandler.GenerateToken("UsernameValue");

    response["Data"] = new { Token = tokenString };

    return Results.Json(response, statusCode: 200);
});

app.MapPost("/contact", [Authorize] async (ContactFormData contactForm, ApplicationDbContext db) =>
{
    contactForm.SubmittedAt = DateTime.UtcNow;

    db.ContactForms.Add(contactForm);

    await db.SaveChangesAsync();

    return Results.Created($"/contact/{contactForm.Id}", contactForm);
});

app.MapGet("/contact", [Authorize] async (ApplicationDbContext db) =>
{
    var contactForms = await db.ContactForms.ToListAsync();

    return Results.Ok(contactForms);
});

app.MapGet("/contact/{id}", [Authorize] async (int id, ApplicationDbContext db) =>
{
    var contactForm = await db.ContactForms.FindAsync(id);

    if (contactForm is null)
        return Results.NotFound();

    return Results.Ok(contactForm);
});

app.MapPut("/contact/{id}", [Authorize] async (int id, ContactFormData contactForm, ApplicationDbContext db) =>
{
    var existingContactForm = await db.ContactForms.FindAsync(id);

    if (existingContactForm is null)
        return Results.NotFound();

    existingContactForm.Name = contactForm.Name;
    existingContactForm.Email = contactForm.Email;
    existingContactForm.Message = contactForm.Message;
    existingContactForm.SubmittedAt = DateTime.UtcNow;

    await db.SaveChangesAsync();

    return Results.Ok(existingContactForm);
});

app.MapDelete("/contact/{id}", [Authorize] async (int id, ApplicationDbContext db) =>
{
    var contactForm = await db.ContactForms.FindAsync(id);

    if (contactForm is null)
        return Results.NotFound();

    db.ContactForms.Remove(contactForm);

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.Run();