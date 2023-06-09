﻿using Grpc.Core;
using Grpc.Net.Client.Web;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Components;

namespace BlockchainApp.Web.Client.Helpers
{
    public static class ChannelExtensions
    {
        public static GrpcChannel GetAnonChannel(this NavigationManager navigation) =>
           GrpcChannel.ForAddress(navigation.BaseUri, new GrpcChannelOptions
           {
               HttpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()))
           });

        public static GrpcChannel GetAuthChannel(this NavigationManager navigation, string token) =>
            GrpcChannel.ForAddress(navigation.BaseUri,
                new GrpcChannelOptions
                {
                    HttpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler())),
                    Credentials = ChannelCredentials.Create(new SslCredentials(),
                        GetJwtCredentials(token))
                });

        private static CallCredentials GetJwtCredentials(string token) =>
            CallCredentials.FromInterceptor((_, metadata) =>
            {
                metadata.AddJwt(token);

                return Task.CompletedTask;
            });

        private static void AddJwt(this Metadata metadata, string token)
        {
            if (!string.IsNullOrEmpty(token))
                metadata.Add("Authorization", $"Bearer {token}");
        }
    }
}
