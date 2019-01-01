using System;
using System.Threading.Tasks;

using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using System.Net.Http;

using System.Collections.Generic;

namespace Microsoft.Bot.Sample.SimpleEchoBot
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {

        private string WelcomeMessage = "Hey. This is a bot. What can I do for you today? 1. Order, 2. FAQ"; 

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            //var message = await argument;
            //await context.PostAsync(WelcomeMessage);


            var message = context.MakeMessage();
            var actions = new List<CardAction>();

            actions.Add(new CardAction() { Title = "1. Order", Value = "1", Type = ActionTypes.ImBack });
            actions.Add(new CardAction() { Title = "2. FAQ", Value = "2", Type = ActionTypes.ImBack });

            message.Attachments.Add(
                new HeroCard
                {
                    Title="Select what you want",
                    Buttons=actions
                }.ToAttachment()
            );

            await context.PostAsync(message);
            context.Wait(SendWelcomeMessageAsync);
        }

        private async Task SendWelcomeMessageAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            string selected = activity.Text.Trim();

            if(selected == "1")
            {
                //await context.PostAsync("This is ordering food. please insert what you want to order");
                context.Call(new OrderDialog(), DialogResumeAfter);
            }
            else if(selected == "2")
            {
                //await context.PostAsync("This is FAQ. please insert your quesion.");
                context.Call(new OrderDialog(), DialogResumeAfter);
            }
            else
            {
                await context.PostAsync("wrong insert. please select 1 or 2");
                context.Wait(SendWelcomeMessageAsync);
            }
        }
        private async Task DialogResumeAfter(IDialogContext context, IAwaitable<object> result)
        {
            try
            {
                await context.PostAsync(WelcomeMessage);
            }
            catch(TooManyAttemptsException)
            {
                await context.PostAsync("Something is wrong.. Sorry");
            }
        }
    }
}