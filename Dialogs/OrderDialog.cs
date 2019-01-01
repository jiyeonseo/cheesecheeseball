using System;
using System.Threading.Tasks;

using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;

using System.Collections.Generic; // for using List
using SimpleEchoBot.Helpers;

namespace Microsoft.Bot.Sample.SimpleEchoBot
{
    [Serializable]
    public class OrderDialog : IDialog<object>
    {
        string ServerUrl = "http://localhost:3978/images";

        public async Task StartAsync(IDialogContext context)
        {
            //context.Wait(MessageReceivedAsync);
            //return Task.CompletedTask;

            await context.PostAsync("This is menu. Please insert the menu and 'done' when you finish the order");

            await this.MessageReceivedAsync(context, null);
        }
        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            //var activity = await result as Activity;

            //if(activity.Text.Trim() == "done")
            //{
            //    context.Done("order completed");
            //}
            //else
            //{
            //    string message = string.Format("You ordered {0}. Thanks",activity.Text);
            //    await context.PostAsync(message);
            //    context.Wait(MessageReceivedAsync);
            //}

            if(result != null)
            {
                var activity = await result as Activity;
                if(activity.Text == "order")
                {
                    await context.PostAsync("Thank you!");
                    context.Done("");
                    return;
                }
                else
                {
                    await context.PostAsync("You ordered " + activity.Text);
                }
            }
            else
            {
                await context.PostAsync("Please select the menu");
            }

            // list menu

            var message = context.MakeMessage();
            message.Attachments.Add(CardHelper.GetHeroCard("Order now", "You can order now", this.ServerUrl + "1.png", "Order Now", "order"));
            message.Attachments.Add(CardHelper.GetHeroCard("Menu 1: $5", "Menu 1 ", this.ServerUrl + "/1.png", "Menu 1", "1"));
            message.Attachments.Add(CardHelper.GetHeroCard("Menu 2: $7", "Menu 2 ", this.ServerUrl + "/2.png", "Menu 2", "2"));
            message.Attachments.Add(CardHelper.GetHeroCard("Menu 3: $8", "Menu 3 ", this.ServerUrl + "/3.png", "Menu 3", "3"));

            message.AttachmentLayout = "carousel";

            await context.PostAsync(message);
            context.Wait(this.MessageReceivedAsync);
        }
    }
}
