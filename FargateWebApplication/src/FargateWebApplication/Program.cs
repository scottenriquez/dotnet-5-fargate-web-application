using Amazon.CDK;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FargateWebApplication
{
    sealed class Program
    {
        public static void Main(string[] args)
        {
            App app = new App();
            new FargateWebApplicationStack(app, "FargateWebApplicationStack");
            app.Synth();
        }
    }
}
