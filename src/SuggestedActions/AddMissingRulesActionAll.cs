using System.Collections.Generic;
using System.Threading;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;

namespace EditorConfig
{
    class AddMissingRulesActionAll : BaseSuggestedAction
    {
        private List<Keyword> _missingRules;
        private EditorConfigDocument _document;
        private ITextView _view;

        public AddMissingRulesActionAll(List<Keyword> missingRules, EditorConfigDocument document, ITextView view)
        {
            _missingRules = missingRules;
            _document = document;
            _view = view;
        }
        public override string DisplayText
        {
            get { return "All"; }
        }

        public override void Execute(CancellationToken cancellationToken)
        {
            SnapshotPoint caretPost = _view.Caret.Position.BufferPosition;

            using (ITextEdit edit = _view.TextBuffer.CreateEdit())
            {


                if (edit.HasEffectiveChanges)
                    edit.Apply();
            }

            _view.Caret.MoveTo(new SnapshotPoint(_view.TextBuffer.CurrentSnapshot, caretPost));
        }
    }
}
