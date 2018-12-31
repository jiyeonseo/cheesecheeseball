using System;
using System.Threading.Tasks;

using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;

namespace Microsoft.Bot.Sample.SimpleEchoBot
{
    [Serializable]
    public class FAQDialog : IDialog<object>
    {

        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
            return Task.CompletedTask;
        }
        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result as Activity;

            if(activity.Text.Trim() == "done")
            {
                context.Done("Order completed");
            }
            else
            {
                await context.PostAsync("This is FAQ Dialog");
                context.Wait(MessageReceivedAsync);
            }
        }
    }
}
