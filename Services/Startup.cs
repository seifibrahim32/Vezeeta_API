using Repositories.IRepository;
using Repositories.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.HttpOverrides;
using Domain.Entities;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using Domain.Data;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace Services
{
    public class Startup
    {
        public IConfiguration ConfigRoot { get; }

        public Startup(IConfiguration configuration)
        {
            ConfigRoot = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.Configure<RequestLocalizationOptions>(
                options =>
                {
                    var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("en-US"),
                        new CultureInfo("ar"),

                    };
                    
                    options.ApplyCurrentCultureToResponseHeaders = true;
                    options.DefaultRequestCulture = new RequestCulture("en-US");
                    options.SupportedCultures = supportedCultures;
                    options.SupportedUICultures = supportedCultures;
                }
            );
            services.AddMvc();

            services.AddDbContext<VezeetaDbContext>(
               options => options.UseSqlServer(ConfigRoot["ConnectionStrings:DefaultConnection"])
            );

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<VezeetaDbContext>()
                .AddDefaultTokenProviders();

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            }).AddJsonOptions(
               options => options.JsonSerializerOptions.PropertyNameCaseInsensitive = false
            );


            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Description = "The official API for Vezeeta backend",
                    Title = "Vezeeta",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Seif Ashraf",
                        Email = "seeifeldina@gmail.com",
                        Url = new Uri("https://linkedin.com/in/seifashrafdev")
                    }
                });

            });
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            }).AddCookie().AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
                {
                    options.SignInScheme = IdentityConstants.ExternalScheme;
                    options.SaveTokens = true;
                    options.ClientId = "900890719775-p07o0tpjioll6pq4lp3fhqe0arfh5aug.apps" +
                                       ".googleusercontent.com";
                    options.ClientSecret = "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";
                });

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(option =>
               {
                   option.SaveToken = true;
                   option.RequireHttpsMetadata = false;
                   option.TokenValidationParameters = new TokenValidationParameters()
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidAudience = ConfigRoot["JWT:ValidAudience"],
                       ValidIssuer = ConfigRoot["JWT:ValidIssuer"],
                       IssuerSigningKey = new SymmetricSecurityKey(
                           Encoding.UTF8.GetBytes(ConfigRoot["JWT:Secret"])
                       )
                   };
               });
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });
            services.AddTransient(typeof(IUserRepository), typeof(AccountRepository));

            services.AddTransient(typeof(IEmailSenderRepository), typeof(PatientEmailSenderRepository));
            services.AddTransient(typeof(IEmailSenderRepository), typeof(DoctorEmailSenderRepository));

            services.AddScoped(typeof(ISearchRepository), typeof(SearchRepository));
            services.AddScoped(typeof(IAdminRepository), typeof(AdminstrationRepository));
            services.AddScoped(typeof(IBookingRepository), typeof(BookingRepository));
             
            services.AddSingleton<IStringLocalizerFactory, ResourceManagerStringLocalizerFactory>();
            services.AddSingleton<IStringLocalizer>(provider =>
                provider.GetService<IStringLocalizerFactory>().Create("HomeController", "MyWebApp"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger().UseSwaggerUI(c =>
                {
                    c.ConfigObject.AdditionalItems["syntaxHighlight"] = new Dictionary<string, object>
                    {
                        ["activated"] = false
                    };
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Vezeeta v1");
                });
                app.UseDeveloperExceptionPage();
            }
            app.UseCookiePolicy(new CookiePolicyOptions
            {
                Secure = CookieSecurePolicy.Always,
                MinimumSameSitePolicy = SameSiteMode.Lax
            });
            app.UseForwardedHeaders();

            var supportedCultures = new[] { new CultureInfo("en-US"), new CultureInfo("ar") };
            var requestLocalizationOptions = new RequestLocalizationOptions  
            {
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };
            var localizeOptions = app.ApplicationServices
                .GetService<IOptions<RequestLocalizationOptions>>();

            app.UseRequestLocalization(localizeOptions.Value);
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
