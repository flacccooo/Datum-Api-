using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/encrypt", async context =>
{
    // Get today's date
    DateTime today = DateTime.Today;
    string encryptedDate = EncryptCaesarCipher(today.ToString("yyyy-MM-dd"), 3);
    await context.Response.WriteAsync("Encrypted date using Caesar cipher: " + encryptedDate);
});

app.MapGet("/", async context =>
{
    // Get today's date
    DateTime today = DateTime.Today;
    await context.Response.WriteAsync("\nToday's date is: " + today.ToString("yyyy-MM-dd"));
});

app.Run();

static string EncryptCaesarCipher(string text, int shift)
{
    string encryptedText = "";
    foreach (char character in text)
    {
        if (char.IsLetter(character))
        {
            char shiftedChar = (char)(character + shift);
            if (char.IsLower(character))
            {
                if (shiftedChar > 'z')
                {
                    shiftedChar = (char)(shiftedChar - 26);
                }
                else if (shiftedChar < 'a')
                {
                    shiftedChar = (char)(shiftedChar + 26);
                }
            }
            else if (char.IsUpper(character))
            {
                if (shiftedChar > 'Z')
                {
                    shiftedChar = (char)(shiftedChar - 26);
                }
                else if (shiftedChar < 'A')
                {
                    shiftedChar = (char)(shiftedChar + 26);
                }
            }
            encryptedText += shiftedChar;
        }
        else if (char.IsDigit(character))
        {
            char shiftedChar = (char)(character + shift);
            if (shiftedChar > '9')
            {
                shiftedChar = (char)(shiftedChar - 10);
            }
            else if (shiftedChar < '0')
            {
                shiftedChar = (char)(shiftedChar + 10);
            }
            encryptedText += shiftedChar;
        }
        else
        {
            encryptedText += character;
        }
    }
    return encryptedText;
}
