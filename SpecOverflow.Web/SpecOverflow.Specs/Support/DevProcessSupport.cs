using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace SpecOverflow.Specs.Support
{
    [Binding]
    public class DevProcessSupport
    {
        [BeforeScenario("specified")]
        public void IgnoreSpecifiedTests()
        {
            Assert.Inconclusive("scenario pending");
        }
    }
}
