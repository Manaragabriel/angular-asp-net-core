using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders.Physical;
using ProAgil.Repository.Data;
using ProAgil.Repository;
using AutoMapper;
using ProAgil.Domain.Identity;
using Microsoft.Extensions.FileProviders;
using System.IO;
using  Microsoft.AspNetCore.Identity;
using  Microsoft.AspNetCore.Authorization;
using  Microsoft.AspNetCore.Mvc.Authorization;
using  Microsoft.AspNetCore.Authentication.JwtBearer;
using  Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ProAgil.api
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
        
            services.AddDbContext<DataContext>(x => x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            IdentityBuilder builder = services.AddIdentityCore<User>(options =>{
                options.Password.RequireDigit= false;
                options.Password.RequireLowercase= false;
                options.Password.RequireNonAlphanumeric=false;
                options.Password.RequireUppercase= false;
                options.Password.RequiredLength= 4;

            });
            builder= new IdentityBuilder(builder.UserType, typeof(Role),builder.Services);
            builder.AddEntityFrameworkStores<DataContext>();
            builder.AddRoleValidator<RoleValidator<Role>>();
            builder.AddRoleManager<RoleManager<Role>>();
            builder.AddSignInManager<SignInManager<User>>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(op=>{
                op.TokenValidationParameters= new TokenValidationParameters{
                    ValidateIssuerSigningKey= true,
                    IssuerSigningKey= new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
                        Configuration.GetSection("AppSettings:Token").Value
                    )),
                    ValidateIssuer= false,
                    ValidateAudience= false,
                };

            });
            services.AddScoped<IProAgilRepository,ProAgilRepository>();
            services.AddAutoMapper();
            services.AddCors();
            services.AddMvc(op=>{
                var policy= new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                op.Filters.Add(new AuthorizeFilter(policy));


            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddJsonOptions(
        options => options.SerializerSettings.ReferenceLoopHandling =            
        Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

           // app.UseHttpsRedirection();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions(){
                FileProvider= new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),@"Resources")),
                RequestPath= new Microsoft.AspNetCore.Http.PathString("/Resources"),
            });
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
