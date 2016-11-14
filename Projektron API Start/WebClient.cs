using System;
using System.Net;

namespace ConsoleApplication1 {
  internal class WebClient {
    public CookieContainer CookieJar { get; }

    public string UserAgent { get; set; }

    public WebClient() {
      this.CookieJar = new CookieContainer();
      this.UserAgent = @"Mozilla/5.0 (Windows NT 10.0; WOW64; rv:49.0) Gecko/20100101 Firefox/49.0";
    }

    public HttpWebResponse Get(string url) {
      RequestBuilder request = this.GetRequestInternal(url)
                                   .Method("GET");
      return Request(request);
    }

    public HttpWebResponse Post(string url) {
      RequestBuilder request = this.GetRequestInternal(url)
                                   .Method("POST");
      return Request(request);
    }

    private RequestBuilder GetRequestInternal(string url) {
      return this.GetRequestBuilder(url)
        .UserAgentIs(this.UserAgent)
        .WithCookieContainer(this.CookieJar);
    }

    public HttpWebResponse Request(RequestBuilder builder) {
      HttpWebRequest httpWebRequest = builder.GetRequest();
      return (HttpWebResponse)httpWebRequest.GetResponse();
    }

    public RequestBuilder GetRequestBuilder(string url) {
      HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
      return new RequestBuilder(httpWebRequest);
    }

    public HttpWebResponse Post(PostParameters postParameters) {
      RequestBuilder request = this.GetRequestInternal(postParameters.Url)
                                   .Method("POST")
                                   .AddHeaders(postParameters.Headers)
                                   .AddPostParameters(postParameters.Parameters)
                                   .Accept(postParameters.Accept)
                                   .AcceptEncoding(postParameters.AcceptEncoding)
                                   .ContentType(postParameters.ContentType);
      return Request(request);
    }
  }
}