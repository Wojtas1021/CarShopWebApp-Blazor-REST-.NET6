﻿namespace CarShopApp.Blazor.Server.UI.Services.Base
{
    public partial class Client : IClient
    {
        public HttpClient HttpClient
        {
            get => _httpClient;
        }
    }
}
