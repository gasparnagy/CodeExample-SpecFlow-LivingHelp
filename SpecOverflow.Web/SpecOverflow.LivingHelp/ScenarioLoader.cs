using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Parser;
using TechTalk.SpecFlow.Parser.SyntaxElements;

namespace SpecOverflow.LivingHelp
{
    public class ScenarioLoadResult
    {
        public string Text { get; set; }
        public Scenario Scenario { get; set; }
    }

    public static class ScenarioLoader
    {
        public static ScenarioLoadResult GetCurrentScenario()
        {
            string fileName = GetCurrentFeatureFile();
            if (fileName == null)
                return null;

            var feature = ParseGherkin(fileName);

            var scenario = feature.Scenarios.FirstOrDefault(s => s.Title == ScenarioContext.Current.ScenarioInfo.Title);
            if (scenario == null)
                return null;

            string plainText = GetPlainText(fileName, scenario, feature);

            return new ScenarioLoadResult {Text = plainText, Scenario = scenario};
        }

        private static string GetPlainText(string fileName, Scenario scenario, Feature feature)
        {
            var lines = File.ReadAllLines(fileName);

            var nextScenario = feature.Scenarios.SkipWhile(s => s != scenario).Skip(1).FirstOrDefault();

            int startLine = scenario.FilePosition.Line;
            int endLine = nextScenario == null ? lines.Count() : nextScenario.FilePosition.Line - 1;

            return string.Join(Environment.NewLine, lines.Skip(startLine - 1).Take(endLine - startLine + 1)).Replace("\t", "  ");
        }

        private static Feature ParseGherkin(string fileName)
        {
            using (var featureFileReader = new StreamReader(fileName))
            {
                var specFlowLangParser = new SpecFlowLangParser(CultureInfo.CurrentCulture);
                var feature = specFlowLangParser.Parse(featureFileReader, fileName);
                return feature;
            }
        }

        private static string GetCurrentFeatureFile()
        {
            try
            {
                var frames = new StackTrace(true).GetFrames();
                if (frames == null)
                    return null;

                return frames.Select(f => f.GetFileName()).FirstOrDefault(fn => fn != null && fn.EndsWith(".feature"));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex, "GetCurrentPositionText");
                return null;
            }
        }
    }
}
