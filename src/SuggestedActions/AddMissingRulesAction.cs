using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Imaging;
using Microsoft.VisualStudio.Imaging.Interop;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text.Editor;

namespace EditorConfig
{
    class AddMissingRulesAction : BaseSuggestedAction
    {
        List<Keyword> _missingRules;
        EditorConfigDocument _document;
        private ITextView _view;

        public AddMissingRulesAction(List<Keyword> missingRules, EditorConfigDocument document, ITextView view)
        {
            _missingRules = missingRules;
            _document = document;
            _view = view;
        }

        public override string DisplayText
        {
            get { return "Add Missing Rules"; }
        }
    
        public override bool HasActionSets
        {
            get { return true; }
        }

        public override ImageMoniker IconMoniker
        {
            get { return KnownMonikers.AddProperty; }
        }

        public override Task<IEnumerable<SuggestedActionSet>> GetActionSetsAsync(CancellationToken cancellationToken)
        {
            var list = new List<SuggestedActionSet>();

            var addMissingRulesActionAll = new AddMissingRulesActionAll(_missingRules, _document, _view);

            var addMissingRulesActionCSharp = new AddMissingRulesActionCSharp(_document, _view);
            var addMissingRulesActionVB = new AddMissingRulesActionVB(_document, _view);

            list.AddRange(CreateActionSet(addMissingRulesActionAll, addMissingRulesActionCSharp, addMissingRulesActionVB));
            return Task.FromResult<IEnumerable<SuggestedActionSet>>(list);
        }
   
        public IEnumerable<SuggestedActionSet> CreateActionSet(params BaseSuggestedAction[] actions)
        {
            return new[] { new SuggestedActionSet(actions) };
        }

        public override void Execute(CancellationToken cancellationToken)
        {
            // do nothing
        }

        internal static List<Keyword> FindMissingRulesAll(List<string> currentRules)
        {
            var missingRules = new List<Keyword>();
            IEnumerator<Keyword> allRules = SchemaCatalog.VisibleKeywords.GetEnumerator();
            while (allRules.MoveNext())
            {
                if (!currentRules.Contains(allRules.Current.Name) && !allRules.Current.Name.StartsWith("dotnet_naming"))
                {
                    missingRules.Add(allRules.Current);
                }
            }

            if (missingRules.Count() == 0)
            {
                return null;
            }

            return missingRules;
        }

        // TO-DO: Find missing rules for C#, Core, VB, .NET
        // Should .NET rules include C# and VB rules, or just general .NET rules? Hmm.... Also, should check if language identifier is present.
        private List<Keyword> FindMissingRulesDotNet(string language)
        {
            foreach (Keyword curRule in _missingRules)
            {
                if (curRule.Name.StartsWith(language))
                {
                    specificMissingRules.Add(curRule);
                }
            }
        }

        private static class LanguageNames
        {
            public const string CSharp = "csharp";

            public const string VisualBasic = "visual_basic";

            public const string DotNet = "dotnet";
        }
    }
}
