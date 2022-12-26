using Elsa.Extensions;
using Elsa.Http.Extensions;
using Elsa.Samples.HelloWorld;
using Elsa.Workflows.Runtime.Extensions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add Elsa services.
services.AddElsa(elsa => elsa
    // Configure the workflow runtime.
    .UseWorkflowRuntime(runtime =>
    {
        // Register our HTTP workflow with the runtime.
        runtime.AddWorkflow<HelloWorldHttpWorkflow>();
    })
        
    // Enable Elsa HTTP module (for HTTP related activities). 
    .UseHttp()
);

// Configure middleware pipeline.
var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

// Add Elsa HTTP middleware (to handle requests mapped to HTTP Endpoint activities)
app.UseHttpActivities();

// Start accepting requests.
app.Run();