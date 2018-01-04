﻿using Retina.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retina.Stages
{
    public class PerLineStage : Stage
    {
        public Stage ChildStage { get; set; }

        public PerLineStage(Config config, Stage childStage)
            : base(config)
        {
            ChildStage = childStage;
        }

        public override string Execute(string input, TextWriter output)
        {
            string[] lines = input.Split(new[] { '\n' });

            IEnumerable<string> resultLines = lines.Select((x, i) => {
                if (Config.GetLimit(0).IsInRange(i, lines.Length))
                    return ChildStage.Execute(x, output);
                else
                    return x;
            });

            string result = String.Join("\n", resultLines);
            
            return result;
        }
    }
}