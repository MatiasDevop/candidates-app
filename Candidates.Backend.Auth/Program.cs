using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using System.Runtime.Intrinsics.Arm;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
const string AuthScheme = "cookie";
const string AuthScheme2 = "cookie2";
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(AuthScheme)
    .AddCookie(AuthScheme)
    .AddCookie(AuthScheme2);

builder.Services.AddAuthorization(builder =>
{
    builder.AddPolicy("eu passport", pb =>
    {
        pb.RequireAuthenticatedUser()
          .AddAuthenticationSchemes(AuthScheme)
          .RequireClaim("passport_type", "eur");
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

//app.Use((ctx, next) =>
//{
//    if (ctx.Request.Path.StartsWithSegments("/login"))
//    {
//        return next();
//    }
//    if (!ctx.User.Identities.Any(x => x.AuthenticationType == AuthScheme))
//    {
//        ctx.Response.StatusCode = 401;
//        return Task.CompletedTask;
//    }

//    if (!ctx.User.HasClaim("passport_type", "eur"))
//    {
//        ctx.Response.StatusCode = 403;
//        return Task.CompletedTask;
//    }

//    return next();
//});

//app.MapGet("/unsecure", (HttpContext ctx) =>
//{

//    return ctx.User.FindFirst("usr")?.Value ??  "empty";
//}).RequireAuthorization("eu passport");

//app.MapGet("/sweden", (HttpContext ctx) =>
//{
//    if (!ctx.User.Identities.Any(x => x.AuthenticationType == AuthScheme))
//    {
//        ctx.Response.StatusCode = 401;
//        return "";
//    }

//    if (!ctx.User.HasClaim("passport_type", "eur"))
//    {
//        ctx.Response.StatusCode = 403;
//        return "";
//    }
    
//    return "allowed";
//});

////[AuthScheme(AuthScheme2)]
////[AuthClaim("passport_type", "eur")]
//app.MapGet("/denmark", (HttpContext ctx) =>
//{
//    if (!ctx.User.Identities.Any(x => x.AuthenticationType == AuthScheme2))
//    {
//        ctx.Response.StatusCode = 401;
//        return "";
//    }

//    if (!ctx.User.HasClaim("passport_type", "eur"))
//    {
//        ctx.Response.StatusCode = 403;
//        return "";
//    }

//    return "allowed";
//});

//app.MapGet("/login", async (HttpContext ctx) =>
//{

//    var claims = new List<Claim>();
//    claims.Add(new Claim("usr", "anton"));
//    claims.Add(new Claim("passport_type", "eur"));
//    var identity = new ClaimsIdentity(claims, AuthScheme);
//    var user = new ClaimsPrincipal(identity);
//    await ctx.SignInAsync(AuthScheme, user);
//});

app.MapControllers();
app.Run();