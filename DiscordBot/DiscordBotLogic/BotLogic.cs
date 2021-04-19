using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBot.DiscordBotLogic
{
    public class BotLogic
    {

        public static string ObtainFaceitElo(string playerName)
        {
            string url = $"https://faceitstats.com/player/{playerName}";
            var web = new HtmlAgilityPack.HtmlWeb();
            HtmlDocument doc = web.Load(url);

            string elo = doc.DocumentNode.SelectNodes("//*[@id=\"app\"]/main/div/div[1]/div[2]/div[1]/div/div[1]/h5")[0].InnerText;

            return elo;
        }

        public static string ObtainLastMatchResult(string playerName)
        {
            string url = $"https://faceitstats.com/player/{playerName}";
            var web = new HtmlAgilityPack.HtmlWeb();
            HtmlDocument doc = web.Load(url);

            string result = doc.DocumentNode.SelectNodes("//*[@id=\"app\"]/main/div/div[7]/div/table/tbody/tr[1]/td[1]/b" )[0].InnerText;           
            string map = doc.DocumentNode.SelectNodes("//*[@id=\"app\"]/main/div/div[7]/div/table/tbody/tr[1]/td[8]")[0].InnerText;
            string results = doc.DocumentNode.SelectNodes("//*[@id=\"app\"]/main/div/div[7]/div/table/tbody/tr[1]")[0].InnerText;

            return result;
        }

        public static string ObtainLastMatchMap(string playerName)
        {
            string url = $"https://faceitstats.com/player/{playerName}";
            var web = new HtmlAgilityPack.HtmlWeb();
            HtmlDocument doc = web.Load(url);

            string map = doc.DocumentNode.SelectNodes("//*[@id=\"app\"]/main/div/div[7]/div/table/tbody/tr[1]/td[8]")[0].InnerText;

            return map;
        }

        public static string ObtainLastMatchScore(string playerName)
        {
            string url = $"https://faceitstats.com/player/{playerName}";
            var web = new HtmlAgilityPack.HtmlWeb();
            HtmlDocument doc = web.Load(url);

            string score = doc.DocumentNode.SelectNodes("//*[@id=\"app\"]/main/div/div[7]/div/table/tbody/tr[1]/td[3]")[0].InnerText;

            return score;
        }
    }

}
