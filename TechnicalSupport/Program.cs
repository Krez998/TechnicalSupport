using DataAccessLayer;
using Domain;
using Domain.Messages;
using Domain.Messages.Commands;
using Domain.Security;
using Domain.Tickets;
using Domain.Tickets.Commands;
using Domain.Tickets.Queries;
using Domain.Users;
using Domain.Users.Commands;
using Domain.Users.Queries;
using Domain.UserTickets;
using Domain.UserTickets.Commands;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TechnicalSupport;
using TechnicalSupport.Data;


var builder = WebApplication.CreateBuilder(args);

var appSettings = builder.Configuration.Get<AppSettings>();

// Чтение конфигурации
builder.Services.Configure<DomainSettings>(builder.Configuration);
builder.Services.Configure<AppSettings>(builder.Configuration);
builder.Services.Configure<PasswordHashingSettings>(builder.Configuration.GetSection("PasswordHashing"));

// Инжектирование IOptions<T>
builder.Services.AddOptions();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(builder =>
    builder.UseNpgsql(appSettings.DbConnection));

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert token",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "Bearer"}
            },
            new string[] { }
        }
    });
});


builder.Services.AddScoped<DataSeeder>();

// Зависимости
builder.Services.AddTransient<IJWTProvider, JWTProvider>();

// Валидаторы
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateTicketCommandValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ChangeTicketStatusCommandValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<GetTicketQueryValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<GetTicketsQueryValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<SetTicketAgentCommandValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<GetUserQueryValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<SendMessageCommandValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateChatCommandValidator>();

// Автомаппер
builder.Services.AddAutoMapper(typeof(UserMapConfig), typeof(TicketMapConfig), typeof(TicketAssignmentMapConfig), typeof(MessageMapConfig));

// Регистрация MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(CreateTicketCommand)));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(ChangeTicketStatusCommand)));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(GetTicketQuery)));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(GetTicketsQuery)));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(SetTicketAgentCommand)));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(CreateUserCommand)));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(GetUserQuery)));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(GetAgentsQuery)));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(SendMessageCommand)));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(CreateChatCommand)));


builder.Services
    .AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        x.RequireAuthenticatedSignIn = false;
    })
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.
                GetBytes(appSettings.Secret)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000");
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DataContext>();

    // Проверяет, есть ли уже данные
    if (!context.Users.Any())
    {
        var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
        seeder.Seed(context);
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
