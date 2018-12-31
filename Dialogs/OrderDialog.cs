using System;
using System.Threading.Tasks;

using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;

namespace Microsoft.Bot.Sample.SimpleEchoBot
{
    [Serializable]
    public class OrderDialog : IDialog<object>
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
                context.Done("order completed");
            }
            else
            {
                string message = string.Format("You ordered {0}. Thanks",activity.Text);
                await context.PostAsync(message);
                context.Wait(MessageReceivedAsync);
            }
        }
    }
}
