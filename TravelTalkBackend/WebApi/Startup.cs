namespace TravelTalk.WebApi {
    using System.IO;
    using Akka.Actor;
    using Akka.Configuration;
    using Akka.Routing;
    using ApplicationService.Comon;
    using Commands.CommandHandler;
    using ConstantContent;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json.Serialization;

    public class Startup {
        
        private const string CONTROLLER_ACTION_ROUTES = "ControllerActionRoutes";

        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private ActorSystem ActorSystem { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            LoggerInitialization.InitLogger();

            Config config = ConfigurationFactory.ParseString(File.ReadAllText(ResourceFileNames.WEP_API_CONFIG_FILE_NAME));

            ActorSystem = ActorSystem.Create(ActorSystemValues.ACTOR_SYSTEM_NAME, config);

            ActorSystem.ActorOf(Props.Create<CommandDispatcherActor>().WithRouter(FromConfig.Instance), 
                    ActorConstants.COMMAND_HANDLER_GROUP_ROUTER_NAME);

            services.AddSingleton(typeof(ActorSystem), serviceProvider => ActorSystem)
                    .AddMvc()
                    .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment hostingEnvironment, ILoggerFactory loggerFactory,
                              IApplicationLifetime applicationLifetime) {
            if (hostingEnvironment.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            applicationLifetime.ApplicationStopping.Register(async () => await CoordinatedShutdown.Get(ActorSystem).Run());

            app.UseMvc(routeBuilder => {
                routeBuilder.MapRoute(CONTROLLER_ACTION_ROUTES,
                        $"{WebApiConstants.API_ROUTES_PREFIX}/{WebApiConstants.CONTROLLER_ROUTE_PART}/{WebApiConstants.ACTION_ROUTE_PART}");
            });
        }
    }
}
