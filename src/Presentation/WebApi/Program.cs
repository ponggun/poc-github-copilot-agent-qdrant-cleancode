using PocGithubCopilotAgentQdrantCleancode.Domain.Interfaces;
using PocGithubCopilotAgentQdrantCleancode.Application.Interfaces;
using PocGithubCopilotAgentQdrantCleancode.Application.Services;
using PocGithubCopilotAgentQdrantCleancode.Infrastructure.Repositories;
using PocGithubCopilotAgentQdrantCleancode.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register application services
builder.Services.AddScoped<IEmbeddingService, OpenAIEmbeddingService>();
builder.Services.AddScoped<IRepository, QdrantRepository>();
builder.Services.AddScoped<IDocumentService, DocumentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
