﻿using Microsoft.AspNetCore.Mvc.Razor;
using System.Security.Cryptography;
using System.Text;

namespace NS.WebApp.MVC.Extensions;

public static class RazorHelpers
{
    public static string StockMessage(this RazorPage page, int quantity)
        => quantity > 0 ? $"Only {quantity} in stock!" : "Sold out product!";

    public static string CurrencyFormat(this RazorPage page, decimal value)
        => value > 0 ? string.Format(Thread.CurrentThread.CurrentCulture, "{0:C}", value) : "Free";

    public static string HashEmailForGravatar(this RazorPage page, string email)
    {
        var md5Hasher = MD5.Create();
        var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(email));
        var sBuilder = new StringBuilder();
        foreach (var t in data)
        {
            sBuilder.Append(t.ToString("x2"));
        }
        return sBuilder.ToString();
    }

    public static string UnitsPerProduct(this RazorPage page, int units) 
        => units > 1 ? $"{units} units" : $"{units} unit";

    public static string SelectOptionsPerQuantity(this RazorPage page, int quantity, int selectedValue = 0)
    {
        var sb = new StringBuilder();
        for (var i = 1; i <= quantity; i++)
        {
            var selected = "";
            if (i == selectedValue) selected = "selected";
            sb.Append($"<option {selected} value='{i}'>{i}</option>");
        }

        return sb.ToString();
    }
}
