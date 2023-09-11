using System.Text.RegularExpressions;
using NLE.Shared.Contracts;

namespace NLE.Test;

public partial class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void ParseLogLineTest()
    {
        var line = "[10/Sep/2023:14:04:11 +0000] - 200 200 - GET https planils-api.sonils.co.ao \"/api/portal/notifications/count\" [Client 105.172.143.112] [Length 102] [Gzip -] [Sent-to 192.168.10.63] \"Mozilla/5.0 (iPhone; CPU iPhone OS 16_6_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/16.6 Mobile/15E148 Safari/604.1\" \"https://portaldocliente.sonils.co.ao/\"";

        Match eventDateMatch = EventDateRegex().Match(line);

        var access = new AccessDTO();
        
        if (eventDateMatch.Success)
        {
            var val = eventDateMatch.Groups[1].Value[12..20];
            access.EventDate = Convert.ToDateTime($"{eventDateMatch.Groups[1].Value[..11]} {eventDateMatch.Groups[1].Value[12..20]}" );
        }

        var data = line.Split(" ");

        access.StatusCode = Convert.ToInt32(data[3]);
        access.HttpMethod = data[6];
        access.Length = Convert.ToInt64(data[13][..^1]);
        access.RemoteAddress = data[11][..^1];
        access.SentTo = data[17][..^1];
        access.Host = data[8];
        /*
        if (match.Success)
        {
            string dateTime = match.Groups[1].Value;
            string statusCode = match.Groups[2].Value;
            string responseSize = match.Groups[3].Value;
            string requestMethod = match.Groups[4].Value;
            string requestUrl = match.Groups[5].Value;
            string clientIP = match.Groups[7].Value;
            string userAgent = match.Groups[12].Value;
            string referrer = match.Groups[13].Value;

            Console.WriteLine($"Fecha y hora: {dateTime}");
            Console.WriteLine($"Código de estado: {statusCode}");
            Console.WriteLine($"Tamaño de respuesta: {responseSize}");
            Console.WriteLine($"Método HTTP: {requestMethod}");
            Console.WriteLine($"URL solicitada: {requestUrl}");
            Console.WriteLine($"IP del cliente: {clientIP}");
            Console.WriteLine($"User-Agent: {userAgent}");
            Console.WriteLine($"Referencia: {referrer}");
        }
        */
        Assert.Pass();
    }

    [GeneratedRegex(@"\[([^]]+)\]")]
    private static partial Regex EventDateRegex();
}