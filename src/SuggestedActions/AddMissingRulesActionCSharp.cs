using System.Threading;
using Microsoft.VisualStudio.Text.Editor;

namespace EditorConfig
{
    class AddMissingRulesActionCSharp : BaseSuggestedAction
    {
        private EditorConfigDocument _document;
        private ITextView _view;

        public AddMissingRulesActionCSharp(EditorConfigDocument document, ITextView view)
        {
            _document = document;
            _view = view;
        }

        public override string DisplayText
        {
            get { return "C#"; }
        }

        public override void Execute(CancellationToken cancellationToken)
        {
 
        }
    }
}
