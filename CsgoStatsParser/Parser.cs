using HtmlAgilityPack;

string? GetSteamId64()
{
    if (Environment.GetCommandLineArgs().Length >= 2)
        return Environment.GetCommandLineArgs()[1];
    else
        return null;
}
string Parse(string steamId64)
{
    string parseResult;
    var target = new Uri($"https://csgostats.gg/player/{steamId64}/");
    var handler = new HttpClientHandler();
    handler.UseDefaultCredentials = true;
    var client = new HttpClient(handler);
    client.DefaultRequestHeaders.Add("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
    //client.DefaultRequestHeaders.Add("accept-encoding", "gzip, deflate, br");
    client.DefaultRequestHeaders.Add("accept-language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
    client.DefaultRequestHeaders.Add("cache-control", "max-age=0");
    client.DefaultRequestHeaders.Add("Referer", "https://csgostats.gg/");
    client.DefaultRequestHeaders.Add("sec-ch-ua", "\" Not A; Brand \";v=\"99\", \"Chromium \";v=\"101\", \"Google Chrome\";v=\"101\"");
    client.DefaultRequestHeaders.Add("sec-ch-ua-mobile", "?0");
    client.DefaultRequestHeaders.Add("sec-ch-ua-platform", "\"Windows\"");
    client.DefaultRequestHeaders.Add("sec-fetch-dest", "document");
    client.DefaultRequestHeaders.Add("sec-fetch-mode", "navigate");
    client.DefaultRequestHeaders.Add("sec-fetch-site", "same-origin");
    client.DefaultRequestHeaders.Add("sec-fetch-user", "?1");
    client.DefaultRequestHeaders.Add("upgrade-insecure-requests", "1");
    client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.54 Safari/537.36");
    try
    {
        var response = client.GetAsync(target).Result;
        var html = response.Content.ReadAsStringAsync().Result;
        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(html);

        var imageRankSrc = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='player-ranks']/img").Attributes["src"].Value;
        var rank = imageRankSrc.Split('/').Last().Trim(')').Split('.').First();
        if (int.TryParse(rank, out _))
            parseResult = $"skillgroup{rank}";
        else
            parseResult = "skillgroup_none";
    }
    catch
    {
        parseResult = "error.parses";
    }
    handler.Dispose();
    client.Dispose();
    return parseResult;
}
string? steamId64 = GetSteamId64();
if (String.IsNullOrEmpty(steamId64))
    Console.Write("error.args");
else
    Console.Write(Parse(steamId64));
