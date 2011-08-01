using System.Configuration;
using TechTalk.SpecFlow;

namespace SpecOverflow.LivingHelp
{
    [Binding]
    public class DataCollectorEvents
    {
        private static HelpDescription helpDescription;
        private readonly DataCollector dataCollector;

        private ScenarioLoadResult scenarioInfo;

        public DataCollectorEvents(DataCollector dataCollector)
        {
            this.dataCollector = dataCollector;
        }

        [BeforeTestRun]
        public static void Init()
        {
            helpDescription = new HelpDescription();
        }

        [BeforeScenarioBlock]
        public void StartCollecting()
        {
            if (ScenarioContext.Current.CurrentScenarioBlock == ScenarioBlock.When)
            {
                if (scenarioInfo == null)
                    scenarioInfo = ScenarioLoader.GetCurrentScenario();

                dataCollector.StartColleting();
            }
        }

        [AfterScenarioBlock]
        public void StopCollecting()
        {
            if (ScenarioContext.Current.CurrentScenarioBlock == ScenarioBlock.When)
                dataCollector.StopCollecting();
        }

        [AfterScenario]
        public void MergeEvents()
        {
            Entry entry = new Entry
                              {
                                  FeatureTitle = FeatureContext.Current.FeatureInfo.Title,
                                  ScenarioTitle = ScenarioContext.Current.ScenarioInfo.Title,
                                  Text = scenarioInfo == null ? null : scenarioInfo.Text,
                                  Scenario = scenarioInfo == null ? null : scenarioInfo.Scenario,
                              };
            dataCollector.MergeEntryTo(helpDescription, entry);
        }

        [AfterTestRun]
        public static void FlushEvents()
        {
            var parser = new HelpDescriptionParser();
            parser.SaveTo(helpDescription, ConfigurationManager.AppSettings["livingHelpPath"]);
        }
    }
}
