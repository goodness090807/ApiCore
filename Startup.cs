using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICore.Datas;
using APICore.Repositorys;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

namespace APICore
{
    public class Startup
    {
        private readonly string AllowSpecificOrigins = "AllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CoreDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            // 跨域驗證
            services.AddCors(option =>
            {
                option.AddPolicy(AllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins("http://todolistcoreweb.herokuapp.com")
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod();
                    });
            } );


            services.AddControllers();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ToDoList API",
                    Description = "TodoList Web API"
                });
            });
            //目前知道的是這兩種寫法
            services.AddAutoMapper(typeof(Startup));//較常用
            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddTransient<UserInfoRepository>();
            services.AddTransient<TodoListRepository>();

            #region Jwt驗證
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    // 當驗證失敗時，回應標頭會包含 WWW-Authenticate 標頭，這裡會顯示失敗的詳細錯誤原因
                    options.IncludeErrorDetails = true; // 預設值為 true，有時會特別關閉

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // 透過這項宣告，就可以從 "sub" 取值並設定給 User.Identity.Name
                        NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
                        // 透過這項宣告，就可以從 "roles" 取值，並可讓 [Authorize] 判斷角色
                        RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",

                        // 一般我們都會驗證 Issuer
                        ValidateIssuer = true,
                        ValidIssuer = Configuration.GetValue<string>("JwtSettings:Issuer"),

                        // 通常不太需要驗證 Audience
                        ValidateAudience = false,
                        //ValidAudience = "JwtAuthDemo", // 不驗證就不需要填寫

                        // 一般我們都會驗證 Token 的有效期間
                        ValidateLifetime = true,

                        // 如果 Token 中包含 key 才需要驗證，一般都只有簽章而已
                        ValidateIssuerSigningKey = false,

                        // "1234567890123456" 應該從 IConfiguration 取得
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("JwtSettings:SignKey"))),

                        //一定要設定這個才能驗證Token的有效時間
                        ClockSkew = TimeSpan.Zero
                    };
                });
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // 跨域驗證
            app.UseCors(AllowSpecificOrigins);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
