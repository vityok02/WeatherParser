using Telegram.Bot.Types.ReplyMarkups;
using Application.Interfaces.ReplyMarkup;

namespace Bot
{
    public class KeyboardMarkup : IAppReplyMarkup
    {
        public ReplyKeyboardMarkup TelegramReplyKeyboardMarkup { get; }

        public KeyboardMarkup(string[] elements)
        {
            var keyboardButtons = GetKeyboardButtons(elements);
            TelegramReplyKeyboardMarkup = new ReplyKeyboardMarkup(keyboardButtons);
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
    }
}
