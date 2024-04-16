using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TopStyleDb.Core.Interfaces;
using TopStyleDb.Core.Services;
using TopStyleDb.Data;
using TopStyleDb.Data.Interfaces;
using TopStyleDb.Data.Repos;

namespace TopStyleDb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            ///////////
            /// JWT ///
            ///////////
            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(opt =>
                {

                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                });


            ///////////////
            /// SWAGGER ///
            ///////////////
            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "TopStyle API", Version = "v1" });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Please enter token.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });


            ////////////////
            /// Conn. Db ///
            ////////////////
            //builder.Services.AddDbContext<TopStyleDbContext>(options =>
            //    options.UseSqlServer(Environment.GetEnvironmentVariable("TOPSTYLEDB_CONNECTION_STRING")));

            builder.Services.AddDbContext<TopStyleDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("TopStyleDb"));
            });

            builder.Services.AddTransient<IAuthService, AuthService>();
            builder.Services.AddTransient<ICustomerService, CustomerService>();
            builder.Services.AddTransient<ICustomerRepo, CustomerRepo>();
            builder.Services.AddTransient<ICategoryService, CategoryService>();
            builder.Services.AddTransient<ICategoryRepo, CategoryRepo>();
            builder.Services.AddTransient<IProductService, ProductService>();
            builder.Services.AddTransient<IProductRepo, ProductRepo>();
            builder.Services.AddTransient<IOrderRepo, OrderRepo>();
            builder.Services.AddTransient<IOrderService, OrderService>();


            var app = builder.Build();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            //app.MapControllers();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.Run();
        }
    }
}
