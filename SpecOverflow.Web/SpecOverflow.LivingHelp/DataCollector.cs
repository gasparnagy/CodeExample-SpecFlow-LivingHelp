using System;
using System.Collections.Generic;
using System.Linq;

namespace SpecOverflow.LivingHelp
{
    public class DataCollector
    {
        private class BrowserEvent
        {
            public string Page { get; set; }
            public ScopeType ScopeType { get; set; }
            public string ScopeKey { get; set; }
        }

        private bool isCollecting = false;
        private readonly List<BrowserEvent> events = new List<BrowserEvent>();

        public void StartColleting()
        {
            isCollecting = true;
        }

        public void StopCollecting()
        {
            isCollecting = false;
        }

        private void RecordEvent(string url, ScopeType scopeType, string key)
        {
            if (!isCollecting)
                return;

            events.Add(new BrowserEvent { ScopeType = scopeType, ScopeKey = key, Page = GetRelativeUrl(url)});
        }

        private string GetRelativeUrl(string url)
        {
            return new Uri(url).AbsolutePath;
        }

        public void OnNavigate(string url)
        {
            RecordEvent(url, ScopeType.Navigate, GetRelativeUrl(url));
        }

        public void OnFormSubmit(string url, string formId)
        {
            RecordEvent(url, ScopeType.FormSubmit, formId); 
        }

        public void OnClick(string url, string controlId)
        {
            RecordEvent(url, ScopeType.Click, controlId);
        }

        public void MergeEntryTo(HelpDescription helpDescription, Entry entry)
        {
            var pageGroups = events.GroupBy(e => e.Page);

            foreach (var pageGroup in pageGroups)
            {
                var page = helpDescription.Pages.FirstOrDefault(p => p.Path == pageGroup.Key);
                if (page == null)
                {
                    page = new Page {Path = pageGroup.Key};
                    helpDescription.Pages.Add(page);
                }

                var scopeGroups = pageGroup.GroupBy(e => new {e.ScopeType, e.ScopeKey});
                bool hasNonNavigate = scopeGroups.Any(sg => sg.Key.ScopeType != ScopeType.Navigate);
                if (hasNonNavigate)
                    scopeGroups = scopeGroups.Where(sg => sg.Key.ScopeType != ScopeType.Navigate);

                foreach (var scopeGroup in scopeGroups)
                {
                    var scope = page.Scopes.FirstOrDefault(s => s.Type == scopeGroup.Key.ScopeType && s.Key == scopeGroup.Key.ScopeKey);
                    if (scope == null)
                    {
                        scope = new Scope {Type = scopeGroup.Key.ScopeType, Key = scopeGroup.Key.ScopeKey};
                        page.Scopes.Add(scope);
                    }

                    scope.Entries.Add(entry);
                }
            }
        }
    }
}
