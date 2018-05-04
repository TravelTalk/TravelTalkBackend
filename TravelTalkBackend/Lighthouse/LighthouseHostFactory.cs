 namespace Lighthouse {
     using System.Collections.Generic;
     using System.IO;
     using System.Linq;
     using Akka.Actor;
     using Akka.Configuration;
     using ConstantContent;
     using Feitzinger.TyrolSky.Utility.InitializationHelper.Logging;

     /// <summary>
     ///     Launcher for the Lighthouse <see cref="ActorSystem"/>
     /// </summary>
     public static class LighthouseHostFactory {
         public static ActorSystem LaunchLighthouse() {
             LoggerInitialization.InitLogger();

             Config clusterConfig = ConfigurationFactory.ParseString(File.ReadAllText(ResourceFileNames.LIGHTHOUSE_CONFIG_NAME));

             Config remoteConfig = clusterConfig.GetConfig("akka.remote");
             string hostname = remoteConfig.GetString("dot-netty.tcp.hostname");
             int port = remoteConfig.GetInt("dot-netty.tcp.port");

             if (port == 0) {
                 throw new ConfigurationException(
                         "Need to specify an explicit port for Lighthouse. Found an undefined port or a port value of 0 in App.config.");
             }

             string selfAddress = string.Format("akka.tcp://{0}@{1}:{2}", ActorSystemValues.ACTOR_SYSTEM_NAME, hostname, port);
             IList<string> seeds = clusterConfig.GetStringList("akka.cluster.seed-nodes");
             if (!seeds.Contains(selfAddress)) {
                 seeds.Add(selfAddress);
             }

             string injectedClusterConfigString = seeds.Aggregate("akka.cluster.seed-nodes = [", (current, seed) => current + @"""" + seed + @""", ");
             injectedClusterConfigString += "]";

             Config finalConfig = ConfigurationFactory.ParseString(
                             string.Format(@"akka.remote.dot-netty.tcp.public-hostname = {0} 
                                             akka.remote.dot-netty.tcp.port = {1}", hostname, port))
                     .WithFallback(ConfigurationFactory.ParseString(injectedClusterConfigString))
                     .WithFallback(clusterConfig);

             return ActorSystem.Create(ActorSystemValues.ACTOR_SYSTEM_NAME, finalConfig);
         }
     }
 }
