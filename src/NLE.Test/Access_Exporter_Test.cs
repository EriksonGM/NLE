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

        var access = new AccessDTO();
        
        var data = line.Split(" ");

        access.EventDate = Convert.ToDateTime($"{data[0][1..12]} {data[0][13..]}");
        access.StatusCode = Convert.ToInt32(data[3]);
        access.HttpMethod = data[6];
        access.Length = Convert.ToInt64(data[13][..^1]);
        access.RemoteAddress = data[11][..^1];
        access.SentTo = data[17][..^1];
        access.Referer = data[^1].Replace("\"","");
        access.Host = data[8];
        access.Url = data[9];
        
        Assert.Pass();
    }
}