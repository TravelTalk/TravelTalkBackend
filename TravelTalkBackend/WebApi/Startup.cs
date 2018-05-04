namespace TravelTalk.WebApi {
    using System.IO;
    using Akka.Actor;
    using Akka.Configuration;
    using Akka.Routing;
    using Commands.GeneralCommandHandling;
    using ConstantContent;
    using Feitzinger.TyrolSky.Utility.InitializationHelper.Logging;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class Startup {
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

            ActorSystem.ActorOf(Props.Create<CommandHandlerActor>().WithRouter(FromConfig.Instance), "commandHandlerRouter");

            services.AddSingleton(typeof(ActorSystem), serviceProvider => ActorSystem);

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment hostingEnvironment, ILoggerFactory loggerFactory,
                              IApplicationLifetime applicationLifetime) {
            if (hostingEnvironment.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            applicationLifetime.ApplicationStopping.Register(async () => await CoordinatedShutdown.Get(ActorSystem).Run());

            app.UseMvc();
        }
    }
}
