using PrettyPrompt;
using PrettyPrompt.Completion;
using PrettyPrompt.Consoles;
using PrettyPrompt.Documents;
using PrettyPrompt.Highlighting;

using SharpEval.Core.IO;

namespace SharpEval
{
    internal class PropmptCallbacks : PromptCallbacks
    {
        private readonly IDocumentationProvider _documentationProvider;

        public PropmptCallbacks(IDocumentationProvider documentationProvider)
        {
            _documentationProvider = documentationProvider;
        }

        protected override Task<(string Text, int Caret)> FormatInput(string text, int caret, KeyPress keyPress, CancellationToken cancellationToken)
        {
            if (text.StartsWith("$$"))
            {
                return base.FormatInput(text.Replace("$$", "$"), caret - 1, keyPress, cancellationToken);
            }
            return base.FormatInput(text, caret, keyPress, cancellationToken);
        }

        protected override async Task<KeyPress> TransformKeyPressAsync(string text,
                                                                       int caret,
                                                                       KeyPress keyPress,
                                                                       CancellationToken cancellationToken)
        {
            if (keyPress.ConsoleKeyInfo.Modifiers.HasFlag(ConsoleModifiers.Alt))
            {
                var @char = keyPress.ConsoleKeyInfo.KeyChar;
                var key = keyPress.ConsoleKeyInfo.Key;
                return new KeyPress(new ConsoleKeyInfo(@char, key, false, false, false));
            }
            return await base.TransformKeyPressAsync(text, caret, keyPress, cancellationToken);
        }

        protected override Task<IReadOnlyList<CompletionItem>> GetCompletionItemsAsync(string text,
                                                                                       int caret,
                                                                                       TextSpan spanToBeReplaced,
                                                                                       CancellationToken cancellationToken)
        {
            var completionItems = new List<CompletionItem>();

            var keyWord = text.Substring(spanToBeReplaced.Start);

            var candidates = _documentationProvider
                 .GetDocumentations()
                 .Where(x => x.Key.StartsWith(keyWord, StringComparison.InvariantCultureIgnoreCase))
                 .OrderBy(x => x.Key);

            foreach (var candidate in candidates)
            {
                var description = string.Join(Environment.NewLine, candidate.Value);
                completionItems.Add(new CompletionItem(candidate.Key, default, null, _ =>
                {
                    return Task.FromResult(new FormattedString(description));
                }));
            }

            return Task.FromResult((IReadOnlyList<CompletionItem>)completionItems);
        }
    }
}