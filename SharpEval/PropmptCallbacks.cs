using PrettyPrompt;
using PrettyPrompt.Consoles;

namespace SharpEval
{
    internal class PropmptCallbacks : PromptCallbacks
    {
        protected override async Task<KeyPress> TransformKeyPressAsync(string text, int caret, KeyPress keyPress, CancellationToken cancellationToken)
        {
            if (keyPress.ConsoleKeyInfo.Modifiers.HasFlag(ConsoleModifiers.Alt))
            {
                var @char = keyPress.ConsoleKeyInfo.KeyChar;
                var key = keyPress.ConsoleKeyInfo.Key;
                return new KeyPress(new ConsoleKeyInfo(@char, key, false, false, false));
            }
            return await base.TransformKeyPressAsync(text, caret, keyPress, cancellationToken);
        }
    }
}