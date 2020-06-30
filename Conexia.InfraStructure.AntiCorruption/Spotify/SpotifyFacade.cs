using Conexia.InfraStructure.AntiCorruption.Spotify.Entities;
using Conexia.Domain.Shared.Facades;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Conexia.InfraStructure.AntiCorruption.Services.Spotify
{
    public class SpotifyFacade : ISpotifyFacade
    {
        private const string spotifyApiToken = "https://accounts.spotify.com";
        private const string spotifyApi = "https://api.spotify.com/v1";

        private const string clientId = "9a09f83d68024f6aa381866913915f0b";
        private const string clientSecret = "59d1584144074d64bd74b9c0c561a9ce";

        private static Token GetToken()
        {
            var client = new RestClient(spotifyApiToken)
            {
                Authenticator = new HttpBasicAuthenticator(clientId, clientSecret)
            };

            var request = new RestRequest("/api/token", Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "client_credentials");
            
            var response = client.Execute(request);
            return JsonConvert.DeserializeObject<Token>(response.Content);
        }

        public object GetPlayListByGenre(string genre)
        {
            var disc = new List<Disc>();

            try
            {
                var _token = GetToken();

                var client = new RestClient(spotifyApi);
                var request = new RestRequest("/recommendations", Method.GET);
                request.AddHeader("Host", "api.spotify.com");
                request.AddHeader("Authorization", $"Bearer {_token.Access_Token}");
                request.AddHeader("content-type", "application/json");

                // Limite de registros
                request.AddParameter("limit", "5");
                request.AddParameter("market", "BR");

                // Parametro para buscar por genero
                request.AddParameter("seed_genres", genre);

                var response = client.Execute(request);
                var spotifyResult = JObject.Parse(response.Content);
                var albunsSpotify = spotifyResult["tracks"].Children().Select(x => x.ToObject<Disc>()).ToList();

                disc.AddRange(albunsSpotify);
            }
            catch (Exception ex)
            {
                disc.Add(new Disc { Id = "999", Name = ex.Message });
            }

            return disc;
        }
    }
}
