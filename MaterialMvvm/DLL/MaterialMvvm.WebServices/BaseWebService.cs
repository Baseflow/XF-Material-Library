using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MaterialMvvm.WebServices
{
    /// <summary>
    /// The base class that is inherited by the other web service classes. 
    /// It contains the methods for sending GET, POST, PUT, and DELETE requests.
    /// </summary>
    public class BaseWebService
    {
        /// <summary>
        /// The base address of the server to which the requests will be sent.
        /// </summary>
        public const string BASE_ADDRESS = "http://your-server.com";

        /// <summary>
        /// The access token which can be attached to a request's Authorization header.
        /// </summary>
        public static string AccessToken;

        private readonly HttpClient _httpClient;

        public BaseWebService()
        {
            this._httpClient = new HttpClient(new ModernHttpClient.NativeMessageHandler())
            {
                BaseAddress = new Uri(BASE_ADDRESS),
                Timeout = TimeSpan.FromSeconds(30)
            };
        }

        /// <summary>
        /// Sends an asynchronous DELETE request.
        /// </summary>
        /// <param name="requestUri">The Unique Resource Identifier of the request.</param>
        /// <returns><c>HttpResponseMessage</c></returns>
        public Task<HttpResponseMessage> DeleteAsync(string requestUri)
        {
            this.Prepare();

            return this._httpClient.DeleteAsync(requestUri);
        }

        /// <summary>
        /// Sends an asynchronous GET request.
        /// </summary>
        /// <param name="requestUri">The Unique Resource Identifier of the request.</param>
        /// <returns><c>HttpResponseMessage</c></returns>
        public Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            this.Prepare();

            return this._httpClient.GetAsync(requestUri);
        }

        /// <summary>
        /// Sends an asynchronous POST request.
        /// </summary>
        /// <param name="requestUri">The Unique Resource Identifier of the request.</param>
        /// <param name="httpContent">The content of the request.</param>
        /// <returns><c>HttpResponseMessage</c></returns>
        public Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent httpContent = null)
        {
            this.Prepare();

            return this._httpClient.PostAsync(requestUri, httpContent);
        }

        /// <summary>
        /// Sends an asynchronous PUT request.
        /// </summary>
        /// <param name="requestUri">The Unique Resource Identifier of the request.</param>
        /// <param name="httpContent">The content of the request.</param>
        /// <returns><c>HttpResponseMessage</c></returns>
        public Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent httpContent = null)
        {
            this.Prepare();

            return this._httpClient.PutAsync(requestUri, httpContent);
        }

        /// <summary>
        /// Prepares the HttpClient by clearing the current default request headers from previous requests, and adding the default Accept header media type of "application/json".
        /// </summary>
        /// <param name="hasAuthorization">If true, adds the Authorization header with the value of Bearer plus the Access Token.</param>
        private void Prepare(bool hasAuthorization = false)
        {
            this._httpClient.DefaultRequestHeaders.Clear();
            this._httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (hasAuthorization)
            {
                this._httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken ?? throw new NullReferenceException("Access Token has no value"));
            }
        }
    }
}
