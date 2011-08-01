using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow.Parser.SyntaxElements;

namespace SpecOverflow.LivingHelp
{
    public static class ScenarioFormatter
    {
        public static string FormatHtml(Scenario scenario)
        {
            StringBuilder formettedScenario = new StringBuilder();

            formettedScenario.AppendLine("<div class='gherkin-scenario'>");

            ScenarioOutline scenarioOutline = scenario as ScenarioOutline;

            foreach (var scenarioStep in scenario.Steps)
                AddStep(scenarioStep, scenarioOutline != null, formettedScenario);

            if (scenarioOutline != null)
            {
                //TODO
            }

            formettedScenario.AppendLine("</div>");

            return formettedScenario.ToString();
        }

        private static void AddStep(ScenarioStep scenarioStep, bool isScenarioOutline, StringBuilder formettedScenario)
        {
            formettedScenario.Append("<p");
            if (scenarioStep is And || scenarioStep is But)
                formettedScenario.Append(" class='gherkin-step gherkin-and-step'");
            else
                formettedScenario.Append(" class='gherkin-step'");
            formettedScenario.AppendLine(">");

            formettedScenario.AppendFormat("<span class='gherkin-keyword'>{0}</span>", scenarioStep.Keyword);

            formettedScenario.AppendFormat("<span>{0}</span>", scenarioStep.Text);
            formettedScenario.AppendLine("</p>");

            if (scenarioStep.MultiLineTextArgument != null)
            {
                //TODO
            }
            if (scenarioStep.TableArg != null)
            {
                AddTable(scenarioStep.TableArg, formettedScenario);
            }
        }

        private static void AddTable(GherkinTable tableArg, StringBuilder formettedScenario)
        {
            formettedScenario.AppendLine("<table class='gherkin-table-arg'>");
            AddTableRow(tableArg.Header, true, formettedScenario);
            foreach (var row in tableArg.Body)
            {
                AddTableRow(row, false, formettedScenario);
            }
            formettedScenario.AppendLine("</table>");
        }

        private static void AddTableRow(GherkinTableRow row, bool isHeader, StringBuilder formettedScenario)
        {
            string elementName = isHeader ? "th" : "td";

            formettedScenario.AppendLine("<tr>");
            foreach (var cell in row.Cells)
            {
                formettedScenario.AppendFormat("<{0}>{1}</{0}>", elementName, string.IsNullOrWhiteSpace(cell.Value) ? "&nbsp;" : cell.Value);
            }
            formettedScenario.AppendLine("</tr>");
        }
    }
}
