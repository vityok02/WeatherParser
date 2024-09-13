using Telegram.Bot.Types.ReplyMarkups;
using Application.Common.Abstract;

namespace Bot
{
    public class AppKeyboardMarkup : IAppReplyMarkup
    {
        public ReplyKeyboardMarkup TelegramReplyKeyboardMarkup { get; }

        public AppKeyboardMarkup(string[] buttons)
        {
            var keyboardButtons = GetKeyboardButtons(buttons);
            TelegramReplyKeyboardMarkup = new ReplyKeyboardMarkup(keyboardButtons);
        }

        public AppKeyboardMarkup(IEnumerable<IEnumerable<string>> buttons)
        {
            TelegramReplyKeyboardMarkup = GetKeyboard(buttons);
        }

        private KeyboardButton[][] GetKeyboardButtons(string[] elements)
        {
            var keyboardButtons = new KeyboardButton[elements.Length][];

            for (int i = 0; i < elements.Length; i++)
            {
                keyboardButtons[i] = [new KeyboardButton(elements[i])];
            }
            return keyboardButtons;
        }

        private ReplyKeyboardMarkup GetKeyboard(IEnumerable<IEnumerable<string>> buttons)
        {
            IEnumerable<IEnumerable<KeyboardButton>> keyboard = buttons
                .Select(b => b.Select(x => new KeyboardButton(x)));
            return new ReplyKeyboardMarkup(keyboard);
        }
    }
}
