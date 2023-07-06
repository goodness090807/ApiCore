using System;
using APICore.Datas;
using APICore.Repositorys;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;

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

            // �������
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

            //�ثe���D���O�o��ؼg�k
            services.AddAutoMapper(typeof(Startup));//���`��
            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddTransient<UserInfoRepository>();
            services.AddTransient<TodoListRepository>();

            #region Jwt����
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    // �����ҥ��ѮɡA�^�����Y�|�]�t WWW-Authenticate ���Y�A�o�̷|��ܥ��Ѫ��Բӿ��~��]
                    options.IncludeErrorDetails = true; // �w�]�Ȭ� true�A���ɷ|�S�O����

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // �z�L�o���ŧi�A�N�i�H�q "sub" ���Ȩó]�w�� User.Identity.Name
                        NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
                        // �z�L�o���ŧi�A�N�i�H�q "roles" ���ȡA�åi�� [Authorize] �P�_����
                        RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",

                        // �@��ڭ̳��|���� Issuer
                        ValidateIssuer = true,
                        ValidIssuer = Configuration.GetValue<string>("JwtSettings:Issuer"),

                        // �q�`���ӻݭn���� Audience
                        ValidateAudience = false,
                        //ValidAudience = "JwtAuthDemo", // �����ҴN���ݭn��g

                        // �@��ڭ̳��|���� Token �����Ĵ���
                        ValidateLifetime = true,

                        // �p�G Token ���]�t key �~�ݭn���ҡA�@�볣�u��ñ���Ӥw
                        ValidateIssuerSigningKey = false,

                        // "1234567890123456" ���ӱq IConfiguration ���o
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("JwtSettings:SignKey"))),

                        //�@�w�n�]�w�o�Ӥ~������Token�����Įɶ�
                        ClockSkew = TimeSpan.Zero
                    };
                });
            #endregion


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    // name: ���� SwaggerDocument �� URL ��m�C
                    name: "v1",
                    new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "My API V1",
                        Description = "A simple example ASP.NET Core Web API",
                        TermsOfService = new Uri("https://apicorepractice.herokuapp.com"),
                        Contact = new OpenApiContact
                        {
                            Name = "Terry",
                            Email = string.Empty,
                            Url = new Uri("https://apicorepractice.herokuapp.com"),
                        },
                        License = new OpenApiLicense
                        {
                            Name = "Use under LICX",
                            Url = new Uri("https://example.com/license"),
                        }
                    }
                );
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // �������
            app.UseCors(AllowSpecificOrigins);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
