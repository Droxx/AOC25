using AOC25.Contracts;
using AOC25.Days;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "Swagger"));
}

app.UseHttpsRedirection();

app.MapPost("/run", (RunInput input) =>
    {
        IDay day = input.Day switch
        {
            Day.Day1 => new Day1(),
            Day.Day2 => new Day2(),
            Day.Day3 => new Day3(),
            Day.Day4 => new Day4(),
            _ => throw new NotImplementedException($"Day {input.Day} is not implemented.")
        };
        return input.Part switch
        {
            Part.Part1 => day.SolvePart1(input.Input),
            Part.Part2 => day.SolvePart2(input.Input),
            _ => throw new NotImplementedException($"Part {input.Part} is not implemented.")
        };
    })
    .WithName("RunDay");

app.Run();