
using System;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Net.Http;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Specialized;
using Telegram.Bot.Types;
using System.Text.RegularExpressions;

namespace Bot
{
    class Program
    {
        private static readonly TelegramBotClient Bot = new TelegramBotClient("1089266960:AAGzGVq-eKfVlNnV2Lt1f_Uer3TQYOd1uKQ");
        public static string Title = string.Empty;
        static void Main(string[] args)
        {
            Bot.OnMessage += BotMessage;
            Bot.OnMessageEdited += BotMessage;
            Bot.OnCallbackQuery += BotMessage_Callback;

            var bot = Bot.GetMeAsync().Result;
            Console.WriteLine(bot.FirstName);

            Bot.StartReceiving();
            Console.ReadLine();
            Bot.StopReceiving();
        }
        private static async void BotMessage(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            string name = $"{message.From.FirstName} {message.From.LastName}";
            Console.WriteLine($"'{name}' отправил сообщение: '{message.Text}'");

            if (message.Text == null || message.Type != MessageType.Text)
                return;
            switch (message.Text)
            {
                case "/start":
                    await Bot.SendTextMessageAsync(message.Chat.Id, "Hi, dear friend! I`m a bot for searching books✨\nI wiil help you to search books what you want to read🎀\n   List of commands:\n/start - start of the bot\n/freeebook - to search free e-books\n/searchbook - categories to search books\n/sitesofreadbooks -list of the sites to read books\n/sitesofbuybooks - list of the sites to buy books\n/paidebooks - to search paid e-books\n/addbooks - to add books to favorite list\n/showbooks - to show your favorite list\n/deletebooks - to delete books from favorite list\n\n✨All books you will find here, you can read/buy at Google Books");
                    break;
                case "/sitesofreadbooks":
                    await Bot.SendTextMessageAsync(message.Chat.Id, "📚 I advice you some sites where you can read books online",
                replyMarkup: sitesRead);
                    break;
                case "/sitesofbuybooks":
                    await Bot.SendTextMessageAsync(message.Chat.Id, "📚 I advice you some sites where you can buy books online",
                 replyMarkup: sitesBuy);
                    break;
                case "/searchbook":
                    var keyboard = new InlineKeyboardMarkup(new[]
                                        {
                                          new[] {InlineKeyboardButton.WithCallbackData("Genre","genre") },
                                          new[] {InlineKeyboardButton.WithCallbackData("Author","author") },
                                          new[] {InlineKeyboardButton.WithCallbackData("Title","title") }
                                        });
                    await Bot.SendTextMessageAsync(message.Chat.Id, "📖 Select a categorie to search books",
                 replyMarkup: keyboard);
                    break;
                case "/freeebook":
                    await Bot.SendTextMessageAsync(message.Chat.Id, "📝Please, write a name of searching free e-book", replyMarkup: new ForceReplyMarkup { Selective = true });
                    break;
                case "/addbooks":
                    await Bot.SendTextMessageAsync(message.Chat.Id, "📝Please, write a name of book what you want to add to your list", replyMarkup: new ForceReplyMarkup { Selective = true });
                    break;
                case "/showbooks":
                    string url1 = "https://booksearch1.azurewebsites.net/api/list";
                    HttpClient client = new HttpClient();
                    var result = await client.GetStringAsync(url1);
                    Temp[] jsi = JsonConvert.DeserializeObject<Temp[]>(result);
                    foreach (var item in jsi)
                    {
                        await Bot.SendTextMessageAsync(message.Chat.Id, item.Id + ". " + item.Name);
                    }
                    break;
                case "/deletebooks":
                    await Bot.SendTextMessageAsync(message.Chat.Id, "📝Please, write a number of book what you want to delete from your list", replyMarkup: new ForceReplyMarkup { Selective = true });
                    break;
                case "/paidebooks":
                    await Bot.SendTextMessageAsync(message.Chat.Id, "📝Please, write a name of searching paid e-book", replyMarkup: new ForceReplyMarkup { Selective = true });
                    break;
            }
            if (message.ReplyToMessage != null && message.ReplyToMessage.Text.Contains("📝Please, write a name of searching free e-book"))
            {
                try { 
                    string url1 = "https://booksearch1.azurewebsites.net/api/read/" + message.Text;
                    HttpClient client = new HttpClient();
                    var result = await client.GetStringAsync(url1);
                    Info jsi = JsonConvert.DeserializeObject<Info>(result);
                    foreach (var kl in jsi.Items)
                    {
                    string title = kl.VolumeInfo.Title;
                    string title1 = kl.VolumeInfo.Description;
                    var title2 = kl.VolumeInfo.Authors;
                    foreach (var item in title2)
                    {
                        if (title1 != null && item != null)
                        {
                            await Bot.SendTextMessageAsync(message.Chat.Id, "✨The title: " + title + "\n" + "✨Author: " + item + "\n" + "✨The plot: \n" + title1);
                        }
                        else
                        {
                            await Bot.SendTextMessageAsync(message.Chat.Id, "✨The title: " + title);
                        }
                    }
                    }
            }
                catch
            {
                await Bot.SendTextMessageAsync(message.Chat.Id, "Sorry, i didn't find such a book🥺");
            }
        }
            if (message.ReplyToMessage != null && message.ReplyToMessage.Text.Contains("📝Please, write a name of searching paid e-book"))
            {
                try
                {
                    string url1 = "https://booksearch1.azurewebsites.net/api/mi/" + message.Text;
                    HttpClient client = new HttpClient();
                    var result = await client.GetStringAsync(url1);
                    Info jsi = JsonConvert.DeserializeObject<Info>(result);
                    foreach (var kl in jsi.Items)
                    {
                        string title = kl.VolumeInfo.Title;
                        string title1 = kl.VolumeInfo.Description;
                        var title2 = kl.VolumeInfo.Authors;
                        foreach (var item in title2)
                        {
                            if (title1 != null && item != null)
                            {
                                await Bot.SendTextMessageAsync(message.Chat.Id, "✨The title: " + title + "\n" + "✨Author: " + item + "\n" + "✨The plot: \n" + title1);
                            }
                            else
                            {
                                await Bot.SendTextMessageAsync(message.Chat.Id, "✨The title: " + title);
                            }
                        }
                    }
                }
                catch
                {
                    await Bot.SendTextMessageAsync(message.Chat.Id, "Sorry, i didn't find such a book🥺");
                }
            }
            if (message.ReplyToMessage != null && message.ReplyToMessage.Text.Contains("📝Please, write a name of book what you want to add to your list"))
            {
                var book = new Book();
                book.Name = message.Text;

                var json = JsonConvert.SerializeObject(book);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                string url1 = "https://booksearch1.azurewebsites.net/api/list";

                HttpClient client = new HttpClient();

                var result1 = await client.PostAsync(url1, data);

                await Bot.SendTextMessageAsync(message.Chat.Id, "📝This book is added to your list");
            }
            if (message.ReplyToMessage != null && message.ReplyToMessage.Text.Contains("📝Please, write a number of book what you want to delete from your list"))
            {
                string url1 = "https://booksearch1.azurewebsites.net/api/list/" + message.Text;
                HttpClient client = new HttpClient();
                var result1 = await client.DeleteAsync(url1);
                    if (Regex.IsMatch(message.Text, @"^[0-9]+$"))
                        await Bot.SendTextMessageAsync(message.Chat.Id, "This book is deleted from your list");
                    else
                        await Bot.SendTextMessageAsync(message.Chat.Id, "Please, write a NUMBER of book what you want to delete from your list");
            }
            if (message.ReplyToMessage != null && message.ReplyToMessage.Text.Contains("Write an author to search books"))
                {
                try
                {
                    string url1 = "https://booksearch1.azurewebsites.net/api/inauthor/" + message.Text;
                    HttpClient client = new HttpClient();
                    var result = await client.GetStringAsync(url1);
                    Info jsi = JsonConvert.DeserializeObject<Info>(result);
                    foreach (var kl in jsi.Items)
                    {
                        string title = kl.VolumeInfo.Title;
                        string title1 = kl.VolumeInfo.Description;
                        var title2 = kl.VolumeInfo.Authors;
                        foreach (var item in title2)
                        {
                            if (title1 != null && item != null)
                            {
                                await Bot.SendTextMessageAsync(message.Chat.Id, "✨The title: " + title + "\n" + "✨Author: " + item + "\n" + "✨The plot: \n" + title1);
                            }
                            else
                            {
                                await Bot.SendTextMessageAsync(message.Chat.Id, "✨The title: " + title);
                            }
                        }
                    }
                }
                catch
                {
                    await Bot.SendTextMessageAsync(message.Chat.Id, "Sorry, i didn't find such a book🥺");
                }
            }
            if (message.ReplyToMessage != null && message.ReplyToMessage.Text.Contains("Write a title to search books"))
            {
                try
                {
                    string url1 = "https://booksearch1.azurewebsites.net/api/title/" + message.Text;
                    HttpClient client = new HttpClient();
                    var result = await client.GetStringAsync(url1);
                    Info jsi = JsonConvert.DeserializeObject<Info>(result);
                    string title;
                    foreach (var kl in jsi.Items)
                    {
                        title = kl.VolumeInfo.Title;
                        string title1 = kl.VolumeInfo.Description;
                        var title2 = kl.VolumeInfo.Authors;
                        foreach (var item in title2)
                        {
                            if (title1 != null)
                            {
                                await Bot.SendTextMessageAsync(message.Chat.Id, "✨The title: " + title + "\n" + "✨Author: " + item + "\n" + "✨The plot: \n" + title1);
                            }
                            else
                            {
                                await Bot.SendTextMessageAsync(message.Chat.Id, "✨The title: " + title + "\n" + "✨Author: " + item);
                            }
                        }
                    }
                }
                catch
                {
                    await Bot.SendTextMessageAsync(message.Chat.Id, "Sorry, i didn't find such a book🥺");
                }
            }
        }
        private static async void BotMessage_Callback(object sc, CallbackQueryEventArgs ev)
        {
            var message = ev.CallbackQuery.Message;

            if (ev.CallbackQuery.Data == "genre")
            {
                var keyboard1 = new InlineKeyboardMarkup(new[]
                {
                                          new[] {InlineKeyboardButton.WithCallbackData("Thriller","thriller") },
                                          new[] {InlineKeyboardButton.WithCallbackData("Fantasy","fantasy") },
                                          new[] {InlineKeyboardButton.WithCallbackData("Drama","drama") },
                                          new[] {InlineKeyboardButton.WithCallbackData("Crime","crime") },
                                          new[] {InlineKeyboardButton.WithCallbackData("Classic","classic") },
                                          new[] {InlineKeyboardButton.WithCallbackData("Fiction","fiction") },
                                          new[] {InlineKeyboardButton.WithCallbackData("Romance","romance") },
                                          new[] {InlineKeyboardButton.WithCallbackData("Horror","horror") },
                                          new[] {InlineKeyboardButton.WithCallbackData("Science","science") },
                                          new[] {InlineKeyboardButton.WithCallbackData("History","history") },
                                          new[] {InlineKeyboardButton.WithCallbackData("Humor","humor") },
                                          new[] {InlineKeyboardButton.WithCallbackData("Tragedy","tragedy") }
                });
                await Bot.SendTextMessageAsync(message.Chat.Id, "📚 Select a genre to search books",
                replyMarkup: keyboard1);
            }
            else
            if (ev.CallbackQuery.Data == "author")
            {
                await Bot.SendTextMessageAsync(message.Chat.Id, "Write an author to search books", replyMarkup: new ForceReplyMarkup { Selective = true });
            }
            if (ev.CallbackQuery.Data == "title")
            {
                await Bot.SendTextMessageAsync(message.Chat.Id, "Write a title to search books", replyMarkup: new ForceReplyMarkup { Selective = true });
            }
            else
            if (ev.CallbackQuery.Data == "thriller")
            {
                string url1 = "https://booksearch1.azurewebsites.net/api/genre/thriller";
                HttpClient client = new HttpClient();
                var result = await client.GetStringAsync(url1);
                Info jsi = JsonConvert.DeserializeObject<Info>(result);
                await Bot.SendTextMessageAsync(message.Chat.Id, "Thriller:\n" );
                foreach (var kl in jsi.Items)
                {
                    string title = kl.VolumeInfo.Title;
                    var title2 = kl.VolumeInfo.Authors;
                    foreach (var item in title2)
                    {
                        await Bot.SendTextMessageAsync(message.Chat.Id, "✨The title: " + title + "\n" + "✨Author: " + item);
                    }
                }
            }
            else
            if (ev.CallbackQuery.Data == "fantasy")
            {
                string url1 = "https://booksearch1.azurewebsites.net/api/genre/fantasy";
                HttpClient client = new HttpClient();
                var result = await client.GetStringAsync(url1);
                Info jsi = JsonConvert.DeserializeObject<Info>(result);
                await Bot.SendTextMessageAsync(message.Chat.Id, "Fantasy:\n" );
                foreach (var kl in jsi.Items)
                {
                    string title = kl.VolumeInfo.Title;
                    var title2 = kl.VolumeInfo.Authors;
                    foreach (var item in title2)
                    {
                        await Bot.SendTextMessageAsync(message.Chat.Id, "✨The title: " + title + "\n" + "✨Author: " + item);
                    }
                }
            }
            else
            if (ev.CallbackQuery.Data == "drama")
            {
                string url1 = "https://booksearch1.azurewebsites.net/api/genre/drama";

                HttpClient client = new HttpClient();

                var result = await client.GetStringAsync(url1);

                Info jsi = JsonConvert.DeserializeObject<Info>(result);
                await Bot.SendTextMessageAsync(message.Chat.Id, "Drama:\n");
                foreach (var kl in jsi.Items)
                {
                    string title = kl.VolumeInfo.Title;
                    var title2 = kl.VolumeInfo.Authors;
                    foreach (var item in title2)
                    {
                        await Bot.SendTextMessageAsync(message.Chat.Id, "✨The title: " + title + "\n" + "✨Author: " + item);
                    }
                }
            }
            else
            if (ev.CallbackQuery.Data == "crime")
            {
                string url1 = "https://booksearch1.azurewebsites.net/api/genre/crime";

                HttpClient client = new HttpClient();

                var result = await client.GetStringAsync(url1);

                Info jsi = JsonConvert.DeserializeObject<Info>(result);
                await Bot.SendTextMessageAsync(message.Chat.Id, "Crime:\n");
                foreach (var kl in jsi.Items)
                {
                    string title = kl.VolumeInfo.Title;
                    var title2 = kl.VolumeInfo.Authors;
                    foreach (var item in title2)
                    {
                        await Bot.SendTextMessageAsync(message.Chat.Id, "✨The title: " + title + "\n" + "✨Author: " + item);
                    }
                }
            }
            else
            if (ev.CallbackQuery.Data == "classic")
            {
                string url1 = "https://booksearch1.azurewebsites.net/api/genre/classic";

                HttpClient client = new HttpClient();

                var result = await client.GetStringAsync(url1);

                Info jsi = JsonConvert.DeserializeObject<Info>(result);
                await Bot.SendTextMessageAsync(message.Chat.Id, "Classic:\n");
                foreach (var kl in jsi.Items)
                {
                    string title = kl.VolumeInfo.Title;
                    var title2 = kl.VolumeInfo.Authors;
                    foreach (var item in title2)
                    {
                        await Bot.SendTextMessageAsync(message.Chat.Id, "✨The title: " + title + "\n" + "✨Author: " + item);
                    }
                }
            }
            else
            if (ev.CallbackQuery.Data == "fiction")
            {
                string url1 = "https://booksearch1.azurewebsites.net/api/genre/fiction";

                HttpClient client = new HttpClient();

                var result = await client.GetStringAsync(url1);

                Info jsi = JsonConvert.DeserializeObject<Info>(result);
                await Bot.SendTextMessageAsync(message.Chat.Id, "Fiction:\n");
                foreach (var kl in jsi.Items)
                {
                    string title = kl.VolumeInfo.Title;
                    var title2 = kl.VolumeInfo.Authors;
                    foreach (var item in title2)
                    {
                        await Bot.SendTextMessageAsync(message.Chat.Id, "✨The title: " + title + "\n" + "✨Author: " + item);
                    }
                }
            }
            else
            if (ev.CallbackQuery.Data == "romance")
            {
                string url1 = "https://booksearch1.azurewebsites.net/api/genre/romance";

                HttpClient client = new HttpClient();

                var result = await client.GetStringAsync(url1);

                Info jsi = JsonConvert.DeserializeObject<Info>(result);
                await Bot.SendTextMessageAsync(message.Chat.Id, "Romance:\n");
                foreach (var kl in jsi.Items)
                {
                    string title = kl.VolumeInfo.Title;
                    var title2 = kl.VolumeInfo.Authors;
                    foreach (var item in title2)
                    {
                        await Bot.SendTextMessageAsync(message.Chat.Id, "✨The title: " + title + "\n" + "✨Author: " + item);
                    }
                }
            }
            else
            if (ev.CallbackQuery.Data == "horror")
            {
                string url1 = "https://booksearch1.azurewebsites.net/api/genre/horror";

                HttpClient client = new HttpClient();

                var result = await client.GetStringAsync(url1);

                Info jsi = JsonConvert.DeserializeObject<Info>(result);
                await Bot.SendTextMessageAsync(message.Chat.Id, "Horror:\n");
                foreach (var kl in jsi.Items)
                {
                    var title = kl.VolumeInfo.Title;
                    var title2 = kl.VolumeInfo.Authors;
                    foreach (var item in title2)
                    {
                        await Bot.SendTextMessageAsync(message.Chat.Id, "✨The title: " + title + "\n" + "✨Author: " + item);
                    }
                }
            }
            else
            if (ev.CallbackQuery.Data == "science")
            {
                string url1 = "https://booksearch1.azurewebsites.net/api/genre/science";

                HttpClient client = new HttpClient();

                var result = await client.GetStringAsync(url1);

                Info jsi = JsonConvert.DeserializeObject<Info>(result);
                await Bot.SendTextMessageAsync(message.Chat.Id, "Science:\n");
                foreach (var kl in jsi.Items)
                {
                    string title = kl.VolumeInfo.Title;
                    var title2 = kl.VolumeInfo.Authors;
                    foreach (var item in title2)
                    {
                        await Bot.SendTextMessageAsync(message.Chat.Id, "✨The title: " + title + "\n" + "✨Author: " + item);
                    }
                }
            }
            else
            if (ev.CallbackQuery.Data == "history")
            {
                string url1 = "https://booksearch1.azurewebsites.net/api/genre/history";

                HttpClient client = new HttpClient();

                var result = await client.GetStringAsync(url1);

                Info jsi = JsonConvert.DeserializeObject<Info>(result);
                await Bot.SendTextMessageAsync(message.Chat.Id, "History:\n");
                foreach (var kl in jsi.Items)
                {
                    string title = kl.VolumeInfo.Title;
                    var title2 = kl.VolumeInfo.Authors;
                    foreach (var item in title2)
                    {
                        await Bot.SendTextMessageAsync(message.Chat.Id, "✨The title: " + title + "\n" + "✨Author: " + item);
                    }
                }
            }
            else
            if (ev.CallbackQuery.Data == "humor")
            {
                string url1 = "https://booksearch1.azurewebsites.net/api/genre/humor";

                HttpClient client = new HttpClient();

                var result = await client.GetStringAsync(url1);

                Info jsi = JsonConvert.DeserializeObject<Info>(result);
                await Bot.SendTextMessageAsync(message.Chat.Id, "Humor:\n");
                foreach (var kl in jsi.Items)
                {
                    string title = kl.VolumeInfo.Title;
                    var title2 = kl.VolumeInfo.Authors;
                    foreach (var item in title2)
                    {
                        await Bot.SendTextMessageAsync(message.Chat.Id, "✨The title: " + title + "\n" + "✨Author: " + item);
                    }
                }
            }
            else
            if (ev.CallbackQuery.Data == "tragedy")
            {
                string url1 = "https://booksearch1.azurewebsites.net/api/genre/tragedy";

                HttpClient client = new HttpClient();

                var result = await client.GetStringAsync(url1);

                Info jsi = JsonConvert.DeserializeObject<Info>(result);
                await Bot.SendTextMessageAsync(message.Chat.Id, "Tragedy:\n");
                foreach (var kl in jsi.Items)
                {
                    string title = kl.VolumeInfo.Title;
                    var title2 = kl.VolumeInfo.Authors;
                    foreach (var item in title2)
                    {
                        await Bot.SendTextMessageAsync(message.Chat.Id, "✨The title: " + title + "\n" + "✨Author: " + item);
                    }
                }
            }
        }    
        public static InlineKeyboardMarkup sitesRead = new InlineKeyboardMarkup(new[]
        {
                 new []{ InlineKeyboardButton.WithUrl("BooksOnline","http://booksonline.com.ua/")},
                 new []{ InlineKeyboardButton.WithUrl("Book-Online","http://book-online.com.ua/") },
                 new []{ InlineKeyboardButton.WithUrl("Tripx ", "https://www.twirpx.com/") },
                 new []{ InlineKeyboardButton.WithUrl("LitMir","https://www.litmir.me/") }
        });

