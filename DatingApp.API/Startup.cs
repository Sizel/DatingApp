
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Data.Models;
using DatingApp.API.Services;
using DatingApp.Data;
using DatingApp.Data.Models;
using DatingApp.Data.Repos;
using DatingApp.Helpers;
using DatingApp.Hubs;
using DatingApp.Misc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			IdentityBuilder builder = services.AddIdentityCore<User>(opt =>
			{
				opt.Password.RequireDigit = false;
				opt.Password.RequiredLength = 4;
				opt.Password.RequireNonAlphanumeric = false;
				opt.Password.RequireUppercase = false;
			});
			builder.AddRoles<Role>();
			builder.AddEntityFrameworkStores<DataContext>();
			builder.AddSignInManager<SignInManager<User>>();
			services.AddControllers(opt =>
			{
				var policy = new AuthorizationPolicyBuilder()
					.RequireAuthenticatedUser()
					.Build();

				opt.Filters.Add(new AuthorizeFilter(policy));
			})
			.AddNewtonsoftJson( o => o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
			services.AddDbContext<DataContext>(x => x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
			services.AddCors();
			services.AddSignalR();
			services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinarySettings"));
			services.AddAutoMapper(typeof(Startup));

			services.AddScoped<ITokenService, TokenService>();
			services.AddScoped<IUserRepository, UsersRepository>();
			services.AddScoped<IPhotoRepository, PhotoRepository>();
			services.AddScoped<ILikeRepository, LikeRepository>();
			services.AddScoped<IMessagesRepository, MessageRepository>();
			services.AddScoped<LogUserActivity>();

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey
							(System.Text.Encoding.UTF8.GetBytes(Configuration.GetValue<string>("PrivateKey"))),
						ValidateIssuer = false,
						ValidateAudience = false
					};
					options.Events = new JwtBearerEvents
					{
						OnMessageReceived = context =>
						{
							var accessToken = context.Request.Query["access_token"];

							if (!string.IsNullOrEmpty(accessToken))
							{
								context.Token = accessToken;
							}
							return Task.CompletedTask;
						}
					};
				});

			services.AddAuthorization(config =>
			{
				config.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
				config.AddPolicy("RequireAdminOrModeratorRole", policy => policy.RequireRole("Admin", "Moderator"));
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{

			app.UseExceptionHandler(builder =>
			{
				builder.Run(async context =>
				{
					context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

					var exceptionHandlingFeature = context.Features.Get<IExceptionHandlerFeature>();

					if (exceptionHandlingFeature != null)
					{
						await context.Response.WriteAsync(exceptionHandlingFeature.Error.Message);
					}
				});
			});

			app.UseRouting();

			app.UseCors(policy => policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials());

			app.UseAuthentication();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
				endpoints.MapHub<MessageHub>("/messagesHub");
			});
		}
	}
}
