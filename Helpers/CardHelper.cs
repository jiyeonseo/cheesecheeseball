using System;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder;

using System.Collections.Generic;

namespace SimpleEchoBot.Helpers
{
    public static class CardHelper
    {
        public static Attachment GetHeroCard(string title, string subTitle, string image, string buttonText, string buttonValue)
        {
            // create image object 
            List<CardImage> images = new List<CardImage>();
            images.Add(new CardImage() { Url = image });

            // create button 
            List<CardAction> buttons = new List<CardAction>();
            buttons.Add(new CardAction() { Title = title, Value = buttonValue, Type = ActionTypes.ImBack });

            // create hero card
            HeroCard card = new HeroCard()
            {
                Title = title,
                Subtitle = subTitle,
                Images = images,
                Buttons = buttons
            };

            return card.ToAttachment();
        }

        public static Attachment GetThumbnailCard(string title, string subTitle, string image, string buttonText, string buttonValue)
        {
            // create image object 
            List<CardImage> images = new List<CardImage>();
            images.Add(new CardImage() { Url = image });

            // create button 
            List<CardAction> buttons = new List<CardAction>();
            buttons.Add(new CardAction() { Title = title, Value = buttonValue, Type = ActionTypes.ImBack });

            // create hero card
            ThumbnailCard thumbnail = new ThumbnailCard()
            {
                Title = title,
                Subtitle = subTitle,
                Images = images,
                Buttons = buttons
            };

            return thumbnail.ToAttachment();
        }
    }
}
