using DiscordBot.DiscordBotLogic;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordBot.Commands
{
    public class FunCommands : BaseCommandModule
    {
        [Command("ping")]
        [Description("Returns pong.")]
        public async Task Ping(CommandContext ctx) 
        {
            await ctx.Channel.SendMessageAsync("Pong").ConfigureAwait(false);
        }
        
        [Command("add")]
        [Description("Adds to numbers together.")]
        public async Task Add(CommandContext ctx,
            [Description("First number.")] int numberOne,
            [Description("Second number.")] int numberTwo)
        {
            await ctx.Channel
                .SendMessageAsync((numberOne + numberTwo).ToString())
                .ConfigureAwait(false);
        }

        [Command("respondmessage")]
        public async Task RespondMessage(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();

            var message = await interactivity.WaitForMessageAsync(x => x.Channel == ctx.Channel).ConfigureAwait(false);

            await ctx.Channel.SendMessageAsync(message.Result.Content);
        }

        [Command("respondreaction")]
        public async Task RespondReaction(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();

            var message = await interactivity.WaitForReactionAsync(x => x.Channel == ctx.Channel).ConfigureAwait(false);

            await ctx.Channel.SendMessageAsync(message.Result.Emoji);
        }

        [Command("poll")]
        public async Task Poll(CommandContext ctx, TimeSpan duration, params DiscordEmoji[] emojiOptions)
        {
            var interactivity = ctx.Client.GetInteractivity();
            var options = emojiOptions.Select(x => x.ToString());

            var pollEmbed = new DiscordEmbedBuilder
            {
                Title = "Poll",
                Description = string.Join(" ", options)
            };

            var pollMessage = await ctx.Channel.SendMessageAsync(embed: pollEmbed).ConfigureAwait(false);

            foreach (var option in emojiOptions)
            {
                await pollMessage.CreateReactionAsync(option).ConfigureAwait(false);
            }

            var result = await interactivity.CollectReactionsAsync(pollMessage, duration).ConfigureAwait(false);
            var distinctResult = result.Distinct();

            var results = distinctResult.Select(x => $"{x.Emoji}: {x.Total}");

            await ctx.Channel.SendMessageAsync(string.Join("\n", results)).ConfigureAwait(false);

        }

        [Command("elo")]
        public async Task Elo(CommandContext ctx, string playerName)
        {            
            await ctx.Channel.SendMessageAsync($"{playerName} : {BotLogic.ObtainFaceitElo(playerName)} ELO points.").ConfigureAwait(false);
        }

        [Command("last")]
        public async Task LastMatch(CommandContext ctx, string playerName)
        {

            await ctx.Channel.SendMessageAsync($"Last match stats:\n" +
                $"RESULT: \t{BotLogic.ObtainLastMatchResult(playerName)}\n" +
                $"MAP: \t{BotLogic.ObtainLastMatchMap(playerName)}\n" +
                $"SCORE: \t{BotLogic.ObtainLastMatchScore(playerName)}")
                .ConfigureAwait(false);
        }


    }
}