        public static InlineKeyboardMarkup sitesBuy = new InlineKeyboardMarkup(new[]
        {
                 new []{ InlineKeyboardButton.WithUrl("PlayMarket", "https://play.google.com/store/books") },
                 new []{ InlineKeyboardButton.WithUrl("Amazon", "https://www.amazon.com/books-used-books-textbooks/") },
                 new []{ InlineKeyboardButton.WithUrl("Book24 ", "https://book24.ua/") },
                 new []{ InlineKeyboardButton.WithUrl("BookClub", "https://www.bookclub.ua/") }
        });
    }
    public partial class Info
    {
        [JsonProperty("items")]
        public List<Item> Items { get; set; }
    }
    public partial class Item
    {
        [JsonProperty("volumeInfo")]
        public VolumeInfo VolumeInfo { get; set; }

        [JsonProperty("saleInfo")]
        public SaleInfo SaleInfo { get; set; }
    }
    public partial class SaleInfo
    {
        [JsonProperty("saleability")]
        public string Saleability { get; set; }
    }
    public partial class  VolumeInfo
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("authors")]
        public List<string> Authors { get; set; }

        [JsonProperty("publisher")]
        public object Publisher { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("categories")]
        public List<string> Categories { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("previewLink")]
        public Uri PreviewLink { get; set; }
    }
    class Book
    {
        public string Name { get; set; }
    }

    public partial class Temp
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }
    }
}
