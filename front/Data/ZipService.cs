using System.IO.Compression;
using System.Text;

public class ZipService
{
    //https://dotnet-snippets.de/snippet/strings-komprimieren-und-dekomprimieren/1058
    public async Task<string> ZipData(string data)
    {
        byte[] buffer = Encoding.UTF8.GetBytes(data);
        var memoryStream = new MemoryStream();
        using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
        {
            gZipStream.Write(buffer, 0, buffer.Length);
        }

        memoryStream.Position = 0;

        var compressedData = new byte[memoryStream.Length];
        memoryStream.Read(compressedData, 0, compressedData.Length);

        var gZipBuffer = new byte[compressedData.Length + 4];
        Buffer.BlockCopy(compressedData, 0, gZipBuffer, 4, compressedData.Length);
        Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gZipBuffer, 0, 4);
        return Convert.ToBase64String(gZipBuffer);
    }
}