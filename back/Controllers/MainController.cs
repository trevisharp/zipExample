using System.IO.Compression;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace back.Controllers;

public class CompressedData
{
    public string Data { get; set; }
}

[ApiController]
[Route("main")]
public class MainController : ControllerBase
{
    [HttpPost]
    public object Post([FromBody]CompressedData data)
    {
        Console.WriteLine(data.Data);
        byte[] gZipBuffer = Convert.FromBase64String(data.Data);
        using (var memoryStream = new MemoryStream())
        {
            int dataLength = BitConverter.ToInt32(gZipBuffer, 0);
            memoryStream.Write(gZipBuffer, 4, gZipBuffer.Length - 4);

            var buffer = new byte[dataLength];

            memoryStream.Position = 0;
            using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
            {
                gZipStream.Read(buffer, 0, buffer.Length);
            }

            return Encoding.UTF8.GetString(buffer);
        }
    }
}
