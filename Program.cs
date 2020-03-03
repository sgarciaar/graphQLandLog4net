using System;
using System.IO;
using System.Reflection;
using GraphQL;
using GraphQL.Types;
using log4net;
using log4net.Config;

namespace Test
{
    class Program
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        static void Main(string[] args)
        {
            Logger.Error("test OK");
            Logger.Info("Hello logging world!");
            Logger.Error("Error!");
            Logger.Warn("Warn!");


            var schema = Schema.For(@"
              type Jedi {
                  name: String,
                  side: String,
                  id: ID
              }

              type Query {
                  hello: String,
                  jedis: [Jedi],
                  jedi(id: ID): Jedi
              }
              ", _ =>
            {
                _.Types.Include<Query>();
            });

            var root = new { Hello = "Hello World!" };

            var json = schema.Execute(_ =>
            {
                _.Query = "{ jedi(id: 1) { name } }";                
            });
           
            Console.WriteLine(json);

            Logger.Error("test OK");
        }
    }
}
