using System;
using System.Collections.Generic;
using System.Net;
using System.Security;
using System.Text;

namespace ConsoleApplication1 {
  internal class RequestBuilder : IRequestBuilder {
    private ICollection<byte[]> _postData;
    private HttpWebRequest _request { get; }

    public RequestBuilder(HttpWebRequest request) {
      this._request = request;
      this._postData = new List<byte[]>();
    }

    public RequestBuilder KeepAlive() {
      this._request.KeepAlive = true;
      return this;
    }

    public RequestBuilder AddHeader(string headerName, string content) {
      this._request.Headers.Add(headerName, content);
      return this;
    }

    public RequestBuilder AddPostParameter(string parameterName, SecureString content) {
      return this.AddPostParameter(parameterName, content.ToStandartString());
    }

    public HttpWebRequest GetRequest() {

      if (this._request.Method == "POST") {
        using (var dataStream = this._request.GetRequestStream()) {
          foreach (byte[] bytese in this._postData) {
            dataStream.Write(bytese, 0, bytese.Length);
          }
        }
      }

      return this._request;
    }

    public RequestBuilder AddPostParameters(IEnumerable<KeyValuePair<string, string>> headers) {
      foreach (KeyValuePair<string, string> header in headers) {
        this.AddPostParameter(header.Key, header.Value);
      }
      return this;
    }

    public RequestBuilder AddPostParameter(string parameterName, string content) {
      if (this._request.Method != "POST") {
        //TODO exception type
        throw new Exception("Must Be PostRequest");
      }

      int contentLength = parameterName.Length + content.Length + 2;
      byte[] bytes = new byte[contentLength];

      byte[] buffer = Encoding.UTF8.GetBytes(parameterName);
      buffer.CopyTo(bytes, 0);

      bytes[parameterName.Length] = (byte)'=';

      buffer = Encoding.UTF8.GetBytes(content);
      buffer.CopyTo(bytes, parameterName.Length + 1);

      bytes[bytes.Length - 1] = (byte)'&';

      this._postData.Add(bytes);

      return this;
    }

    public RequestBuilder WithPostData(byte[] postData) {
      if (this._request.Method == "POST") {
        if (postData != null) {
          using (var dataStream = this._request.GetRequestStream()) {
            dataStream.Write(postData, 0, postData.Length);
          }
        }
      }

      return this;
    }

    public RequestBuilder TimeoutIs(int timeout) {
      this._request.Timeout = timeout;
      return this;
    }

    public RequestBuilder Method(string metod) {
      this._request.Method = metod;
      return this;
    }

    public RequestBuilder Accept(string accept) {
      this._request.Accept = accept;
      return this;
    }
    public RequestBuilder AcceptEncoding(string encoding) {
      this._request.Headers.Add("AcceptEncoding", encoding);
      return this;
    }

    public RequestBuilder RefererIs(string referer) {
      this._request.Referer = referer;
      return this;
    }

    public RequestBuilder UserAgentIs(string userAgent) {
      this._request.UserAgent = userAgent;
      return this;
    }

    public RequestBuilder ContentType(string contentType) {
      this._request.ContentType = contentType;
      return this;
    }

    public RequestBuilder WithCookieContainer(CookieContainer cookieJar) {
      this._request.CookieContainer = cookieJar;
      return this;
    }

    public RequestBuilder UpgradeInsecureRequests(string value) {
      this._request.Headers.Add("Upgrade-Insecure-Requests", value);
      return this;
    }

    public RequestBuilder AddHeaders(IEnumerable<KeyValuePair<string, string>> parameters) {
      foreach (KeyValuePair<string, string> parameter in parameters) {
        this.AddHeader(parameter.Key, parameter.Value);
      }
      return this;
    }
  }
}