using System;
using System.Threading;
using Microsoft.VisualStudio.Text.Editor;

namespace EditorConfig
{
    class AddMissingRulesActionVB : BaseSuggestedAction
    {
        private EditorConfigDocument _document;
        private ITextView _view;

        public AddMissingRulesActionVB(EditorConfigDocument document, ITextView view)
        {
            _document = document;
            _view = view;
        }

        public override string DisplayText
        {
            get { return "Basic"; }
        }

        public override void Execute(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
