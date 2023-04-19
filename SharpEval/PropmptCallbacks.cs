using PrettyPrompt;
using PrettyPrompt.Completion;
using PrettyPrompt.Consoles;
using PrettyPrompt.Documents;
using PrettyPrompt.Highlighting;

namespace SharpEval
{
    internal class PropmptCallbacks : PromptCallbacks
    {
        private readonly IDictionary<string, List<string>> _documentation;

        public PropmptCallbacks(IDictionary<string, List<string>> documentation)
        {
            _documentation = documentation;
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

            var candidates = _documentation
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